﻿namespace CouponsLtd.UpsertModels
{
    public class SearchParams
    {
        public string Text { get; set; }
        public int Skip { get; set; }
        public int Limit { get; set; }
        public int OrderBy { get; set; }
    }
}
