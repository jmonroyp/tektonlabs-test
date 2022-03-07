namespace Test.Core.Dtos
{
    public class ExternalProductDetailDto
    {
        public int id { get; set; }
        public string title { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string image { get; set; }
        public ExternalProductRatingDto rating { get; set; }
    }

    public class ExternalProductRatingDto
    {
        public decimal rate { get; set; }
        public int count { get; set; }
    }
}