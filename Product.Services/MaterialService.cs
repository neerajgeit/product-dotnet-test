using System;
using System.Collections.Generic;
using System.Linq;
using Product.Data.Repositories;
using Product.DomainObjects.Models;

namespace Product.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _materialRepository;

        public MaterialService(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
        }

        public MaterialModel GetById(Int32 id)
        {
            return Transfer(_materialRepository.GetById(id));
        }


        public IList<MaterialModel> GetAll()
        {
            return _materialRepository.GetAll().Select(Transfer).ToList();
        }

        public IList<MaterialModel> GetAllWithProducts()
        {
            return _materialRepository.GetAllWithProducts().Select(TransferWithProductMaterialModel).ToList(); ;
        }

        public void Merge(Int32 materialIdToKeep, Int32[] materialIdToDelete)
        {
            foreach (var id in materialIdToDelete)
            {
                //Get all the ProductMaterial whith materialIdToDelete
                var _materialwithProductsDelete = _materialRepository.GetMaterialsWithProducts(id);
                var _productMaterialDelete = _materialwithProductsDelete.Select(p => p.ProductMaterials).ToList();
                for (int i = 0; i < _productMaterialDelete.Count(); i++)
                {
                    var collectionOfMaterial = _productMaterialDelete[i];

                    foreach (var prMaterial in collectionOfMaterial)
                    {

                        var _materialwithProductsToKeep = _materialRepository.GetMaterialsWithProducts(materialIdToKeep).FirstOrDefault();
                        var _actualProductMaterial = _materialwithProductsToKeep.ProductMaterials.FirstOrDefault();

                        //Need to update the quentity only and then delet the unused product details
                        _actualProductMaterial.Quantity += prMaterial.Quantity;
                        _materialRepository.UpdateProductMaterials(_actualProductMaterial);

                        _materialRepository.DeleteProductMaterials(prMaterial.ProductMaterialId);

                        _materialRepository.Delete(prMaterial.MaterialId);


                        if (collectionOfMaterial.Count() <= 0)
                        {
                            break;
                        }
                    }
                   
                }
                //}
                //Delete the Material (I would prefer to mark delte but this desing is like this so keep it as it is)

            }
        }


        public void Create(string name, decimal cost)
        {
            _materialRepository.Create(name, cost);
        }

        public void Update(Int32 id, String name, Decimal cost)
        {
            _materialRepository.Update(id, name, cost);

        }

        public void Delete(Int32 id)
        {
            _materialRepository.Delete(id);
        }

        private MaterialModel Transfer(Data.Entities.Material entity)
        {
            return new MaterialModel
            {
                MaterialId = entity.MaterialId,
                Name = entity.Name,
                Cost = entity.Cost

            };
        }


        private MaterialModel TransferWithProductMaterialModel(Data.Entities.Material entity)
        {
            var mm = new MaterialModel
            {
                MaterialId = entity.MaterialId,
                Name = entity.Name,
                Cost = entity.Cost,
                Materials = new List<ProductMaterialModel>()
            };


            foreach (var m in entity.ProductMaterials)
            {
                var material = new ProductMaterialModel { Quantity = m.Quantity };
                material.MaterialName = m.Material.Name;
                material.Quantity = m.Quantity;
                material.MaterialId = m.MaterialId;

                mm.Materials.Add(material);
            }

            return mm;
        }
    }
}