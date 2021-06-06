using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CouponsLtd.Data.Entities
{
    [Table("UserCoupon")]

    public class UserCouponDAO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CouponId { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset Activated { get; set; }
    }
}
