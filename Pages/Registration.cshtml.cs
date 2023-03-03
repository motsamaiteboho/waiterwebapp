using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace WaiterWebApp.Pages;

public class RegistrationModel : PageModel
{
    private readonly IUserManger userManger;
    public RegistrationModel(IUserManger pUserManger)
    {
        userManger = pUserManger;
    }

    [BindProperty(SupportsGet = true), Required]
    public RegstrationDetails user { get; set; }

    [BindProperty(SupportsGet = true)]
    public string FeedBackMessage {get; set;} = string.Empty;

    public void OnGet()
    {

    }
    public IActionResult OnPostReg()
    {
        if(!string.IsNullOrEmpty(user.username) && !string.IsNullOrEmpty(user.password) && !string.IsNullOrEmpty(user.Repassword))
        {
            if(user.password == user.Repassword)
            {
                user newUser = new user(){ username = user.username, password = user.password };
                if(userManger.AddUser(newUser))
                {
                    return RedirectToPage("/Index");
                }
            }
            FeedBackMessage = "passwords must be the same";
            return Page();
        }
        else
        {
            FeedBackMessage = "all fields are  required";
            return Page();
        }
    }
}
public class RegstrationDetails
{
    public string? username {get; set;}
    public string? password {get; set;}
    public string? Repassword {get; set;}
}