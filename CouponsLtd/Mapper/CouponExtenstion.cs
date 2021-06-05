using CouponsLtd.Data.Entities;
using CouponsLtd.DomainModels;
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
                    IsActived = d.IsActived,
                    Name = d.Name
                };

                mappedData.Add(coupon);
            }

            return mappedData;
        }
    }
}
