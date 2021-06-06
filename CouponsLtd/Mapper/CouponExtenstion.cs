using CouponsLtd.Data.Entities;
using CouponsLtd.DomainModels;
using CouponsLtd.UpsertModels;
using System.Collections.Generic;

namespace CouponsLtd.Mapper
{
    public static class CouponExtenstion
    {
        public static List<Coupon> MapToCoupon(this List<CouponDAO> dao)
        {
            var mappedData = new List<Coupon>();
            foreach (var d in dao)
            {
                var coupon = new Coupon()
                {
                    Id = d.Id,
                    Description = d.Description,
                    IsActived = false,//need to add some logic to map it with user
                    Name = d.Name
                };

                mappedData.Add(coupon);
            }

            return mappedData;
        }

        public static List<CouponDAO> MapToCouponDao(this List<CouponUpsert> coupons)
        {
            var mappedData = new List<CouponDAO>();
            foreach (var c in coupons)
            {
                var coupon = new CouponDAO()
                {
                    Id = System.Guid.NewGuid(),
                    Description = c.Description,
                    Name = c.Name,
                    Code=c.Code,
                    Created=System.DateTime.UtcNow
                };

                mappedData.Add(coupon);
            }

            return mappedData;
        }
    }
}
