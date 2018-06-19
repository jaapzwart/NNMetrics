////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Controllers\AccountController.cs
//
// summary:	Implements the account controller class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NNMetrics.Models;
using NNMetrics.Models.AccountViewModels;
using NNMetrics.Services;


////////////////////////////////////////////////////////////////////////////////////////////////////
// namespace: NNMetrics.Controllers
//
// summary:	Controller for handling Account logic.
////////////////////////////////////////////////////////////////////////////////////////////////////

namespace NNMetrics.Controllers
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A controller for handling accounts. </summary>
    ///
    /// <remarks>   Administrator, 12/06/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        /// <summary>   Manager for user. </summary>
        private readonly UserManager<ApplicationUser> _userManager;
        /// <summary>   Manager for sign in. </summary>
        private readonly SignInManager<ApplicationUser> _signInManager;
        /// <summary>   The email sender. </summary>
        private readonly IEmailSender _emailSender;
        /// <summary>   The logger. </summary>
        private readonly ILogger _logger;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="userManager">      Manager for user. </param>
        /// <param name="signInManager">    Manager for sign in. </param>
        /// <param name="emailSender">      The email sender. </param>
        /// <param name="logger">           The logger. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets a message describing the error. </summary>
        ///
        /// <value> A message describing the error. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [TempData]
        public string ErrorMessage { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP GET requests) login. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="returnUrl">    (Optional) URL of the return. </param>
        ///
        /// <returns>   An asynchronous result that yields an IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) login. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="model">        The model. </param>
        /// <param name="returnUrl">    (Optional) URL of the return. </param>
        ///
        /// <returns>   An asynchronous result that yields an IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    Data.SharedData.userName = model.Email;
                    Data.SharedData.adminUserName = model.Email;

                    // Down security is crappy. Next implementation would be to create an Admin Table with field SecretWord.
                    // This SecretWord can be given for every instance of this web app and makes it more secure.
                    // To enhance security even more, make web app ssl and encrypt the secret word.
                    // TODO: implement better security. Not it must contain admin as admin user. Very bad.
                    if(!Data.SharedData.userName.Contains("admin"))
                    {
                        return RedirectToAction("index", "Teams");
                    }
                    return RedirectToLocal(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction(nameof(LoginWith2fa), new { returnUrl, model.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToAction(nameof(Lockout));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP GET requests) login with 2fa. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <exception cref="ApplicationException"> Thrown when an Application error condition occurs. </exception>
        ///
        /// <param name="rememberMe">   True to remember me. </param>
        /// <param name="returnUrl">    (Optional) URL of the return. </param>
        ///
        /// <returns>   An asynchronous result that yields an IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWith2fa(bool rememberMe, string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            var model = new LoginWith2faViewModel { RememberMe = rememberMe };
            ViewData["ReturnUrl"] = returnUrl;

            return View(model);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) login with 2fa. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <exception cref="ApplicationException"> Thrown when an Application error condition occurs. </exception>
        ///
        /// <param name="model">        The model. </param>
        /// <param name="rememberMe">   True to remember me. </param>
        /// <param name="returnUrl">    (Optional) URL of the return. </param>
        ///
        /// <returns>   An asynchronous result that yields an IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWith2fa(LoginWith2faViewModel model, bool rememberMe, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var authenticatorCode = model.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

            var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, model.RememberMachine);

            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID {UserId} logged in with 2fa.", user.Id);
                return RedirectToLocal(returnUrl);
            }
            else if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID {UserId} account locked out.", user.Id);
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                _logger.LogWarning("Invalid authenticator code entered for user with ID {UserId}.", user.Id);
                ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
                return View();
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP GET requests) login with recovery code. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <exception cref="ApplicationException"> Thrown when an Application error condition occurs. </exception>
        ///
        /// <param name="returnUrl">    (Optional) URL of the return. </param>
        ///
        /// <returns>   An asynchronous result that yields an IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWithRecoveryCode(string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) login with recovery code. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <exception cref="ApplicationException"> Thrown when an Application error condition occurs. </exception>
        ///
        /// <param name="model">        The model. </param>
        /// <param name="returnUrl">    (Optional) URL of the return. </param>
        ///
        /// <returns>   An asynchronous result that yields an IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWithRecoveryCode(LoginWithRecoveryCodeViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            var recoveryCode = model.RecoveryCode.Replace(" ", string.Empty);

            var result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);

            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID {UserId} logged in with a recovery code.", user.Id);
                return RedirectToLocal(returnUrl);
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID {UserId} account locked out.", user.Id);
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                _logger.LogWarning("Invalid recovery code entered for user with ID {UserId}", user.Id);
                ModelState.AddModelError(string.Empty, "Invalid recovery code entered.");
                return View();
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP GET requests) gets the lockout. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <returns>   An IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP GET requests) registers this object. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="returnUrl">    (Optional) URL of the return. </param>
        ///
        /// <returns>   An IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) registers this object. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="model">        The model. </param>
        /// <param name="returnUrl">    (Optional) URL of the return. </param>
        ///
        /// <returns>   An asynchronous result that yields an IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");

                    Data.SharedData.userName = model.Email;
                    Data.SharedData.adminUserName = model.Email;
                    if (Data.SharedData.userName != "admin@admin.com")
                    {
                        return RedirectToAction("index", "Metrics");
                    }
                    return RedirectToLocal(returnUrl);
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) logout. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <returns>   An asynchronous result that yields an IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            Data.SharedData.userName = "";
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) external login. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="provider">     The provider. </param>
        /// <param name="returnUrl">    (Optional) URL of the return. </param>
        ///
        /// <returns>   An IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// (An Action that handles HTTP GET requests) callback, called when the external login.
        /// </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="returnUrl">    (Optional) URL of the return. </param>
        /// <param name="remoteError">  (Optional) The remote error. </param>
        ///
        /// <returns>   An asynchronous result that yields an IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToAction(nameof(Login));
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in with {Name} provider.", info.LoginProvider);
                return RedirectToLocal(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["LoginProvider"] = info.LoginProvider;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                return View("ExternalLogin", new ExternalLoginViewModel { Email = email });
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) external login confirmation. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <exception cref="ApplicationException"> Thrown when an Application error condition occurs. </exception>
        ///
        /// <param name="model">        The model. </param>
        /// <param name="returnUrl">    (Optional) URL of the return. </param>
        ///
        /// <returns>   An asynchronous result that yields an IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    throw new ApplicationException("Error loading external login information during confirmation.");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(nameof(ExternalLogin), model);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP GET requests) confirm email. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <exception cref="ApplicationException"> Thrown when an Application error condition occurs. </exception>
        ///
        /// <param name="userId">   Identifier for the user. </param>
        /// <param name="code">     The code. </param>
        ///
        /// <returns>   An asynchronous result that yields an IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP GET requests) forgot password. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <returns>   An IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP POST requests) forgot password. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="model">    The model. </param>
        ///
        /// <returns>   An asynchronous result that yields an IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
                await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                   $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP GET requests) forgot password confirmation. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <returns>   An IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// (An Action that handles HTTP GET requests) resets the password described by code.
        /// </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <exception cref="ApplicationException"> Thrown when an Application error condition occurs. </exception>
        ///
        /// <param name="code"> (Optional) The code. </param>
        ///
        /// <returns>   An IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
            {
                throw new ApplicationException("A code must be supplied for password reset.");
            }
            var model = new ResetPasswordViewModel { Code = code };
            return View(model);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// (An Action that handles HTTP POST requests) resets the password described by model.
        /// </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="model">    The model. </param>
        ///
        /// <returns>   An asynchronous result that yields an IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            AddErrors(result);
            return View();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// (An Action that handles HTTP GET requests) resets the password confirmation.
        /// </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <returns>   An IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   (An Action that handles HTTP GET requests) access denied. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <returns>   An IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Helpers

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds the errors. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="result">   The result. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Redirect to local. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <param name="returnUrl">    URL of the return. </param>
        ///
        /// <returns>   An IActionResult. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }
}
