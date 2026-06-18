using System.ComponentModel;

namespace FirstSiteShopWithMvc.Models
{
    public class Item
    {
        public Item(string name, int price, string description, string fullText, string image, int categoryId)
        {
            Name = name;
            Price = price;
            Description = description;
            FullText = fullText;
            Image = image;
            CategoryId = categoryId;
        }

        public Item() { }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string FullText { get; set; }
        public string Image {  get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }


    }
}
