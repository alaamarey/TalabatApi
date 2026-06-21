using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specifications.OrderSpecification;
using Talabat.Core.Specifications.ProductSpecification;

namespace Talabat.Services
{
    public class OrderService : IOrderService
    {
        ///private readonly IGenaricRepository<Order> _orderRepository;
        ///private readonly IGenaricRepository<Product> _productRepository;
        ///private readonly IGenaricRepository<DeliveryMethod> _deliveryMethod;


        private readonly IBasketRepository _basketRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        public OrderService(IBasketRepository basketRepository, UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork , IPaymentService paymentService)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
            _userManager = userManager;

            ///_productRepository = productRepository;
            ///_orderRepository = orderRepository;
            ///_deliveryMethod = deliveryMethod;
        }

        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId)
        {
            // 1 // Get CustomerBasket by BasketId From IBasketRepository 
            var basket = await _basketRepository.GetBasketAsync(basketId);

            if(basket is null) return null;

            // 2 // Get BasketItems Seleted From From GenaricRepository<Basket>
            // **  //  Order علشان اتاكد من صحه المنتجات اللي في الباسكت يعني مفيش فسفوسه فسك هتدخل غلط لل 
            var orderItems = new List<OrderItem>();

            if (basket.BasketItems.Count > 0)
            {
                foreach (var basketItem in basket.BasketItems)
                {
                    var spec = new ProductWithBrandAndCategorySpecifications(basketItem.Id);
                    var product = await _unitOfWork.Repository<Product>().GetByIdWithSpecAsync(spec);
                    if (product is not null)
                    {
                        var productOrderItem = new ProductOrderItem(product.Id, product.Name, product.Description
                                 , product.PictureUrl, product.ProductBrand.Name, product.ProductCategory.Name);

                        var orderItem = new OrderItem(productOrderItem, product.Price, basketItem.Quantity);
                        orderItems.Add(orderItem);
                    }
                                }
            }


            // 3 // Calculate SubTotal of all BasketItems 
            var subTotal = orderItems.Sum(OI => OI.Price * OI.Quantity);


            // 4 // Get DeliveryMethod From GenaricRepository<DeliveryMethod>
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(deliveryMethodId);


            // 5 // Get Address
            var user = _userManager.Users.Include(U => U.Address).SingleOrDefault(U => U.Email == buyerEmail);
            Address address = new Address();
            if (user is not null)
            {
                //if (user.Address is not null)
                    address = new Address("user.Address.FirstName"," user.Address.LastName", "user.Address.Country", "user.Address.City"," user.Address.Street");
            }

            // 5 // ensure that i will create order with unique PaymentIntentId 
            ////  PaymentIntentId  اللي قبلي فشل في اني اعمل ال  Order  في ال  PaymentIntentId لل  Create  علي اساس ممكن وانا بعمل 

            var orderSpec = new OrderWithPaymentIntentId(basket.PaymentIntentId!);

            var existingOrder = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(orderSpec);

            if(existingOrder is not null) 
            {
                _unitOfWork.Repository<Order>().Delete(existingOrder);

                ////  احتمال مش اكيد ان مثلا الكلاينت غير في المنتجات زود او نقص مثلا 
                await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            }


            // 6 // Create Order
            var order = new Order(buyerEmail, address, deliveryMethod, orderItems, subTotal, basket.PaymentIntentId!);
            await _unitOfWork.Repository<Order>().AddAsync(order);


            // 7 // Save Order on DataBase
            var count = await _unitOfWork.CompleteAsync();
            if (count <= 0) return null;

            return order;
        }



        public async Task<IReadOnlyList<Order>?> GetOrdersForUserAsync(string buyerEmail)
            => await _unitOfWork.Repository<Order>().GettAllWithSpecAsync(new OrderFilterSpecification(buyerEmail));


        public async Task<Order?> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            var spec = new OrderFilterSpecification(buyerEmail, orderId);
            var order = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);
            if (order is not null) return order;
            return null;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        => await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();







    }
}
