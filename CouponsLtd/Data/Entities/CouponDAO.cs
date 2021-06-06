using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CouponsLtd.Data.Entities
{
    [Table("Coupon")]
    public class CouponDAO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public DateTime Created { get; set; }
    }
}
