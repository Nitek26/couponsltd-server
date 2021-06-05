using System.Collections.Generic;
using System.Linq;

namespace CouponsLtd.ViewModels
{
    public class CollectionResponse<TResult>
    {
        public CollectionResponse(List<TResult> collectionResponse, int skip = 0, int limit = 100,List<string> errors=null,bool isSuccessful=true)
        {
            Count = collectionResponse.Count();
            IsSuccessful = isSuccessful;
            Items = collectionResponse;
            Errors = errors;
            Limit = limit;
        }

        public int Count { get; set; }
        public int Skip { get; set; }
        public int Limit { get; set; }
        public List<string> Errors { get; set; }
        public bool IsSuccessful { get; set; }
        public List<TResult> Items { get; set; }
    }
}
