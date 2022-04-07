using CustomerOnboarding.Domain.Model;
using CustomerOnboarding.Domain.Model.DTO;
using CustomerOnboarding.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PayStack.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.Services.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private PayStackApi payStack { get; set; }
        private readonly string token;
        private readonly AppDbContext _context;
        private readonly Audit _audit;
        public PaymentService(IConfiguration configuration, AppDbContext context, Audit audit)
        {
            _configuration = configuration;
            token = _configuration["PaystackSK:SecretKey"];
            payStack = new PayStackApi(token);
            _context = context;
            _audit = audit;

        }


        public TransactionInitializeResponse Pay(PaymentDTO payment)
        {
            TransactionInitializeRequest request = new()
            {
                AmountInKobo = payment.Amount * 100,
                Email = payment.Email,
                Reference = Generate().ToString(),
                Currency = "NGN",
            };

            TransactionInitializeResponse response = payStack.Transactions.Initialize(request);

            return response;


        }

        public async Task <string> InitialisePayment(PaymentDTO payment)
        {
            var transactionResponse = Pay(payment);
            if (transactionResponse.Status)
            {
                var transaction = new Payment()
                {
                    Amount = payment.Amount,
                    Email = payment.Email,
                    TransactionRef = transactionResponse.Data.Reference,
                    FirstName = payment.FirstName,
                    LastName = payment.LastName

                };
                var newPayment = Payment.CreatePayment(payment);
                await _context.AddAsync(transaction);
                await _context.SaveChangesAsync();
                return transactionResponse.Data.AuthorizationUrl;
            }
           
            return transactionResponse.Message;
        }

        
        // if (response.Status)
        // {
        //     var transaction = new Payment()
        //     {
        //         Amount = payment.Amount,
        //         Email = payment.Email,
        //         TransactionRef = request.Reference,
        //         FirstName = payment.FirstName,
        //         LastName = payment.LastName

        //     };
        //     await _context.Payments.AddAsync(transaction);
        //     await _context.SaveChangesAsync();

        // }
        //return request;



        public async Task<dynamic> VerifyPayment(string reference)
        {
            try
            {


                var result = payStack.Transactions.Verify(reference);

                //TransactionVerifyResponse response = payStack.Transactions.Verify(reference);
                if (result.Data.Status == "success")
                {
                    var transactionStatus = await _context.Payments.Where(x => x.TransactionRef == reference).FirstOrDefaultAsync();
                    if (transactionStatus != null)
                    {
                        transactionStatus.Status = true;
                        _context.Update(transactionStatus);
                   await _context.SaveChangesAsync();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                _audit.LogFatal(ex);
                throw;
            }
        }
      
        public static int Generate()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            return random.Next(100000000, 999999999);
        }

    }
}
