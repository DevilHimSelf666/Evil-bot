using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

