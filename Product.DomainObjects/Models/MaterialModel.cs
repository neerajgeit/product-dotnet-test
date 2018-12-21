using System;
using System.Collections.Generic;

namespace Product.DomainObjects.Models
{
    public class MaterialModel
    {
        public Int32 MaterialId { get; set; }

        public String Name { get; set; }

        public Decimal Cost { get; set; }

        public IList<ProductMaterialModel> Materials { get; set; }
    }
}