using CustomerOnboarding.Domain.Model;
using CustomerOnboarding.Domain.Model.DTO;
using PayStack.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.Services.Service
{
    public interface IPaymentService
    {
        //Task<Response<dynamic>> Pay(Payment payment);
        TransactionInitializeResponse Pay(PaymentDTO payment);
        Task<string> InitialisePayment(PaymentDTO payment);
        //TransactionVerifyResponse Verify(string reference);
         Task <dynamic> VerifyPayment(string reference);





        //Task<Response<dynamic>> Generate();
    }
}
