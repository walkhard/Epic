using System;

namespace Epic.Models
{
    public class User
    {
        public int Id { get; set; }

        public Guid Identifier { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}