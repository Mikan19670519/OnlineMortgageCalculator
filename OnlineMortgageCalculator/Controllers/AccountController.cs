using OnlineMortgageCalculator.Models;
using OnlineMortgageCalculator.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace OnlineMortgageCalculator.Controllers
{
	public class AccountController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Register()
		{
			Register register = new Register();
			return View(register);
		}

		[HttpPost]
		public ActionResult SaveRegisterDetails(Register registerDetails)
		{
			if (ModelState.IsValid)
			{
				using (var databaseContext = new LoginRegistrationEntities())
				{
					RegisterUser reglog = new RegisterUser();

					reglog.FirstName = registerDetails.FirstName;
					reglog.LastName = registerDetails.LastName;
					reglog.Email = registerDetails.Email;
					reglog.Password = registerDetails.Password;

					databaseContext.RegisterUsers.Add(reglog);
					databaseContext.SaveChanges();
				}

				ViewBag.Message = "User Details Saved";
				return View("Register", registerDetails);
			}
			else
			{
				return View("Register", registerDetails);
			}
		}

		public ActionResult Login()
		{
			LoginViewModel login = new LoginViewModel();

			return View(login);
		}

		[HttpPost]
		public ActionResult Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var isValidUser = IsValidUser(model);

				if (isValidUser != null)
					return View("Welcome", isValidUser);
				else
				{
					ModelState.AddModelError("Failure", "Wrong Username and password combination !");
					return View();
				}
			}
			else
			{
				return View(model);
			}
		}

		public ActionResult Welcome()
		{
			RegisterUser reguser = new RegisterUser();
			return View(reguser);
		}

		public RegisterUser IsValidUser(LoginViewModel model)
		{
			using (var dataContext = new LoginRegistrationEntities())
			{
				RegisterUser user = dataContext.RegisterUsers.Where(query => query.Email.Equals(model.Email) && query.Password.Equals(model.Password)).SingleOrDefault();

				if (user == null)
					return null;
				else
					return user;
			}
		}

		public ActionResult ForgotPassword()
		{
			return View();
		}

		[HttpPost]
		public ActionResult ForgotPassword(string EmailID)
		{
			string resetCode = Guid.NewGuid().ToString();
			var verifyUrl = "/Account/ResetPassword/" + resetCode;
			var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
			using (var context = new LoginRegistrationEntities())
			{
				var getUser = (from s in context.RegisterUsers where s.Email == EmailID select s).FirstOrDefault();
				if (getUser != null)
				{
					getUser.ResetPasswordCode = resetCode;
					context.Configuration.ValidateOnSaveEnabled = false;
					context.SaveChanges();

					var subject = "Password Reset Request";
					var body = "Hi " + getUser.FirstName + ", <br/> You recently requested to reset your password for your account. Click the link below to reset it. " +

						 " <br/><br/><a href='" + link + "'>" + link + "</a> <br/><br/>" +
						 "If you did not request a password reset, please ignore this email or reply to let us know.<br/><br/> Thank you";

					SendEmail(getUser.Email, body, subject);

					ViewBag.Message = "Reset password link has been sent to your email id.";
				}
				else
				{
					ViewBag.Message = "User doesn't exists.";
					return View();
				}
			}

			return View();
		}

		public ActionResult ResetPassword(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
			{
				return HttpNotFound();
			}

			using (var context = new LoginRegistrationEntities())
			{
				var user = context.RegisterUsers.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
				if (user != null)
				{
					ResetPasswordModel model = new ResetPasswordModel();
					model.ResetCode = id;
					return View(model);
				}
				else
				{
					return HttpNotFound();
				}
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ResetPassword(ResetPasswordModel model)
		{
			var message = "";
			if (ModelState.IsValid)
			{
				using (var context = new LoginRegistrationEntities())
				{
					var user = context.RegisterUsers.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
					if (user != null)
					{
						user.Password = model.NewPassword;
						user.ResetPasswordCode = "";
						context.Configuration.ValidateOnSaveEnabled = false;
						context.SaveChanges();
						message = "New password updated successfully";
					}
				}
			}
			else
			{
				message = "Something invalid";
			}
			ViewBag.Message = message;
			return View(model);
		}

		private void SendEmail(string emailAddress, string body, string subject)
		{
			using (MailMessage mm = new MailMessage("youremail@gmail.com", emailAddress))
			{
				mm.Subject = subject;
				mm.Body = body;

				mm.IsBodyHtml = true;
				SmtpClient smtp = new SmtpClient();
				smtp.Host = "smtp.gmail.com";
				smtp.EnableSsl = true;
				NetworkCredential NetworkCred = new NetworkCredential("youremail@gmail.com", "YourPassword");
				smtp.UseDefaultCredentials = true;
				smtp.Credentials = NetworkCred;
				smtp.Port = 587;
				smtp.Send(mm);
			}
		}
	}
}