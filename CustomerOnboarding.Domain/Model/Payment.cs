using CustomerOnboarding.Domain.Helpers;
using CustomerOnboarding.Domain.Model.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.Domain.Model
{
    public  class Payment
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string TransactionRef { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }   
        public int Amount { get; set; }
        public bool Status { get; set; }
        public DateTime dateTime { get; set; } = DateTime.Now; 
        public virtual long? CustomerId { get; set; }   

        [ForeignKey(nameof(CustomerId))]
        public virtual Customer Customer { get; set; }


        public static Payment CreatePayment(PaymentDTO payment)
        {
            var newPayment = Utilities.MapTo<Payment>(payment);
            return newPayment;
        }
    }
}
