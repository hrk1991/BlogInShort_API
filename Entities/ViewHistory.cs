using System;

namespace WebApi.Entities
{
    public class ViewHistory : General
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public DateTime ViewTime { get; set; }
    }
}