using CustomerOnboarding.Domain.Model;
using CustomerOnboarding.Domain.Model.DTO;
using CustomerOnboarding.Helpers;
using CustomerOnboarding.Services.Service;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PayStack.Net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEWCustomerOnboarding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentsService;
        private readonly IConfiguration _configuration;
        private PayStackApi payStack { get; set; }
        private readonly string token;
        private readonly AppDbContext _context;
        private readonly Audit _audit;

        public PaymentsController(IConfiguration configuration, AppDbContext context, Audit audit, IPaymentService paymentService)
        {
            _configuration = configuration;
            token = _configuration["AppSettings:PaystackSK"];
            payStack = new PayStackApi(token);
            _context = context;
            _audit = audit;
            _paymentsService = paymentService;

        }

        [HttpGet]
        [Route("Get-All-Transactions")]
        public async Task<ActionResult<IEnumerable<Payment>>> ViewTransactions()
        {
            try
            {
                var allTransactions = _context.Payments.Where(x => x.Status == true).ToList();
                return allTransactions;
            }
            catch (Exception ex)
            {
                _audit.LogFatal(ex);
                throw ex;
            }
        }


        [HttpPost]
        [Route("Pay")]

        public async Task<IActionResult> Pay(PaymentDTO payment)
        {
            try
            {
                var pay = await  _paymentsService.InitialisePayment(payment);
                return Ok(pay);

            }
            catch (Exception ex)
            {
                _audit.LogFatal(ex);
                throw ex;
            }

        }

        [HttpGet]
        [Route("Verify-Payment")]
        public async Task<IActionResult> Verify(string reference)
        {
            try
            {
                var verifyPayment = await _paymentsService.VerifyPayment(reference);
                return Ok(verifyPayment);
            }
            catch (Exception ex)
            {
                _audit.LogFatal(ex);
                    throw ex;
            }
        }

    }
}
    

