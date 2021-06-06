using CouponsLtd.Data;
using CouponsLtd.DomainModels;
using CouponsLtd.Helpers;
using CouponsLtd.Mapper;
using CouponsLtd.UpsertModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CouponsLtd.Services
{
    public class CouponService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _environment;

        public CouponService(ApplicationDbContext applicationDbContext, IWebHostEnvironment environment)
        {
            this._applicationDbContext = applicationDbContext;
            this._environment = environment;
        }

        public async Task<List<Coupon>> GetCoupons()
        {
            var data = await _applicationDbContext.Coupons.AsNoTracking().ToListAsync();
            var mappedData = data.MapToCoupon();
            return mappedData;
        }

        public async Task<bool> CreateCoupons(List<CouponUpsert> coupons, bool usePrefilledData)
        {
            if (usePrefilledData)
            {
                coupons = JsonHelper.LoadFromJson<List<CouponUpsert>>(_environment.ContentRootPath + "/Data/Mocks/coupons.json");
            }

            var mappedCoupons = coupons.MapToCouponDao();
            //for bigger data sets it should be bulk update
            foreach (var c in mappedCoupons)
            {
                await _applicationDbContext.Coupons.AddAsync(c);
            }

            await _applicationDbContext.SaveChangesAsync();
            return await Task.FromResult(true);
        }
    }
}
