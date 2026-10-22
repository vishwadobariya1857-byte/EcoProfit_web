using System.ComponentModel.DataAnnotations;
namespace EcoProfit.Models;
public class RegisterForm
{
 [Required,StringLength(80),Display(Name="Full name")] public string FullName {get;set;} = "";
 [Required,EmailAddress] public string Email {get;set;} = "";
 [Required,MinLength(6),DataType(DataType.Password)] public string Password {get;set;} = "";
 [Compare(nameof(Password)),DataType(DataType.Password),Display(Name="Confirm password")] public string ConfirmPassword {get;set;} = "";
}
public class LoginForm
{
 [Required,EmailAddress] public string Email {get;set;} = "";
 [Required,DataType(DataType.Password)] public string Password {get;set;} = "";
}