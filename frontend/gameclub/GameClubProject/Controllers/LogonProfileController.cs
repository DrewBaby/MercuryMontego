using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text.Encodings.Web;
using GameClubProject.Models;
using GameClubProject.Data;


namespace GameClubProject.Controllers
{
//    [System.Web.Mvc.AllowAnonymous, Route("UserProfile")]
    [Authorize]
    public class LogonProfileController : Controller
    {

        private readonly GameclubDBContext _dbContext;
        private readonly  IGameClubData _gameClubData = null;

        public LogonProfileController(IGameClubData gameClubData, GameclubDBContext context)
        {
            _dbContext = context;
            _gameClubData = gameClubData;
        }

        public string Welcome(string name, int numTimes = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {numTimes}");
        }
        
        
        [HttpGet,AllowAnonymous]
        public IActionResult UserProfile(string returnUrl = null)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Home", "GameQuery");
        }

        //[Route("login")]
        public IActionResult login()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }
        //#1 - Initial Get register Page
        [AllowAnonymous]
        public async Task<IActionResult> register(bool isSuccess = false, int Userid = 0)
        {
            var model = new UserAccount();

            if (!User.Identity.IsAuthenticated) return RedirectToAction(nameof(login));
            var userid = User.Claims.FirstOrDefault().Value.ToString();
            var d = await _gameClubData.GetRegisteredUserByIdAsync(userid);            
            //if (d.FirstOrDefault().UserId == User.Claims.FirstOrDefault().Value.ToString())
            //{
            //    return RedirectToAction("Home","GameQuery");
            //}

            List<MembershipStatus> msList = new List<MembershipStatus>();
            msList=(from c in _dbContext.MembershipStatuses select c).ToList();
            //msList.Insert(0,new MembershipStatus)
            ViewBag.MembershipStatusId = msList;
            ViewBag.IsSuccess = isSuccess;
            ViewBag.UserId = Userid;
            model.UserId = User.Claims.FirstOrDefault().Value.ToString();
            model.Email = User.Claims.Where(e => e.Type.Contains("emailaddress")).Select(e => e.Value).SingleOrDefault();
            model.FirstName = User.Claims.Where(e => e.Type.Contains("givenname")).Select(e => e.Value).SingleOrDefault();
            model.LastName = User.Claims.Where(e => e.Type.Contains("surname")).Select(e => e.Value).SingleOrDefault();
            return View(model);
        }
        
        //#2 - Post Submission to Register Page
        [HttpPost]
        public async Task<IActionResult> register(UserAccount userAccount)
        {            
            if (ModelState.IsValid)
            {   
                //string returnAddress = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                
                int id = await _gameClubData.AddRegisteredUser(userAccount);
                if (id > 0)
                {
                    return RedirectToAction(nameof(register), new { isSuccess = true, bookId = id });
                }
            }
            return View();
        }


        [AllowAnonymous,Route("google-login")]        
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("Home", "GameQuery") };            
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [AllowAnonymous,Route("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {            
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (result == null)
                return RedirectToAction(nameof(login));
            else
            {
                
            }
            ////if (result.Succeeded)
            //if (!string.IsNullOrEmpty(returnURL))
            //{
            //    return LocalRedirect(returnURL);
            //}
           // if (User.Identity.IsAuthenticated)
            //{
                var claims = result.Principal.Identities
                                   .FirstOrDefault()
                                   .Claims
                                   .Select(claim => new
                                   {
                                       claim.Issuer,
                                       claim.OriginalIssuer,
                                       claim.Type,
                                       claim.Value

                                   });
                var test = User.Claims.FirstOrDefault();                
                return Json(claims);
            //} return Json(User.Identity.Name);                                 
        }
 
        [AllowAnonymous]
        public async Task<IActionResult> GoogleLogout()
        {
            
            if (User?.Identity.IsAuthenticated == true)
            {
                // delete local authentication cookie
                await HttpContext.SignOutAsync();     
             
                
            }
            return RedirectToAction("About","GameQuery");
            

        }

        public async Task EndSession()
        {
            // If AAD sends a single sign-out message to the app, end the user's session, but don't redirect to AAD for sign out.
            await HttpContext.SignOutAsync (CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> SignedOut()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                await EndSession();
            }
            return View();
        }
        //Secured
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}



