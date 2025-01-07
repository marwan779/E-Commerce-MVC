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
    public class CompanyController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }
        public IActionResult Index()
        {
            List<Company> Companies = _unitOfWork.Company.GetAll().ToList();

            return View(Companies);
        }

        public IActionResult UpSert(int? id)
        {
           

            if(id == null || id == 0) // create
            {
				return View(new Company());
			}
            else // update 
            {
                Company Company = _unitOfWork.Company.Get(u => u.Id == id);
                return View(Company);
            }

        }

        [HttpPost]
        public IActionResult UpSert(Company Company)
        {
            if (ModelState.IsValid)
            {                

                if(Company.Id == 0)
                {
					_unitOfWork.Company.Add(Company);
					TempData["Success"] = "Company Created Successfully";
				}
                else
                {
					_unitOfWork.Company.Update(Company);
					TempData["Success"] = "Company Updated Successfully";

				}

				_unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                

				return View(Company);

			}  
        }

       

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var companyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
            if (companyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Company.Remove(companyToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion

    }
}
