using CouponsLtd.Models;
using CouponsLtd.Services;
using CouponsLtd.UpsertModels;
using CouponsLtd.ViewModels;
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

        public CouponsController(ILogger<CouponsController> logger,CouponService couponService)
        {
            _logger = logger;
            this._couponService = couponService;
        }


        [HttpPost("searchcoupons")]
        public async Task<IActionResult> SearchCoupons([FromBody] SearchParams searchParams)
        {
           var x=await  _couponService.GetCoupons();
            var coupons = new List<CouponVM>
            {
                new CouponVM(){
                    Description="Some coupon description",
                    Id=Guid.NewGuid(),
                    Name="SomeWeb.com"
                },
                new CouponVM(){
                    Description="Some other coupon description",
                    Id=Guid.NewGuid(),
                    Name="OtherWeb.com"
                },
            };

            return Ok(Task.FromResult(new Response<List<CouponVM>>(coupons)));
        }

        [HttpPost("activatebonus/{couponId:guid}/{promoCode}")]
        public async Task<IActionResult> ActivateBonus(Guid couponId, string promoCode)
        {
            return Ok(Task.FromResult(new Response<string>("Activated")));
        }
    }
}
