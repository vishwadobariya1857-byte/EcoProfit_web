using EcoProfit.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Globalization;

namespace EcoProfit.Controllers
{
    public class ReportsController : Controller
    {
        private readonly AppDbContext db;

        public ReportsController(AppDbContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            var id = HttpContext.Session.GetInt32("UserId");

            if (id == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var entries = await db.WasteEntries
                .Where(x => x.UserId == id)
                .OrderByDescending(x => x.RecordedAt)
                .ToListAsync();

            return View(entries);
        }

        public async Task<IActionResult> ExportCsv()
        {
            var id = HttpContext.Session.GetInt32("UserId");

            if (id == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var entries = await db.WasteEntries
                .Where(x => x.UserId == id)
                .OrderBy(x => x.Id)
                .ToListAsync();

            // Escape CSV values correctly
            static string Csv(string? value)
            {
                return "\"" + (value ?? "").Replace("\"", "\"\"") + "\"";
            }

            var sb = new StringBuilder();

            sb.AppendLine(
                "Date,Business,Material,QuantityKg,RatePerKg,EstimatedValue,Notes"
            );

            foreach (var e in entries)
            {
                sb.AppendLine(string.Join(",",
                    Csv(e.RecordedAt.ToString("yyyy-MM-dd")),
                    Csv(e.BusinessType),
                    Csv(e.Material),
                    e.QuantityKg.ToString(CultureInfo.InvariantCulture),
                    e.RatePerKg.ToString(CultureInfo.InvariantCulture),
                    e.EstimatedValue.ToString(CultureInfo.InvariantCulture),
                    Csv(e.Notes)
                ));
            }

            return File(
                new UTF8Encoding(true).GetBytes(sb.ToString()),
                "text/csv",
                "ecoprofit-report.csv"
            );
        }
    }
}