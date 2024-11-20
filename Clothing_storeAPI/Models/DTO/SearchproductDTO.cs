namespace Clothing_storeAPI.Models.DTO
{
    public class SearchproductDTO
    {
        public string? productName { get; set; }
        public float? MinPrice { get; set; }
        public float? MaxPrice { get; set; }
        public int PageNumber { get; set; } = 1; // Trang hiện tại
        public int PageSize { get; set; } = 10;  // Số lượng sản phẩm trên mỗi trang
    }
}
