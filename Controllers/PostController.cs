using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using WebApi.Entities;
using System;

namespace WebApi.Controllers
{
  [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class PostController : ControllerBase
    {
        private IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public IActionResult AddPost([FromBody]Post PostParam)
        {
            try 
            {
                
            var IsAdded = _postService.AddPost(PostParam);

            if (IsAdded)
               return Ok(_postService.GetAllPost());
            else
            return Ok("Error in adding Post");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public IActionResult GetAllPost()
        {
            return Ok(_postService.GetAllPost());
            
        }
    }
}
