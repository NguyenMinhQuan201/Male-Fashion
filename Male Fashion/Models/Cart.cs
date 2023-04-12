namespace Male_Fashion.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public float Quantity { get; set; }
        public string ImagePath { get; set; }
        public float Price { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public float TotalPrice { get; set; }
        public bool Status { get; set; }
    }
}
