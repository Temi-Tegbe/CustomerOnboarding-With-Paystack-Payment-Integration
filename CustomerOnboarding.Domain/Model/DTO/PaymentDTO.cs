using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.Domain.Model.DTO
{
    public class PaymentDTO
    {
        
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Amount { get; set; }
        //public bool Status { get; set; }
        public DateTime dateTime { get; set; } = DateTime.Now;
        public virtual long? CustomerId { get; set; }
    }
}
