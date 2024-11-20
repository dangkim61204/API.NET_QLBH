namespace Clothing_storeAPI.Models.DTO
{
    public class CategoryDTO
    {
        public int categoryId { get; set; }
        public string categoryName { get; set; }
        public bool status { get; set; }
        // Không bao gồm `Products`

    }
}
