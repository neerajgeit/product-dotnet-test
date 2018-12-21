using System;
using System.Linq;
using System.Web.Mvc;
using Product.DomainObjects.Models;
using Product.Services;

namespace ProductWeb.Controllers
{
    public class MaterialController : Controller
    {
        private readonly IMaterialService _materialService;

        public MaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        // GET
        public ActionResult Index()
        {
            return View(_materialService.GetAll());
        }

        // GET
        public ActionResult View(Int32 id)
        {
            ViewBag.SelectedMaterialId = id;
            //return View(_materialService.GetById(id));
            return View(_materialService.GetAllWithProducts().Where(mm=>mm.Materials.Count()>0).ToList());
        }

      

        // GET: RateReviews/Create
        public ActionResult CreateMaterial()
        {
            
            return View();
        }

        // POST: Material/CreateMaterial, This will create the Material
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMaterial([Bind(Include = "Name,Cost")] MaterialModel material)
        {
            if (ModelState.IsValid)
            {

                _materialService.Create(material.Name, material.Cost);
                return RedirectToAction("Index");
            }

            
            return View(material);
        }


       
        public void Merge(string materialIdToKeep, string materialIdToDelete)
        {
            ViewBag.Message = "";
            int[] listOf = { 0 };
            if (!string.IsNullOrEmpty(materialIdToDelete))
            {
                listOf = materialIdToDelete.Split(',').Select(int.Parse).ToArray();
            }
            
            _materialService.Merge(Convert.ToInt32( materialIdToKeep), listOf);

            ViewBag.Message = "Merging has done successfully, check ProdcutMaterials table for Quantity update.";

            //RedirectToAction("Index", "Material");
        }

    }
}