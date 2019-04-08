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
    public interface IPostService
    {
        bool AddPost(Post post);
        IEnumerable<Post> GetAllPost();
        
    }

    public class PostService : IPostService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private static List<Post> _post = new List<Post>
        { 
           
        };

        public bool AddPost(Post post)
        {
            _post.Add(post);
            return true;
        }
        public IEnumerable<Post> GetAllPost()
        {
            return _post;
        }

        
    }
}