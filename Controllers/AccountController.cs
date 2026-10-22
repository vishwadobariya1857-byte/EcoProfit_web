using EcoProfit.Data;
using EcoProfit.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace EcoProfit.Controllers;
public class AccountController(AppDbContext db) : Controller
{
 [HttpGet] public IActionResult Register()=>View(new RegisterForm());
 [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Register(RegisterForm model)
 {
  if(!ModelState.IsValid)return View(model);
  var email=model.Email.Trim().ToLowerInvariant();
  if(await db.Users.AnyAsync(x=>x.Email==email)){ModelState.AddModelError(nameof(model.Email),"Email already registered.");return View(model);}
  var user=new AppUser{FullName=model.FullName.Trim(),Email=email};
  user.PasswordHash=new PasswordHasher<AppUser>().HashPassword(user,model.Password);
  db.Users.Add(user);await db.SaveChangesAsync();
  HttpContext.Session.SetInt32("UserId",user.Id);HttpContext.Session.SetString("Name",user.FullName);
  return RedirectToAction("Index","Home");
 }
 [HttpGet] public IActionResult Login()=>View(new LoginForm());
 [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Login(LoginForm model)
 {
  if(!ModelState.IsValid)return View(model);
  var email=model.Email.Trim().ToLowerInvariant();
  var user=await db.Users.FirstOrDefaultAsync(x=>x.Email==email);
  if(user==null || new PasswordHasher<AppUser>().VerifyHashedPassword(user,user.PasswordHash,model.Password)==PasswordVerificationResult.Failed)
  {ModelState.AddModelError("","Invalid email or password.");return View(model);}
  HttpContext.Session.SetInt32("UserId",user.Id);HttpContext.Session.SetString("Name",user.FullName);
  return RedirectToAction("Index","Home");
 }
 [HttpPost,ValidateAntiForgeryToken] public IActionResult Logout(){HttpContext.Session.Clear();return RedirectToAction(nameof(Login));}
}