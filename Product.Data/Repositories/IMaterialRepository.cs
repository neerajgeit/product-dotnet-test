using System;
using System.Collections.Generic;
using Product.Data.Entities;

namespace Product.Data.Repositories
{
    public interface IMaterialRepository
    {
        Material GetById(Int32 id);
        IList<Material> GetAll();

        IList<Material> GetAllWithProducts();

        IList<Entities.Material> GetMaterialsWithProducts(Int32 materialID);
        void Update(Int32 id, String name, Decimal cost);

        void UpdateProductMaterials(ProductMaterial productMaterial);
        void DeleteProductMaterials(Int32 id);

        void DeleteProductMaterialsByMaterialId(Int32 materialId);

        void Create(String name, Decimal cost);
        void Delete(Int32 id);
    }
}