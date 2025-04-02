using System.ComponentModel.DataAnnotations;

namespace ModelLayer
{
    public class TokenModel
    {
        [Key]
        public int id { get; set; }
        public required string name { get; set; }
        public required string symbol { get; set; }
        public long total_supply { get; set; }
        public required string contract_address { get; set; }
        public int total_holders { get; set; }
        public decimal price { get; set; }
    }
}
