using System.ComponentModel.DataAnnotations;

namespace Evil.Domain.Entities
{
    public class Dragon
    {
        [Key]
        public int Level { get; set; }
        [Required]
        public decimal GoldCost { get; set; }
    }
}

