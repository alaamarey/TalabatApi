using StackExchange.Redis;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talabat.Core.Entities.Identity
{
    public class Adddress
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ApplicationUserId { get; set; }

    }
}