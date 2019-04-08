using System;

namespace WebApi.Entities
{
    public class LoginHistory : General
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime LoginTime { get; set; }
    }
}