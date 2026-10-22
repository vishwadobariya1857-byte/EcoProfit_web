using EcoProfit.Data;
using EcoProfit.Models;
using EcoProfit.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace EcoProfit.Controllers;
public class WasteController(AppDbContext db,WasteAdvisor advisor) : Controller
{
 private int? UserId=>HttpContext.Session.GetInt32("UserId");
 public async Task<IActionResult> Index(string? search)
 {
  if(UserId==null)return RedirectToAction("Login","Account");
  var q=db.WasteEntries.Where(x=>x.UserId==UserId);
  if(!string.IsNullOrWhiteSpace(search))q=q.Where(x=>x.Material.Contains(search)||x.BusinessType.Contains(search));
  ViewBag.Search=search;return View(await q.OrderByDescending(x=>x.Id).ToListAsync());
 }
 public IActionResult Create()=>UserId==null?RedirectToAction("Login","Account"):View(new WasteEntry());
 [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Create([Bind("BusinessType,Material,QuantityKg,RatePerKg,Notes,RecordedAt")] WasteEntry entry)
 {
  if(UserId==null)return RedirectToAction("Login","Account");
  ValidateBusiness(entry);
  if(!ModelState.IsValid)return View(entry);
  entry.UserId=UserId.Value;db.WasteEntries.Add(entry);await db.SaveChangesAsync();TempData["Success"]="Waste entry saved.";
  return RedirectToAction(nameof(Details),new{id=entry.Id});
 }
 public async Task<IActionResult> Details(int id)
 {
  if(UserId==null)return RedirectToAction("Login","Account");
  var e=await db.WasteEntries.FirstOrDefaultAsync(x=>x.Id==id&&x.UserId==UserId);
  if(e==null)return NotFound();ViewBag.Advice=advisor.Analyze(e);return View(e);
 }
 public async Task<IActionResult> Edit(int id)
 {
  if(UserId==null)return RedirectToAction("Login","Account");
  var e=await db.WasteEntries.FirstOrDefaultAsync(x=>x.Id==id&&x.UserId==UserId);return e==null?NotFound():View(e);
 }
 [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Edit(int id,[Bind("BusinessType,Material,QuantityKg,RatePerKg,Notes,RecordedAt")] WasteEntry input)
 {
  if(UserId==null)return RedirectToAction("Login","Account");
  var e=await db.WasteEntries.FirstOrDefaultAsync(x=>x.Id==id&&x.UserId==UserId);if(e==null)return NotFound();
  ValidateBusiness(input);if(!ModelState.IsValid)return View(input);
  e.BusinessType=input.BusinessType;e.Material=input.Material;e.QuantityKg=input.QuantityKg;e.RatePerKg=input.RatePerKg;e.Notes=input.Notes;e.RecordedAt=input.RecordedAt;
  await db.SaveChangesAsync();TempData["Success"]="Entry updated.";return RedirectToAction(nameof(Index));
 }
 [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Delete(int id)
 {
  if(UserId==null)return RedirectToAction("Login","Account");
  var e=await db.WasteEntries.FirstOrDefaultAsync(x=>x.Id==id&&x.UserId==UserId);if(e==null)return NotFound();
  db.WasteEntries.Remove(e);await db.SaveChangesAsync();TempData["Success"]="Entry deleted.";return RedirectToAction(nameof(Index));
 }
 private void ValidateBusiness(WasteEntry e)
 {
  if(e.BusinessType!="Tailoring"&&e.BusinessType!="Plastic Bottle Manufacturing")ModelState.AddModelError(nameof(e.BusinessType),"Choose a valid business type.");
  if(e.RecordedAt.Date>DateTime.Today)ModelState.AddModelError(nameof(e.RecordedAt),"Date cannot be in the future.");
 }
}