using CouponsLtd.Data.Entities;
using CouponsLtd.Mapper;
using CouponsLtd.Models;
using CouponsLtd.Services;
using CouponsLtd.UpsertModels;
using CouponsLtd.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CouponsLtd.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/coupons")]
    public class CouponsController : ControllerBase
    {

        private readonly ILogger<CouponsController> _logger;
        private readonly CouponService _couponService;
        private readonly IHttpContextAccessor _context;

        public CouponsController(ILogger<CouponsController> logger,
            CouponService couponService, IHttpContextAccessor context)
        {
            _logger = logger;
            _couponService = couponService;
            _context = context;
        }

        [HttpPost("searchcoupons")]
        public async Task<IActionResult> SearchCoupons([FromBody] SearchParams searchParams)
        {
            Guid userId = ((UserDAO)_context.HttpContext.Items["User"]).Id;

            var coupons = await _couponService.GetCoupons(searchParams, userId);
            List<CouponVM> couponsVM = coupons.MapToCouponVM();
            return Ok(Task.FromResult(new CollectionResponse<CouponVM>(couponsVM, searchParams)));
        }

        [HttpPost("activatebonus/{couponId:guid}/{promoCode}")]
        public async Task<IActionResult> ActivateBonus(Guid couponId, string promoCode)
        {
            Guid userId = ((UserDAO)_context.HttpContext.Items["User"]).Id;
            var result = await _couponService.ActivateCoupon(userId, couponId, promoCode);
            return Ok(result);
        }
    }
}
