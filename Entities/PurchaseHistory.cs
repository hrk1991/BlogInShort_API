namespace WebApi.Entities
{
    public class PurchaseHistory : General
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int PlanId { get; set; }
        public int Views {get; set;}
        public bool IsCompleted {get; set;}
    }
}