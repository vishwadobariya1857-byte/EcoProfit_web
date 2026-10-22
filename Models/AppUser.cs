using System.ComponentModel.DataAnnotations;
namespace EcoProfit.Models;
public class AppUser
{
 public int Id {get;set;}
 [Required,StringLength(80)] public string FullName {get;set;} = "";
 [Required,EmailAddress,StringLength(160)] public string Email {get;set;} = "";
 [Required] public string PasswordHash {get;set;} = "";
 public List<WasteEntry> WasteEntries {get;set;} = new();
}