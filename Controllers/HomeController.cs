using MemberForm.Models;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace MemberForm.Controllers
{
    public class HomeController : Controller
    {
        Repository repository = new Repository();

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login model)
        {
            var IsExists = repository.CheckLogin(model);
            if (IsExists)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Msg = "Invalid Username/Password";
                return View();
            }
            
        }

        public ActionResult Index()
        {
            List<RegistrationVM> memberDetails= repository.GetMemberDetails();
            return View(memberDetails);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegistrationVM registration)
        {
            bool result = false;
            if (registration != null)
            {
                result = repository.RegisterMember(registration);
            }
            if (result)
            {
                return RedirectToAction("Index", "Home");
            }
            else
                return View();
            
        }
        
        public ActionResult Update(int id)
        {
            var mem = repository.GetMemberDetails().Find(x => x.MemberId == id);
            RegistrationViewModel rvm = new RegistrationViewModel();
            rvm.MemberId = mem.MemberId;
            rvm.Name = mem.Name;
            rvm.ContactNo = mem.ContactNo;
            rvm.JoiningDate = mem.JoiningDate.ToString("yyyy-MM-dd");
            rvm.InvestmentDate = mem.InvestmentDate.ToString("yyyy-MM-dd");
            rvm.IDProof = mem.IDProof;
            rvm.Photo = mem.Photo;
            return View(rvm);
        }
        [HttpPost]
        public ActionResult Update(int id, RegistrationVM emp)
        {
            if (ModelState.IsValid == true)
            {
                bool check = repository.UpdateMember(emp);
                if (check == true)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public ActionResult Details(int id)
        {
            var member = repository.Details(id);
            return View(member);
        }
        
        [HttpPost]
        public ActionResult UpdateName(string name,int id)
        {
            repository.UpdateName(name,id);
            return RedirectToAction("Index");
        }
       
        [HttpPost]
        public ActionResult UpdateContactNo(string ContactNo, int id)
        {
            repository.UpdateContectNo(ContactNo, id);
            return Json("Success",JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateJoiningDate(string JoiningDate, int id)
        {
            repository.UpdtJoiningDate(JoiningDate, id);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateInvestmentDate(string InvestmentDate, int id)
        {
            repository.UpdtInvestmentDate(InvestmentDate, id);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateIDProof(HttpPostedFileBase IDProof, int MemberId)
        {
            repository.UpdateIDProof(IDProof, MemberId);
            var Update = repository.Details(MemberId);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdatePhoto(HttpPostedFileBase Photo, int MemberId)
        {
            repository.UpdatePhoto(Photo, MemberId);
            var Update = repository.Details(MemberId);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
    }
}