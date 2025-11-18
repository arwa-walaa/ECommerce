using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceDomain.Contarcts
{
    public interface ICachRepo
    {
        Task<string?> GetAsync(string key);
        Task SetAsync(string Key, string Value , TimeSpan TimeToLive);
       
    }
}
