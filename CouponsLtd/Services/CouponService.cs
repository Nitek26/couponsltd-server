using CouponsLtd.Data;
using CouponsLtd.Data.Entities;
using CouponsLtd.DomainModels;
using CouponsLtd.Helpers;
using CouponsLtd.Mapper;
using CouponsLtd.UpsertModels;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouponsLtd.Services
{
    public class CouponService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _environment;
        private readonly UnitOfWork _unitOfWork;

        public CouponService(ApplicationDbContext applicationDbContext,
            IWebHostEnvironment environment, UnitOfWork unitOfWork)
        {
            this._applicationDbContext = applicationDbContext;
            this._environment = environment;
            this._unitOfWork = unitOfWork;
        }
        public IOrderedQueryable<CouponDAO> OrderingMethod(IQueryable<CouponDAO> query, int order)
        {
            OrderByEnum orderBy = (OrderByEnum)order;

            switch (orderBy)
            {
                case OrderByEnum.Name:
                    return query.OrderBy(c => c.Name);
                case OrderByEnum.Created:
                    return query.OrderBy(c => c.Created);
                case OrderByEnum.Code:
                    return query.OrderBy(c => c.Code);
                default:
                    return query.OrderBy(c => c.Name);
            }
        }

        public async Task<List<Coupon>> GetCoupons(SearchParams p, Guid userId)
        {
            var couponsDAO = (await _unitOfWork.Coupons
                .GetAsync(p.Skip, p.Limit, x => x.Name.Contains(p.Text), x => OrderingMethod(x, p.OrderBy)));

            var activatedCoupons = (await _unitOfWork.UsersCoupons
                .GetAsync(p.Skip, p.Limit, x => x.UserId == userId));

            var coupons = couponsDAO.MapToCoupon();

            foreach (var active in activatedCoupons)
            {
                var toBeActivated = coupons.FirstOrDefault(x => x.Id == active.CouponId);
                if (toBeActivated != null)
                {
                    toBeActivated.IsActived = true;
                }
            }

            return coupons;
        }

        public async Task<bool> ActivateCoupon(Guid userId, Guid couponId, string promoCode)
        {
            var coupon = (await _unitOfWork.Coupons.GetAsync(0, 100, x => x.Id == couponId)).FirstOrDefault();
            if (promoCode != coupon?.Code)
            {
                return false;
            }

            var userCoupon = new UserCouponDAO()
            {
                Id = Guid.NewGuid(),
                IsActive = true,
                Activated = System.DateTime.UtcNow,
                CouponId = couponId,
                UserId = userId
            };

            await _applicationDbContext.UsersCoupons.AddAsync(userCoupon);
            await _applicationDbContext.SaveChangesAsync();

            return true;
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
            return true;
        }
    }
}
