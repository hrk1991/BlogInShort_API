namespace WebApi.Entities
{
    public class Plans : General
    {
        public int Id { get; set; }
        public int Points { get; set; }
        public int Views { get; set; }
        public bool IsActive {get; set;}
        public bool IsDeleted {get; set;}
    }
}