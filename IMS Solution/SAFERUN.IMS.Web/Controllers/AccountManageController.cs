﻿
 

using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SAFERUN.IMS.Web.Models;
using SAFERUN.IMS.Web.Extensions;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Controllers
{
    [Authorize]
   public class AccountManageController : Controller
    {
        private ApplicationUserManager _userManager;

        public AccountManageController()
        {
        }

        public AccountManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }
        public ActionResult Index()
        {
            return View();
        }
      
        [HttpGet]
        public ActionResult GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            int totalCount = 0;

            var users = _userManager.Users.OrderByName(sort, order);
            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    if (filter.field == "UserName")
                    {
                        users = users.Where(x => x.UserName.Contains(filter.value));
                    }
                    if (filter.field == "Email")
                    {
                        users = users.Where(x => x.Email.Contains(filter.value));
                    }
                    if (filter.field == "PhoneNumber")
                    {
                        users = users.Where(x => x.PhoneNumber.Contains(filter.value));
                    }
                }
            }
            totalCount = users.Count();
            var datalist = users.Skip((page-1)*rows).Take(rows);
            var datarows = datalist.Select(n => new { Id = n.Id, UserName = n.UserName, Email = n.Email, PhoneNumber = n.PhoneNumber, AccessFailedCount = n.AccessFailedCount, LockoutEnabled = n.LockoutEnabled, LockoutEndDateUtc = n.LockoutEndDateUtc }).ToList();
            var pagelist = new { total = totalCount, rows = datarows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveData(UserChangeViewModel users)
        {
            if (users.updated != null)
            {
                foreach (var updated in users.updated)
                {
                    var user = new ApplicationUser { UserName = updated.UserName, PhoneNumber = updated.PhoneNumber, Email = updated.Email };
                    var result = UserManager.Update(user);
                }
            }
            if (users.deleted != null)
            {
                foreach (var deleted in users.deleted)
                {
                    var user = new ApplicationUser { UserName = deleted.UserName, PhoneNumber = deleted.PhoneNumber, Email = deleted.Email };
                    var result = UserManager.Delete(user);
                }
            }
            if (users.inserted != null)
            {
                foreach (var inserted in users.inserted)
                {
                    var user = new ApplicationUser { UserName = inserted.UserName, PhoneNumber= inserted.PhoneNumber, Email = inserted.Email };
                    var result =   UserManager.Create(user, "123456");
                }
            }
           

            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "AccountManager");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {

            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return View("Error");
            }

            return View(user);

        }

        [HttpPost]
        public async Task<ActionResult> Edit(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                var item = await UserManager.FindByIdAsync(user.Id);
                item.UserName = user.UserName;
                item.PhoneNumber = user.PhoneNumber;
                item.Email = user.Email;
                var result = await UserManager.UpdateAsync(item);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "AccountManager");
                }
                AddErrors(result);
            }
            return View(user);

        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(id);
                var result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    if (Request.IsAjaxRequest())
                    {
                        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                    }
                    return RedirectToAction("Index", "AccountManager");
                }
                AddErrors(result);
            }
            return View();
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}