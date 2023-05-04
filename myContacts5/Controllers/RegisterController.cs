using myContacts5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace myContacts5.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        myphoneEntities db = new myphoneEntities();
        myphoneEntities2 db2 = new myphoneEntities2();

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult SaveData(user model)
        {
            db.users.Add(model);
            db.SaveChanges();
            return Json("Registration Successful", JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckValidUser(user model)
        {
            string result = "Fail";
            var DataItem = db.users.Where(x => x.email == model.email && x.password == model.password).SingleOrDefault();
            if (DataItem != null)
            {
                Session["UserID"] = DataItem.Id.ToString();
                Session["UserEmail"] = DataItem.email.ToString();
                result = "Success";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserProfile()
        {
            return View();
            /*if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }*/
        }

        public JsonResult GetContactList()
        {
            List<ContactsViewModel> ContList = db2.contactsTables.Where(x => x.IsDeleted == false).Select(x => new ContactsViewModel
            {
                ContactId = x.ContactId,
                Name = x.Name,
                Contact = x.Contact,
                Email = x.Email
            }).ToList();

            return Json(ContList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetContactById(int ContactId)
        {
            contactsTable model = db2.contactsTables.Where(x => x.ContactId == ContactId).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveInDatabase(ContactsViewModel model)
        {
            var result = false;
            try
            {
                if(model.ContactId>0)
                {
                    contactsTable Cont = db2.contactsTables.SingleOrDefault(x => x.IsDeleted == false && x.ContactId == model.ContactId);
                    Cont.Name = model.Name;
                    Cont.Contact = model.Contact;
                    Cont.Email = model.Email;
                    db2.SaveChanges();
                    result = true;
                }
                else
                {
                    contactsTable Cont = new contactsTable();
                    Cont.Name = model.Name;
                    Cont.Contact = model.Contact;
                    Cont.Email = model.Email;
                    Cont.IsDeleted = false;
                    db2.contactsTables.Add(Cont);
                    db2.SaveChanges();
                    result = true;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteContactRecord(int ContactId)
        {
            bool result = false;
            contactsTable Cont = db2.contactsTables.SingleOrDefault(x => x.IsDeleted == false && x.ContactId == ContactId);
            if (Cont != null)
            {
                Cont.IsDeleted = true;
                db2.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }

    
}