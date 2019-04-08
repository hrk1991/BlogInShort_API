using System;

namespace WebApi.Entities
{
    public class User : General
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string WebsiteLink { get; set; }
        public string Token { get; set; }
        public bool IsDeleted {get; set;}
        public DateTime LastLoginTime{get; set;}
    }
    
}