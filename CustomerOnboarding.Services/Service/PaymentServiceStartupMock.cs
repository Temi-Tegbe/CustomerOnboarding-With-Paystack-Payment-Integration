using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.Services.Service
{
    public class PaymentServiceStartupMock
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IPaymentService, PaymentService>();
            //optionsBuilder.UseSqlServer("Server=10.0.41.101; Database=SpectaCreditCardDb; User=sa; Password=tylent; Connection Timeout=30;");
        }
    }
}
