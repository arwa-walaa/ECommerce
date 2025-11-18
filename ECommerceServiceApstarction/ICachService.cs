using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceServiceApstarction
{
    public interface ICachService 
    {
        Task<string?> GetAsync(string key);
        Task SetAsync(string Key, object Value, TimeSpan TimeToLive);
    }
}
