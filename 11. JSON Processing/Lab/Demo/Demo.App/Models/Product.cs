namespace Demo.App.Models
{
    using Newtonsoft.Json;

    public class ProductDto
    {
        public ProductDto()
        {
        }

        public ProductDto(string name, string description, decimal price)
            :this()
        {
            this.Name = name;
            this.Description = description;
            this.Price = price;
        }

        [JsonProperty("product-name")] //Parse Name to product-name
        public string Name { get; set; }

        [JsonIgnore] //Skip the property
        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}