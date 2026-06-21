using Microsoft.Extensions.Configuration;
using Stripe;
using System.Configuration;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specifications.OrderSpecification;
using Product = Talabat.Core.Entities.Product;

namespace Talabat.Services
{

    /// PaymentIntent => TotalSumOfProducts + deliveryCost .& ContentType  
    public class PaymentService : IPaymentService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public PaymentService(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId)
        {
            // ****************** Order اللي بيكون شايل تمن ال  PaymentIntentId  علشان يعمل ال  Stripe عايزه اكلم ال ******************



            // 1 //  SecretKey  انا اللي كنت مسجل عندك والدليل اهو  Stripe  فاكرني يا

            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];


            ////  PaymentIntentId محتاجها يعمل من خلالها  Stripe  بعد كدا بجهز شويه حاجات كدا علي السخان علشان 
            // 2 //  بتاعي  order اول حاجه محتاجين نحسب تمن ال 

            /// الاول علشان احسب تمن المنتجات  Basket  بجيب ال 
            var basket = await _basketRepository.GetBasketAsync(basketId);

            if (basket is null || !basket.BasketItems.Any()) return null;


            // 3 // بعدل علي المنتجات بحيث تاخد السعر الاصلي 
            if (basket.BasketItems.Any())
            {
                foreach (var basketItem in basket.BasketItems)
                {

                    var product = await _unitOfWork.Repository<Product>().GetAsync(basketItem.Id);

                    if (product?.Price != basketItem.Price)
                        basketItem.Price = product?.Price ?? 0;
                }
            }

            // 4 //   ShippingPrice  اختار  User بحسب برضوا لو كان ال 
            if (basket.DeliveryMethodId is not null)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(basket.DeliveryMethodId);
                basket.ShippingPrice = deliveryMethod?.Cost;
            }


            // 5 //  واقوله اتفضل يا باشا Stripe   اروح اكلم ال  PaymentIntentId   بعد ما جهزت حاجتي علي السخان علشان اعمل ال 

            // 5.1 //  Add  او مفيهاش وفي الحاله دي بعمل  Update   وفي الحاله دي انا بعمل  PaymentIntentId فيها  Basket  فانا هنا هيكون عندي حالتين الحاله الاولي ان بالفعل ال 


            PaymentIntentService paymentIntentService = new PaymentIntentService();

            if (basket.PaymentIntentId is null)  //  PaymentIntentId هعمل ال 
            {
                var paymentCreateOptions = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.BasketItems.Sum(i => (i.Price * 100) * i.Quantity) + (long)(basket.ShippingPrice * 100 ?? 0),
                    Currency = "usd",
                };

                var paymentIntent = await paymentIntentService.CreateAsync(paymentCreateOptions);

                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else // Amount من حيث ال  PaymentIntentId هعدل علي  ال 
            {

                var paymentUpdateOptions = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.BasketItems.Sum(i => (i.Price * 100) * i.Quantity) + (long)(basket.ShippingPrice * 100 ?? 0),
                };

                await paymentIntentService.UpdateAsync(basket.PaymentIntentId, paymentUpdateOptions);

            }


            await _basketRepository.UpdateOrCreateBasketAsync(basket);
            return basket;



            // ** PaymentMethod //    انا عارف اللي هيدفعلي هاخد منه فلوس اد ايه 
        }

        public async Task<Order> UpdatePaymentIntentToSucceededOrFailedAsync(string paymentIntentId, bool isSucceeded)
        {


            var spec = new OrderWithPaymentIntentId(paymentIntentId);
            var order = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);

            if(  isSucceeded)
            {

                order.OrderStatus = OrderStatus.PaymentSucceded;

            }
            else
            {

                order.OrderStatus = OrderStatus.PaymentFailed;

            }


           await _unitOfWork.CompleteAsync();


            return order;

        }

       
    }
}
