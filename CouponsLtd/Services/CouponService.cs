using CouponsLtd.Data;
using CouponsLtd.DomainModels;
using CouponsLtd.Mapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouponsLtd.Services
{
    public class CouponService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CouponService(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }

        public async Task<List<Coupon>> GetCoupons()
        {
            var data=await _applicationDbContext.Coupons.AsNoTracking().ToListAsync();
            var mappedData = data.MapToCoupon();
            return mappedData;
        }
    }
}
