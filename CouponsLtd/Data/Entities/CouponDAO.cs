using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouponsLtd.Data.Entities
{
    public class CouponDAO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActived { get; set; }
        public DateTime Created { get; set; }
    }
}
