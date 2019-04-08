namespace WebApi.Entities
{
    public class Post : General
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId{get; set;}
        public string Image { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public bool IsActive {get; set;}
        public bool IsDeleted {get; set;}
    }
}
