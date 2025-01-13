using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DesafioSGP.Domain.Entities
{
    [Table("users")]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public EType Role { get; set; }

        public User(int id, string name, string email, string password, EType role)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            Role = role;
        }

        public User() { }

        public enum EType
        {
            [EnumMember(Value = "Admin")]
            Admin = 1,
            [EnumMember(Value = "User")]
            User = 2
        }
    }
}
