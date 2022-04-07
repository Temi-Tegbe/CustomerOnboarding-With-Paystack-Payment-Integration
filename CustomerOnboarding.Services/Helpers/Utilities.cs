using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.Services.Helpers
{
    public class Utilities
    {

        public static T MapTo<T>(dynamic data)
        {
            var dataString = JsonConvert.SerializeObject(data);
            return JsonConvert.DeserializeObject<T>(dataString);
        }
    }

}
