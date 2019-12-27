using System.ComponentModel.DataAnnotations;

namespace MyPlayground.DBContext
{
    public class CitiesList
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
    }
}