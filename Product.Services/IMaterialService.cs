using System;
using System.Collections.Generic;
using Product.DomainObjects.Models;

namespace Product.Services
{
    public interface IMaterialService
    {
        MaterialModel GetById(Int32 id);
        IList<MaterialModel> GetAll();



        IList<MaterialModel> GetAllWithProducts();

        void Merge(Int32 materialIdToKeep, Int32 [] materialIdToDelete);

        void Update(Int32 id, String name, Decimal cost);

        void Create(String name, Decimal cost);
        void Delete(Int32 id);
    }
}