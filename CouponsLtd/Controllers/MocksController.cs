using CouponsLtd.Helpers;
using CouponsLtd.Services;
using CouponsLtd.UpsertModels;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _environment;

        public MocksController(ILogger<MocksController> logger,
            CouponService couponService, IUserService userService, IWebHostEnvironment environment)
        {
            _logger = logger;
            this._couponService = couponService;
            this._userService = userService;
            this._environment = environment;
        }

        public IWebHostEnvironment Environment { get; }

        [HttpPost("addmockcoupons/{usePrefilledData}")]
        public async Task<IActionResult> AddMockCoupons(bool usePrefilledData, [FromBody] List<CouponUpsert> coupons)
        {
            var result = await _couponService.CreateCoupons(coupons, usePrefilledData);
            if (!result)
                return BadRequest(new { message = "Something went wrong" });
            return Ok(result);
        }

        [HttpPost("createmockusers/{usePrefilledData}")]
        public async Task<IActionResult> CreateMockUsers(bool usePrefilledData, [FromBody] List<UserUpsert> users)
        {
            var result = await _userService.CreateUsers(users, usePrefilledData);

            if (!result)
                return BadRequest(new { message = "Something went wrong" });

            return Ok(result);
        }
    }
}
