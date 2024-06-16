using CompanyCatering.DataLayer.DBContext;
using CompanyCatering.DataLayer.Entities;
using CompanyCatering.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Runtime.ExceptionServices;

namespace CompanyCatering.Controllers
{
    public class AccountController : Controller
    {
        private readonly CompanyCateringDbContext _companyCateringDbContext;
        private readonly UserManager<Users> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;
        private readonly SignInManager<Users> _signManager;

        public AccountController(CompanyCateringDbContext onlineLibraryDbContext, IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration, UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            _companyCateringDbContext = onlineLibraryDbContext;
            _httpContextAccessor = httpContextAccessor;
            _config = configuration;
            _userManager = userManager;
            _signManager = signInManager;
        }
        public async Task<IActionResult> Index(string filterTerm)
        {
            var users = _companyCateringDbContext.Users
        .Where(p => (p.isDeleted == false || p.isDeleted == null))
        .OrderBy(p => p.UserName)
        .ToList();

            var userModels = new List<UsersModel>();
            foreach (var user in users)
            {
                var userModel = new UsersModel()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    isAdmin = user.isAdmin,
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName,
                    RegistrationDate = user.RegistrationDate,
                    Roles = new List<string>()
                };

                var roles = await _userManager.GetRolesAsync(user);
                foreach (var role in roles)
                {
                    userModel.Roles.Add(role);
                }

                userModels.Add(userModel);
            }

            if (!string.IsNullOrEmpty(filterTerm))
            {
                userModels = userModels.Where(p => p.UserName.Contains(filterTerm) || p.Email.Contains(filterTerm))
                                  .ToList();
            }
            return View(userModels);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost, ActionName("Register")]
        public async Task<IActionResult> Register([Bind("FirstName", "LastName", "Birthday", "Email", "Password", "Phone", "CookSelected")] AccountRegisterModel accountRegisterModel)
        {
            try
            {
                var User = new Users()
                {
                    Name = accountRegisterModel.Name,
                    Surname = accountRegisterModel.Surname,
                    Birthday = accountRegisterModel.Birthday,
                    Email = accountRegisterModel.Email,
                    PhoneNumber = accountRegisterModel.PhoneNumber,
                    IdNumber = accountRegisterModel.IdNumber,
                    Password = accountRegisterModel.Password,
                    Role = accountRegisterModel.CookSelected,
                };

                //Check for any user with same email.
                if (await _userManager.FindByEmailAsync(accountRegisterModel.Email) != null)
                {
                    throw new Exception("User with exists with this email");
                }

                //Create async
                var result = await _userManager.CreateAsync(User, accountRegisterModel.Password);
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.ToString());
                }
  

                var userProfileModel = new UserProfileModel()
                {
                    FirstName = User.Name,
                    LastName = User.Name,
                    Birthday = User.Birthday,
                    Email = User.Email,
                    Role = User.Role == true ? "Cook" : "Employee",
                    Phone = User.PhoneNumber,
                };
                return View("MyProfile", userProfileModel);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error in registering");
            }
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost, ActionName("LogIn")]
        public async Task<IActionResult> Login([Bind("Email", "Password")] LogInModel logInModel)
        {
            try
            {
                
                var user = await _userManager.FindByEmailAsync(logInModel.Email);
                if (user != null && await _userManager.CheckPasswordAsync(user, logInModel.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    List<Claim> authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };
                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecretKey"]));//Secret key

                    var token = new JwtSecurityToken(
                        issuer: _config["Issuer"], //Your App URL
                        audience: _config["Audience"], //Your App URL
                        expires: DateTime.Now.AddDays(3),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );
                    authClaims.Add(new Claim("token", token.Payload.ToString()));
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(authClaims, JwtBearerDefaults.AuthenticationScheme);
                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        IsPersistent = true
                    };
                    //Sign in on the session
                    await _signManager.SignInAsync(user, true);

                    //check if user is authenticated
                    if (_companyCateringDbContext.UserLogins.Where(p => p.UserId == user.Id.ToString()).FirstOrDefault() == null)
                    {
                        await _companyCateringDbContext.UserLogins.AddAsync(new IdentityUserLogin<string>()
                        {
                            LoginProvider = "Simple Web Application Log in" + user.Id,
                            ProviderDisplayName = "Application Log in",
                            UserId = user.Id.ToString(),
                            ProviderKey = "User Password",
                        });
                        await _companyCateringDbContext.SaveChangesAsync();

                    }
                    var userProfileModel = new UserProfileModel()
                    {
                        FirstName = user.Name,
                        LastName = user.Surname,
                        Email = user.Email,
                        Role = user.Role == true ? "Cook" : "Employee",
                        Phone = user.PhoneNumber,
                    };
                    return View("MyProfile", userProfileModel);
                }
                else
                {
                    throw new Exception("Error in logging in. Check your credentials");
                }

            }
            catch(Exception ex) 
            {
                ViewBag.ErrorMessage = $"{ex.Message}";
                return View();
            }
        }
        public async Task<IActionResult> MyProfile()
        {
            try
            {
                var sessionIdentity = User.Identity;
                //check if user is authenticated
                if (sessionIdentity.IsAuthenticated == false)
                {
                    return RedirectToAction("Login");
                }
                var userID = User.Claims.Where(p => p.Type == ClaimTypes.Email).FirstOrDefault().Value;
                var user = await _userManager.FindByEmailAsync(userID);
                if (user == null)
                {
                    throw new Exception("User not found");
                }
                var UserProfileModel = new UserProfileModel()
                {
                    FirstName = user.Name,
                    Email = user.Email,
                    IsAdmin = user.Role == true ? "Cook" : "Employee",
                    Phone = user.PhoneNumber,
                    Birthday = user.Birthday
                };
                return View(UserProfileModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error in getting profile");
            }
        }
    }
}


