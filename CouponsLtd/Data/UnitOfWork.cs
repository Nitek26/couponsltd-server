using CouponsLtd.Data.Entities;
using System.Threading.Tasks;

namespace CouponsLtd.Data
{
    public class UnitOfWork
    {

        private ApplicationDbContext _dbContext;
        private BaseRepository<CouponDAO> _coupons;
        private BaseRepository<UserDAO> _users;
        private BaseRepository<UserCouponDAO> _usersCoupons;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<CouponDAO> Coupons
        {
            get
            {
                return _coupons ??
                    (_coupons = new BaseRepository<CouponDAO>(_dbContext));
            }
        }

        public IRepository<UserDAO> Users
        {
            get
            {
                return _users ??
                    (_users = new BaseRepository<UserDAO>(_dbContext));
            }
        }

        public IRepository<UserCouponDAO> UsersCoupons
        {
            get
            {
                return _usersCoupons ??
                    (_usersCoupons = new BaseRepository<UserCouponDAO>(_dbContext));
            }
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
