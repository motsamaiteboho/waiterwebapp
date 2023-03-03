using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
namespace WaiterWebApp.Pages;

public class StatsWeekModel : PageModel
{
    private readonly IWaiterManger waiterManger;
    public StatsWeekModel( IWaiterManger pWaiterManger)
    {
        waiterManger = pWaiterManger;
    }

    [BindProperty(SupportsGet = true), Required]
    public IEnumerable<Waiter> waiters {get;set;} = Enumerable.Empty<Waiter>();

    [BindProperty(SupportsGet = true)]
    public string username {get; set;} = string.Empty;

    [Route("{username}")]
    public void OnGet(string username)
    {
        this.username = username;
        waiters = waiterManger.GetWaitersWeek2();
    }

    public IActionResult OnPost()
    {
        if (ModelState.IsValid == false)
        {
            return Page();
        }
        return RedirectToPage("/Index");
    }
    
    public IActionResult OnPostBack()
    {
        HttpContext.Session.Clear();
        return RedirectToPage("/Stats", new { username =  username });
    }
     public IActionResult OnPostClear()
    {
        waiterManger.ClearWeek2();
        return RedirectToPage("/StatsWeek");
    }

}