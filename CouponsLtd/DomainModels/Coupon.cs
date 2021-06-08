using System;

namespace CouponsLtd.DomainModels
{
    public class Coupon
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActived { get; set; }
        public string PromoCode{ get; set; }
    }
}
