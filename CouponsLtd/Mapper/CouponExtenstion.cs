using CouponsLtd.Data.Entities;
using CouponsLtd.DomainModels;
using CouponsLtd.Models;
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
                    Name = d.Name,
                    PromoCode=d.Code
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
                    Code = c.Code,
                    Created = System.DateTime.UtcNow
                };

                mappedData.Add(coupon);
            }

            return mappedData;
        }

        public static List<CouponVM> MapToCouponVM(this List<Coupon> coupons)
        {
            var mappedData = new List<CouponVM>();
            foreach (var c in coupons)
            {
                var coupon = new CouponVM()
                {
                    Id=c.Id,
                    Description = c.Description,
                    Name = c.Name,
                    IsActived = c.IsActived,
                    PromoCode=c.PromoCode
                };

                mappedData.Add(coupon);
            }

            return mappedData;
        }
    }
}
