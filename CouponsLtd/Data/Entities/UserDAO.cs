using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CouponsLtd.Data.Entities
{
    [Table("User")]
    public class UserDAO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}