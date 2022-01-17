using PagedList;
using StaffRegistrationWebApp.Data;
using StaffRegistrationWebApp.Data.Models;
using StaffRegistrationWebApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StaffRegistrationWebApp.Controllers
{
    public class StaffController : Controller
    {
        // GET: Staff
        AppDBContext ctx;
        StaffRepository staffRepo = null;

        public StaffController()
        {
            ctx = new AppDBContext();
            staffRepo = new StaffRepository(ctx);
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult _staffList(DateTime? date=null,int pagesize = 15, int page = 1, string name = "")
        {
            IEnumerable<Stafftb> resultList = null;
                
                DateTime nextday = date.Value.AddDays(1);
           
            
            var skipindex = pagesize * (page - 1);
          
            resultList = staffRepo.GetStaff().Where(a => a.CreatedDateTime >= date && a.CreatedDateTime <= nextday).Where(a => a.Name.StartsWith(name)).OrderByDescending(a => a.DateOfJoin).Skip(skipindex).Take(pagesize);
            var totalCount = resultList.Count();
            var totalpages = (int)Math.Ceiling((double)totalCount / pagesize);
            var pagedList = new StaticPagedList<Stafftb>(resultList, page, pagesize, totalCount);
            ListwithPage<Stafftb> model = new ListwithPage<Stafftb>();
            model.Results = pagedList;
            model.TotalCount = totalCount;
            model.TotalPages = totalpages;
            return PartialView("_staffList", model);


        }

        public ActionResult _createStaffFormPartial(string formType, int? id)
        {

            Stafftb staff = new Stafftb();
            if (formType == "Add")
            {
                return PartialView("_createStaffFormPartial", staff);
            }
            else
            {
                Stafftb result = staffRepo.GetStaff().Where(a => a.ID == id).FirstOrDefault();
                return PartialView("_createStaffFormPartial", result);
            }
        }

        [HttpPost]
        public ActionResult CreateUpdateStaff(Stafftb staffObj)
        {
            Stafftb result;

            if (staffObj.ID > 0)
            {
                staffObj.CreatedDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Myanmar Standard Time"));

                result = staffRepo.UpdateStaffObj(staffObj);
                if (result != null)
                {
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
                return null;
            }
            else
            {
                staffObj.CreatedDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Myanmar Standard Time"));
                result = staffRepo.AddStaffObj(staffObj);
                if (result != null)
                {
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                   
                    return RedirectToAction("_createStaffFormPartial");
                }


            }
        }

        public ActionResult Delete(int id)
        {
            List<Stafftb> staffObj;
            if (id > 0)
            {
                staffObj = staffRepo.Delete(id);
                if (staffObj != null)
                {
                    return RedirectToAction("Index");
                }
                return null;
            }
            else
            {
                return Json("fail", JsonRequestBehavior.AllowGet);
            }

        }
    }
}