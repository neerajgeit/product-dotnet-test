using System;
using System.Collections.Generic;
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

        public void Update(Int32 id, String name, Decimal cost)
        {
            var entity = _context.Materials.First(x => x.MaterialId == id);
            entity.Name = name;
            entity.Cost = cost;
        }

        public void Delete(Int32 id)
        {
            var entity = _context.Materials.First(x => x.MaterialId == id);
            _context.Materials.Remove(entity);
        }
    }
}