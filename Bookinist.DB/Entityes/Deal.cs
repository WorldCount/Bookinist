﻿using System.ComponentModel.DataAnnotations.Schema;
using Bookinist.DB.Entityes.Base;

namespace Bookinist.DB.Entityes
{
    public class Deal : Entity
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public virtual Book Book { get; set; }
        public virtual Seller Seller { get; set; }
        public virtual  Buyer Buyer { get; set; }

        public override string ToString() => $"Сделка '{Book}: {Seller}, {Buyer}, {Price:C}'";
    }
}