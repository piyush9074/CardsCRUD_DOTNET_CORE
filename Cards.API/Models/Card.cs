﻿using System.ComponentModel.DataAnnotations;

namespace Cards.API.Models
{
    public class Card
    {
        [Key]
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public string CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public int CVC { get; set; }
    }
}
