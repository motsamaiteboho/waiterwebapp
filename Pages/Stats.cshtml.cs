using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
namespace WaiterWebApp.Pages;

public class StatsModel : PageModel
{
    private readonly IWaiterManger waiterManger;
    public StatsModel( IWaiterManger pWaiterManger)
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
        waiters = waiterManger.GetWaitersWeek1();
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
         return RedirectToPage("/Index");
    }
    public IActionResult OnPostNext()
    {
        //Console.WriteLine("tee");
        return RedirectToPage("/StatsWeek");
        //return RedirectToPage("/StatsWeek", new { username =  username });
    }
     public IActionResult OnPostClear()
    {
        waiterManger.ClearWeek1();
        return RedirectToPage("/Stats");
    }

}