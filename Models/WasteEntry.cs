using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EcoProfit.Models;
public class WasteEntry
{
 public int Id {get;set;}
 [Required,Display(Name="Business type")] public string BusinessType {get;set;} = "Tailoring";
 [Required,Display(Name="Waste material"),StringLength(100)] public string Material {get;set;} = "";
 [Range(0.01,1000000),Display(Name="Quantity (kg)")] public decimal QuantityKg {get;set;}
 [Range(0,1000000),Display(Name="Estimated recovery rate (₹/kg)")] public decimal RatePerKg {get;set;}
 [StringLength(300)] public string? Notes {get;set;}
 [DataType(DataType.Date),Display(Name="Recorded date")] public DateTime RecordedAt {get;set;} = DateTime.Today;
 public int UserId {get;set;}
 public AppUser? User {get;set;}
 [NotMapped] public decimal EstimatedValue => QuantityKg * RatePerKg;
}