using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fmsvcbus.Sender
{
    public interface ISender
    {
        public Task<long> SendValueAsync(string value);
    }
}
