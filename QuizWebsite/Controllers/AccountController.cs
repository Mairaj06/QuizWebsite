﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessRule;
using DataModel;
using QuizWebsite.Utility;

namespace QuizWebsite.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logon()
        {
            return View();
        }
        
        [HttpPost]
        public JsonResult SaveUser(Users User)
        {
            string Key = "M@m06m@m";
            User.Password = EncryptDecrypt.Encrypt(User.Password, Key);
            BlUsers Obj = new BlUsers();
            Obj.AddUser(User);
            return Json(false);

        }
        [HttpPost]
        public JsonResult CreateUserSession(Users User)
        {
            SessionHelper Helper = new SessionHelper();
            Helper.SetSession("User", User);
            return Json(true);
        }
        public ActionResult UsersList()
        {
            return View();
        }

    }
}
