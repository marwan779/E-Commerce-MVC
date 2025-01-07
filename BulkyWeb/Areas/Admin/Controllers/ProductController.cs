using Microsoft.AspNetCore.Mvc;
using Bulky.DataAccess.Data;
using Bulky.Models;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork UnitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = UnitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> Products = _unitOfWork.Product.GetAll(IncludeProperties: "Category").ToList();

            return View(Products);
        }

        public IActionResult UpSert(int? id)
        {
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll()
			   .Select(u => new SelectListItem
			   {
				   Text = u.Name,
				   Value = u.Id.ToString()
			   }),

                Product = new Product()
		    };

            if(id == null || id == 0) // create
            {
				return View(productVM);
			}
            else // update 
            {
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }

        }

        [HttpPost]
        public IActionResult UpSert(ProductVM productVM, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                string RootPath = _webHostEnvironment.WebRootPath;
                if(Image != null)
                {
                    string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                    string ImagePath = Path.Combine(RootPath, @"Images\Product");

                    if(!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        string OldImagePath = Path.Combine(RootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                        if(System.IO.File.Exists(OldImagePath)) System.IO.File.Delete(OldImagePath);
                    }

                    using( var FileStream = new FileStream(Path.Combine(ImagePath, ImageName),FileMode.Create))
                    {
                       Image.CopyTo(FileStream);
                    }

                    productVM.Product.ImageUrl = @"\Images\Product\" + ImageName;
                }

                if(productVM.Product.Id == 0 || productVM.Product.Id == null)
                {
					_unitOfWork.Product.Add(productVM.Product);
					TempData["Success"] = "Product Created Successfully";
				}
                else
                {
					_unitOfWork.Product.Update(productVM.Product);
					TempData["Success"] = "Product Updated Successfully";

				}

				_unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll()
               .Select(u => new SelectListItem
               {
                   Text = u.Name,
                   Value = u.Id.ToString()
               });

				return View(productVM);

			}  
        }

       

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(IncludeProperties: "Category").ToList();
            return Json(new { data = objProductList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            string productPath = @"images\products\product-" + id;
            string finalPath = Path.Combine(_webHostEnvironment.WebRootPath, productPath);

            if (Directory.Exists(finalPath))
            {
                string[] filePaths = Directory.GetFiles(finalPath);
                foreach (string filePath in filePaths)
                {
                    System.IO.File.Delete(filePath);
                }

                Directory.Delete(finalPath);
            }


            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion

    }
}
