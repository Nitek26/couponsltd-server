using CouponsLtd.UpsertModels;
using System.Collections.Generic;
using System.Linq;

namespace CouponsLtd.ViewModels
{
    public class CollectionResponse<TResult>
    {
        public CollectionResponse(List<TResult> collectionResponse,SearchParams searchParams,List<string> errors=null,bool isSuccessful=true)
        {
            Count = collectionResponse.Count();
            IsSuccessful = isSuccessful;
            Items = collectionResponse;
            Errors = errors;
            SearchParams = searchParams;
        }

        public int Count { get; set; }
        public SearchParams SearchParams { get; set; }
        public List<string> Errors { get; set; }
        public bool IsSuccessful { get; set; }
        public List<TResult> Items { get; set; }
    }
}
