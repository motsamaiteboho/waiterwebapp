using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace WaiterWebApp.Pages;

public class IndexModel : PageModel
{
    private readonly IUserManger userManger;
    public IndexModel(IUserManger pUserManger)
    {
        userManger = pUserManger;
    }

    [BindProperty(SupportsGet = true), Required]
    public user user { get; set; }

    [BindProperty(SupportsGet = true)]
    public string FeedBackMessage {get; set;} = string.Empty;

    public void OnGet()
    {

    }
    public IActionResult OnPostSignIn()
    {
        //if (ModelState.IsValid)
        //{
        //    return Page();
        //}
        //Console.WriteLine(HttpContext.Session.GetString(user.username));

        if(!string.IsNullOrEmpty(user.username) && !string.IsNullOrEmpty(user.password))
        {
          HttpContext.Session.SetString("username", user.username);
          HttpContext.Session.SetInt32("counter",0);
          if(userManger.GetUsers().ToList().FirstOrDefault(o => o.username == user.username) != null)
          {
            user existuser = userManger.GetUser(user.username);

           if(user.username == "Teboho")
           { 
            if(existuser.password == user.password)
            {
               return RedirectToPage("/Stats", new { username =  user.username });
                //return RedirectToPage("/StatsWeek", new { username =  user.username });
            }
            else
            {
                FeedBackMessage = "incorrect password";
                return Page();
            }
            }
            if(existuser.password == user.password)
            {
                if(existuser != null)
                {
                    return RedirectToPage("/Schedule", new { username =  user.username });
                }
            }
            else
            {
                FeedBackMessage = "incorrect password";
                return Page();
            }
        }
        else
        {
            return RedirectToPage("/Registration");
        }
        }
        FeedBackMessage = "all fields are required";
        return Page();
    }

    public IActionResult OnPostReg()
    {
        return RedirectToPage("/Registration");
    }

    
}
