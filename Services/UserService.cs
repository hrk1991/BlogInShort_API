using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll(string token);
        User GetUser(string username);
        IEnumerable<User> UpdateUser(User user);
        IEnumerable<User> DeleteUser(string username);
        User CreateUser(User user);
    }

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private static List<User> _users = new List<User>
        { 
           
        };

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public User Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;


            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;

            return user;
        }

        public IEnumerable<User> GetAll(string token)
        {
            // return users without passwords
            return _users.Select(x => {
                x.Password = null;
                x.Token = token;
                return x;
            });
        }

        public User GetUser(string username)
        {
            var user = new User();
            // return users without passwords
            user = _users.Where(y=>y.Username == username && y.IsDeleted == false).SingleOrDefault();
            if(user != null)
                user.Password = string.Empty;
            return user;
        }

        public IEnumerable<User> UpdateUser(User user)
        {
            
            _users.Where(x=>x.Username == user.Username).Select(usr=>{
                                usr.FirstName=user.FirstName; 
                                usr.LastName=user.LastName;
                                return usr;}).ToList();
            return _users;
        }

         public IEnumerable<User> DeleteUser(string username)
        {
            var user = new User();
            // return users without passwords
            user = _users.Where(y=>y.Username == username && y.IsDeleted == false).SingleOrDefault();
            user.Password = string.Empty;
            _users.Where(x=>x.Username == username).Select(usr=>{usr.IsDeleted=true; return usr;}).ToList();
            return _users;
        }
        public User CreateUser(User user)
        {
            var usr = new User();
             // return users without passwords
            usr = _users.Where(y => y.MobileNumber == user.MobileNumber
                                || y.Email == user.Email ).SingleOrDefault();
            if(usr != null)
            {
                user.ErrorMessage = "Mobile Number or Email already Exists !";
                user.IsError = true;
            }
            else
            {
                _users.Add(user);
            }
            return user;
        }
    }
}