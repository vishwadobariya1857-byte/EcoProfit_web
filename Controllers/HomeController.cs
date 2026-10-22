using EcoProfit.Data;
using EcoProfit.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace EcoProfit.Controllers;
public class HomeController(AppDbContext db,WasteAdvisor advisor) : Controller
{
 public async Task<IActionResult> Index()
 {
  var id=HttpContext.Session.GetInt32("UserId");if(id==null)return RedirectToAction("Login","Account");
  var items=await db.WasteEntries.Where(x=>x.UserId==id).OrderByDescending(x=>x.RecordedAt).ToListAsync();
  ViewBag.TotalKg=items.Sum(x=>x.QuantityKg);ViewBag.TotalValue=items.Sum(x=>x.EstimatedValue);
  ViewBag.HighCount=items.Count(x=>advisor.Analyze(x).Priority=="High");
  return View(items);
 }
 public IActionResult Error()=>View();
}