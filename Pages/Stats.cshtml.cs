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

    public void OnGet()
    {
        waiters = waiterManger.GetWaiters();
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
         return RedirectToPage("/Index");
    }
     public IActionResult OnPostClear()
    {
        waiterManger.Clear();
        return RedirectToPage("/Stats");
    }

}