using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Product.Data.Context;
using Product.Data.Entities;

namespace Product.Data.Repositories
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly ProductDataContext _context;
        public MaterialRepository(ProductDataContext context)
        {
            _context = context;
        }

        public Material GetById(Int32 id)
        {
            return _context.Materials.FirstOrDefault(x => x.MaterialId == id);
        }

        public IList<Material> GetAll()
        {
            return _context.Materials.ToList();
        }


        public IList<Material> GetAllWithProducts()
        {
            return _context
                   .Materials
                   .Include(x => x.ProductMaterials)
                   .Include(x => x.ProductMaterials.Select(y => y.Material))
                   .ToList();
        }

        public IList<Entities.Material> GetMaterialsWithProducts(Int32 materialID)
        {
            return _context
                   .Materials
                   .Include(x => x.ProductMaterials)
                   .Include(x => x.ProductMaterials.Select(y => y.Material))
                   .Where(m =>m.MaterialId == materialID)
                   .ToList();
        }




        public void UpdateProductMaterials(ProductMaterial productMaterial)
        {
            _context.Entry(productMaterial).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteProductMaterials(Int32 id)
        {
            var entity = _context.ProductMaterials.First(x => x.ProductMaterialId == id);
            _context.ProductMaterials.Remove(entity);
            _context.SaveChanges();
        }

        public void DeleteProductMaterialsByMaterialId(Int32 materialId)
        {
            var entity = _context.ProductMaterials.First(x => x.MaterialId == materialId);
            _context.ProductMaterials.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Int32 id, String name, Decimal cost)
        {
            var entity = _context.Materials.First(x => x.MaterialId == id);
            entity.Name = name;
            entity.Cost = cost;
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Int32 id)
        {
            var entity = _context.Materials.First(x => x.MaterialId == id);
            _context.Materials.Remove(entity);
            _context.SaveChanges();
        }

        public void Create(string name, decimal cost)
        {
            Material material = new Material() { Name=name, Cost = cost};
            var entity = _context.Materials.Add(material);
            _context.SaveChanges();
        }

      
    }
}