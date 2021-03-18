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


namespace GameClubProject.Controllers
{
//    [System.Web.Mvc.AllowAnonymous, Route("UserProfile")]
    [Authorize]
    public class LogonProfileController : Controller
    {

        private readonly GameclubDBContext _context;

        
        public LogonProfileController(GameclubDBContext context)
        {
            _context = context;
        }

        public string Welcome(string name, int numTimes = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {numTimes}");
        }
        
        public IActionResult UserProfile()
        {
            return View();
        }

        public IActionResult login()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        public IActionResult register()
        {
            //var properties = new AuthenticationProperties { RedirectUri = Url.Action("register","LogonProfile") };
            //return Challenge(properties, GoogleDefaults.AuthenticationScheme);

            List<MembershipStatus> msList = new List<MembershipStatus>();
            msList=(from c in _context.MembershipStatuses select c).ToList();
            //msList.Insert(0,new MembershipStatus)
            ViewBag.MembershipStatusId = msList;
            return View();
        }

        [AllowAnonymous,Route("google-login")]        
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("Home", "GameQuery") };
            //ViewBag.ReturnUrl = returnUrl;
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [Route("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //if (User.Identity.IsAuthenticated)

                var claims = result.Principal.Identities
                .FirstOrDefault().Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });

            return Json(claims);
        }

        public IActionResult GoogleLogout()
        {
            return new SignOutResult(new[]
            {
                CookieAuthenticationDefaults.AuthenticationScheme,
            }
            );            
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



