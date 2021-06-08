using CouponsLtd.Services;
using CouponsLtd.UpsertModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CouponsLtd.Controllers
{
    [ApiController]
    [Route("api/v1/mocks")]
    public class MocksController : ControllerBase
    {

        private readonly ILogger<MocksController> _logger;
        private readonly CouponService _couponService;
        private readonly IUserService _userService;

        public MocksController(ILogger<MocksController> logger,
            CouponService couponService, IUserService userService)
        {
            _logger = logger;
            _couponService = couponService;
            _userService = userService;
        }

        [HttpPost("addmockcoupons/{usePrefilledData}")]
        public async Task<IActionResult> AddMockCoupons(bool usePrefilledData, [FromBody] List<CouponUpsert> coupons)
        {
            var result = await _couponService.CreateCoupons(coupons, usePrefilledData);         
            return Ok(result);
        }

        [HttpPost("createmockusers/{usePrefilledData}")]
        public async Task<IActionResult> CreateMockUsers(bool usePrefilledData, [FromBody] List<UserUpsert> users)
        {
            var result = await _userService.CreateUsers(users, usePrefilledData);
            return Ok(result);
        }
    }
}
