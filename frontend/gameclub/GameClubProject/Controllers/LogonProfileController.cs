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

        private readonly GameclubDBContext _context;
        private readonly  IGameClubData _gameClubData = null;

        public LogonProfileController(IGameClubData gameClubData, GameclubDBContext context)
        {
            _context = context;
            _gameClubData = gameClubData;
        }

        public string Welcome(string name, int numTimes = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {numTimes}");
        }
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult UserProfile(string returnUrl = null)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Home", "GameQuery");
        }

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
            msList=(from c in _context.MembershipStatuses select c).ToList();
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
        //[AllowAnonymous]
        //public IActionResult GoogleLogout()
        //{
        //    return new SignOutResult(new[]
        //    {
        //        CookieAuthenticationDefaults.AuthenticationScheme,
        //    }
        //    );
        //}

        [AllowAnonymous]
        public IActionResult GoogleLogout()
        {
            var callbackUrl = Url.Action("SignedOut", "LogonProfile", values: null, protocol: Request.Scheme);
            return SignOut(new AuthenticationProperties { RedirectUri = callbackUrl },
                CookieAuthenticationDefaults.AuthenticationScheme);

            //var properties = new AuthenticationProperties { RedirectUri = Url.Action("SignedOut") };
            //return Challenge(properties, GoogleDefaults.AuthenticationScheme);

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
        //public async Task<IActionResult> OnPostConfirmationAsync(string returnUrl = null)
        //{
        //    returnUrl = returnUrl ?? Url.Content("~/");
        //    // Get the information about the user from the external login provider
        //    var info = await _signInManager.GetExternalLoginInfoAsync();

        //    if (info == null)
        //    {
        //        ErrorMessage =
        //            "Error loading external login information during confirmation.";

        //        return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        var user = new IdentityUser
        //        {
        //            UserName = Input.Email,
        //            Email = Input.Email
        //        };

        //        var result = await _userManager.CreateAsync(user);

        //        if (result.Succeeded)
        //        {
        //            result = await _userManager.AddLoginAsync(user, info);

        //            if (result.Succeeded)
        //            {
        //                // If they exist, add claims to the user for:
        //                //    Given (first) name
        //                //    Locale
        //                //    Picture
        //                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.GivenName))
        //                {
        //                    await _userManager.AddClaimAsync(user,
        //                        info.Principal.FindFirst(ClaimTypes.GivenName));
        //                }

        //                if (info.Principal.HasClaim(c => c.Type == "urn:google:locale"))
        //                {
        //                    await _userManager.AddClaimAsync(user,
        //                        info.Principal.FindFirst("urn:google:locale"));
        //                }

        //                if (info.Principal.HasClaim(c => c.Type == "urn:google:picture"))
        //                {
        //                    await _userManager.AddClaimAsync(user,
        //                        info.Principal.FindFirst("urn:google:picture"));
        //                }

        //                // Include the access token in the properties
        //                var props = new AuthenticationProperties();
        //                props.StoreTokens(info.AuthenticationTokens);
        //                props.IsPersistent = true;

        //                await _signInManager.SignInAsync(user, props);

        //                _logger.LogInformation(
        //                    "User created an account using {Name} provider.",
        //                    info.LoginProvider);

        //                return LocalRedirect(returnUrl);
        //            }
        //        }

        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError(string.Empty, error.Description);
        //        }
        //    }

        //    LoginProvider = info.LoginProvider;
        //    ReturnUrl = returnUrl;
        //    return Page();
        //}
    }
}



