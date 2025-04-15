namespace ECommerce.Application.Features.Categories.DTOs
{
    public class GetCategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
