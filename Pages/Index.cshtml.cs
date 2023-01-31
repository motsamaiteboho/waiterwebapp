using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
namespace WaiterWebApp.Pages;

public class IndexModel : PageModel
{
    
    private readonly IWaiterManger waiterManger;
    public IndexModel(IWaiterManger pWaiterManger)
    {
        waiterManger = pWaiterManger;
    }
    public string[] ShiftDays = new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

    [BindProperty(SupportsGet = true), Required]
    public Waiter waiter { get; set; }

    [BindProperty]
    public List<string> SelectedShiftDays { get; set; } = new List<string>();

    public void OnGet()
    {

    }
    public IActionResult OnPostAdd()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        var shiftdays = this.SelectedShiftDays;
        if(shiftdays.Count() > 0)
        {
            waiterManger.Add(waiter, shiftdays);
        }
        return RedirectToPage("/Index");
    }

    public IActionResult OnPostUpdate()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        var shiftdays = this.SelectedShiftDays;
        if(shiftdays.Count() > 0)
        {
            waiterManger.update(waiter, shiftdays);
        };

        return RedirectToPage("/Index");
    }

    public IActionResult OnPostStats(Waiter waiter)
    {
        Console.WriteLine(waiter.Name);
        Console.WriteLine("Stats");
        return RedirectToPage("/Index");
    }
}
