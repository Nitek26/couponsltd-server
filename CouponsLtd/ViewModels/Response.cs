using System.Collections.Generic;

namespace CouponsLtd.ViewModels
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
