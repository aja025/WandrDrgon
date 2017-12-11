using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using WanderDragon.Data;
using WanderDragon.Models;

namespace WanderDragon.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly WanderDragonContext _context;
        private readonly ILogger _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            WanderDragonContext context,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        private async Task<UserModel> GetUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var claims = await _userManager.GetClaimsAsync(user);
            var vm = new UserModel
            {
                UserName = user.UserName,
                Claims = claims.ToDictionary(c => c.Type, c => c.Value)
            };
            return vm;
        }

        // POST: /Account/Login
        [HttpPost("login")]
        //[AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation(1, "User logged in.");
                    var user = await GetUser(model.Email);
                    return Ok(user);
                }
                if (result.RequiresTwoFactor)
                {
                    // TODO: need to handle 2 factor?
                    //return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning(2, "User account locked out.");
                    this.ModelState.AddModelError("", "User account locked out.");
                    return BadRequest(this.ModelState);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return BadRequest(this.ModelState);
                }
            }

            // If we got this far, something failed, redisplay form
            return BadRequest(this.ModelState);
        }

        // POST: /Account/LogOff
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            return Ok();
        }

        // POST: /Account/Register
        [HttpPost("Register")]
        //[AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]RegisterModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var profile = new Profile
                    {
                        UserId = user.Id,
                        UserName = user.UserName,
                        DisplayName = "",
                        AboutMe = "Add your info here!",
                        Image = "images/User.jpg"
                    };
                    _context.Profiles.Add(profile);
                    await _context.SaveChangesAsync();

                    await _userManager.AddClaimAsync(user, new Claim("profileId", profile.ProfileId.ToString()));

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");
                    var userViewModel = await GetUser(user.UserName);
                    return Ok(userViewModel);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed
            return BadRequest(this.ModelState);
        }

        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        //[AllowAnonymous]
        [HttpGet("GetExternalLogins")]
        public async Task<IEnumerable<ExternalLoginModel>> GetExternalLogins()
        {
            return (await _signInManager.GetExternalAuthenticationSchemesAsync()).Select(a => new ExternalLoginModel
            {
                DisplayName = a.DisplayName,
                AuthenticationScheme = a.Name   //a.AuthenticationScheme
            });
        }

        // POST: /Account/ExternalLogin
        [HttpGet("ExternalLogin")]
        //[AllowAnonymous]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        // GET: /Account/ExternalLoginCallback
        [HttpGet("ExternalLoginCallback")]
        //[AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            // TODO: what to do on error?
            if (remoteError != null)
            {
                _logger.LogError($"Error from external provider: {remoteError}");
                //return RedirectToPage("./Login");
                return LocalRedirect("/");
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                //return RedirectToAction(nameof(Login));
                return LocalRedirect("/");
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (result.Succeeded)
            {
                _logger.LogInformation(5, "User logged in with {Name} provider.", info.LoginProvider);
                return LocalRedirect("/dashboard");
                //return RedirectToLocal(returnUrl);
            }

            // TODO: refactor and handle ExternalLoginCallback logic
            //if (result.RequiresTwoFactor)
            //{
            //    return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl });
            //}
            if (result.IsLockedOut)
            {
                return View("Lockout");
                // TODO: refactor and handle lockout logic
                throw new Exception("lockout");
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["LoginProvider"] = info.LoginProvider;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                return LocalRedirect("/externalRegister");
                //return RedirectToLocal("/externalRegister");
                //return View("ExternalLoginConfirmation", new ExternalLoginConfirmationModel { Email = email });

                // #region development code
                // var user = new ApplicationUser { UserName = email, Email = email };
                // var createUserResult = await _userManager.CreateAsync(user);
                // if (createUserResult.Succeeded)
                // {
                //     createUserResult = await _userManager.AddLoginAsync(user, info);
                //     if (createUserResult.Succeeded)
                //     {
                //         var profile = new Profile
                //         {
                //             UserId = user.Id,
                //             UserName = user.UserName,
                //             DisplayName = "",
                //             AboutMe = "Add your info here!",
                //             Image = "images/User.jpg"
                //         };
                //         _context.Profiles.Add(profile);
                //         await _context.SaveChangesAsync();

                //         await _userManager.AddClaimAsync(user, new Claim("profileId", profile.ProfileId.ToString()));

                //         await _signInManager.SignInAsync(user, isPersistent: false);
                //         _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                //         //return LocalRedirect(Url.GetLocalUrl(returnUrl));
                //         return LocalRedirect("/");
                //         // var userViewModel = await GetUser(user.UserName);
                //         // return Ok(userViewModel);
                //     }
                // }
                // foreach (var error in createUserResult.Errors)
                // {
                //     ModelState.AddModelError(string.Empty, error.Description);
                // }
                // return LocalRedirect("/");
                // #endregion development code
            }
        }

        // POST: /Account/ExternalLoginConfirmation
        [HttpPost("ExternalLoginConfirmation")]
        //[AllowAnonymous]
        public async Task<IActionResult> ExternalLoginConfirmation([FromBody]ExternalLoginConfirmationModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    ModelState.AddModelError("", "ExternalLoginFailure");
                    return BadRequest(this.ModelState);
                    //return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        var profile = new Profile
                        {
                            UserId = user.Id,
                            UserName = user.UserName,
                            DisplayName = "",
                            AboutMe = "Add your info here!",
                            Image = "images/User.jpg"
                        };
                        _context.Profiles.Add(profile);
                        await _context.SaveChangesAsync();

                        await _userManager.AddClaimAsync(user, new Claim("profileId", profile.ProfileId.ToString()));
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogInformation(6, "User created an account using {Name} provider.", info.LoginProvider);
                        var userViewModel = await GetUser(user.UserName);
                        return Ok(userViewModel);
                    }
                }
                AddErrors(result);
            }

            // ViewData["ReturnUrl"] = returnUrl;
            return BadRequest(this.ModelState);
        }

        [HttpDelete("{id}")]
         public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByIdAsync(id);
            var claims = await _userManager.GetClaimsAsync(user);
            if (user == null)
            {
                return NotFound();
            }

            await _userManager.RemoveClaimsAsync(user, claims);
            await _userManager.DeleteAsync(user);
            return NoContent();
        }

        // TODO: refactor this, is the user supposed to see this?  Is the error log enough info?
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                _logger.LogError(error.Description);
            }
        }

        [HttpGet("checkAuthentication")]
        //[AllowAnonymous]
        public async Task<IActionResult> CheckAuthentication()
        {
            if (this._signInManager.IsSignedIn(this.User))
            {
                var userViewModel = await GetUser(this.User.Identity.Name);
                return Ok(userViewModel);
            }
            return Ok();
        }
    }
}