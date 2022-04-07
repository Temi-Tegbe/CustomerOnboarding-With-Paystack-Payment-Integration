using CustomerOnboarding.Domain.Helpers;
using CustomerOnboarding.Domain.Model.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.Domain.Model
{
    public class Customer : CustomerBase
    {
        [Key]
        [Required]
        public long CustomerId { get; set; }

        [Required]
        public long UserId { get; set; }

        public ApplicationUser User { get; set; }

        
      

        [Required]
        public DateTime DateRegistered { get; set; }

        public DateTime LastModified { get; set; }

        public static Customer CreateCustomer(CustomerRegistrationDTO customer)
        {
            var newCustomer = Utilities.MapTo<Customer>(customer);
            newCustomer.DateRegistered = DateTime.Now;
            newCustomer.LastModified = DateTime.Now;
            return newCustomer;
        }


    }
}
