using CouponsLtd.Filters;
using CouponsLtd.Models;
using CouponsLtd.UpsertModels;
using CouponsLtd.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CouponsLtd.Controllers
{
    [ApiKeyAuth]
    [ApiController]
    [Route("api/v1/coupons")]
    public class CouponsController : ControllerBase
    {

        private readonly ILogger<CouponsController> _logger;

        public CouponsController(ILogger<CouponsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCoupons()
        {
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
                new CouponVM(){
                    Description="Some different description",
                    Id=Guid.NewGuid(),
                    Name="DifferentWeb.com"
                }
            };

            return Ok(Task.FromResult(new Response<List<CouponVM>>(coupons)));
        }

        [HttpPost]
        public async Task<IActionResult> SearchCoupons(SearchParams searchParams)
        {
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

        [HttpPost]
        public async Task<IActionResult> ActivateBonus(Guid CouponId, string promoCode)
        {
            return Ok(Task.FromResult(new Response<string>("Activated")));
        }
    }
}
