using EcoProfit.Models;
namespace EcoProfit.Services;
public record WasteAdvice(string Recommendation,string Priority,string Explanation,decimal EstimatedValue);
public class WasteAdvisor
{
 public WasteAdvice Analyze(WasteEntry e)
 {
  var m=e.Material.ToLowerInvariant();
  var business=e.BusinessType.ToLowerInvariant();
  string action;
  if (m.Contains("pet") || m.Contains("plastic") || m.Contains("bottle")) action="Segregate clean PET/plastic by type and contact an authorized recycler.";
  else if(m.Contains("fabric") || m.Contains("cloth") || m.Contains("cotton")) action="Sort fabric by size; consider pouches, patchwork or selling usable scraps.";
  else if(m.Contains("paper") || m.Contains("cardboard")) action="Keep paper dry and bundle it for a local paper recycler.";
  else if(m.Contains("metal") || m.Contains("aluminium")) action="Separate metal by type and request quotes from scrap dealers.";
  else if(business=="tailoring") action="Sort leftover materials for reuse or recycling; verify buyer demand.";
  else action="Segregate the material and check local reuse or authorized recycling options.";
  var priority=e.QuantityKg>=50?"High":e.QuantityKg>=15?"Medium":"Low";
  return new WasteAdvice(action,priority,"Priority is based on recorded quantity (High ≥ 50 kg; Medium ≥ 15 kg). Estimates are not live market prices.",e.EstimatedValue);
 }
}