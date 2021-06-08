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
        private readonly IWebHostEnvironment _environment;
        private readonly UnitOfWork _unitOfWork;

        public CouponService(IWebHostEnvironment environment, UnitOfWork unitOfWork)
        {
            _environment = environment;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Coupon>> GetCoupons(SearchParams p, Guid userId)
        {
            var couponsDAO = (await _unitOfWork.Coupons
                .GetAsync(p.Skip, p.Limit, x => x.Name.Contains(p.Text), x => OrderingMethod(x, p.OrderBy)));

            var activatedCoupons = (await _unitOfWork.UsersCoupons
                .GetAsync(p.Skip, p.Limit, x => x.UserId == userId));

            var mappedCoupons = new List<Coupon>();

            foreach (var c in couponsDAO)
            {
                var coupon = new Coupon()
                {
                    Id = c.Id,
                    Description = c.Description,
                    Name = c.Name,
                };

                var toBeActivated = activatedCoupons.FirstOrDefault(x => x.CouponId == c.Id);
                if (toBeActivated != null)
                {
                    coupon.IsActived = true;
                    coupon.PromoCode = c.Code;
                }

                mappedCoupons.Add(coupon);
            }

            return mappedCoupons;
        }

        public async Task<bool> ActivateCoupon(Guid userId, Guid couponId, string promoCode)
        {
            var coupon = (await _unitOfWork.Coupons.GetAsync(0, 1, x => x.Id == couponId)).FirstOrDefault();
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

            _unitOfWork.UsersCoupons.Insert(userCoupon);
            await _unitOfWork.CommitAsync();

            return true;
        }

        public async Task<int> CreateCoupons(List<CouponUpsert> coupons, bool usePrefilledData)
        {
            int inserted = 0;
            if (usePrefilledData)
            {
                coupons = JsonHelper.LoadFromJson<List<CouponUpsert>>(_environment.ContentRootPath + "/Data/Mocks/coupons.json");
            }

            var mappedCoupons = coupons.MapToCouponDao();
            //for bigger data sets it should be bulk update
            foreach (var c in mappedCoupons)
            {
                var couponDAO = (await _unitOfWork.Coupons.GetAsync(0, 1, x => x.Code == c.Code && x.Name == c.Name)).FirstOrDefault();
                if (couponDAO != null)
                    continue;
                _unitOfWork.Coupons.Insert(c);
                inserted++;
            }

            await _unitOfWork.CommitAsync();
            return inserted;
        }
        private IOrderedQueryable<CouponDAO> OrderingMethod(IQueryable<CouponDAO> query, OrderByEnum orderBy)
        {
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
    }
}
