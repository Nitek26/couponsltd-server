using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouponsLtd.DomainModels
{
    public class Response<TResult>
    {
        public Response(TResult itemResponse, List<string> errors = null, bool isSuccessful = true)
        {
            Errors = errors;
            IsSuccessful = isSuccessful;
            Item = itemResponse;
        }
        public List<string> Errors { get; set; }
        public bool IsSuccessful { get; set; }
        public TResult Item { get; set; }
    }
}
