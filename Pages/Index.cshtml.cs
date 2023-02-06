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

    [BindProperty(SupportsGet = true)]
    public string FeedBackMessage {get; set;} = string.Empty;

    [BindProperty]
    public List<string> SelectedShiftDays { get; set; } = new List<string>();

    public void OnGet()
    {

    }
    public IActionResult OnPostAdd()
    {
        var shiftdays = this.SelectedShiftDays;
        string result = waiterManger.Add(waiter, shiftdays);
        if(shiftdays.Count() > 0 && shiftdays.Count() <= 3)
        {
            if( string.IsNullOrEmpty(result))
            {
                FeedBackMessage = "successfully added ";
                return Page();
            }
        }
        else
        {
            FeedBackMessage = "select atleast 1 working day";
            return Page();
        }
        FeedBackMessage = result + " is/are filled";
        return Page();
    }

    public IActionResult OnPostUpdate()
    {
        var shiftdays = this.SelectedShiftDays;
        string result = waiterManger.Add(waiter, shiftdays);
        if(shiftdays.Count() > 0 && shiftdays.Count() <= 3)
        {
            if(string.IsNullOrEmpty( waiterManger.update(waiter, shiftdays) ))
            {
                FeedBackMessage = "successfully updated";
                return Page();
            }
        }
        else
        {
             FeedBackMessage = "select atleast 1 working day";
             return Page();
        }
        FeedBackMessage = result + " is/are filled";
        return Page();
    }

    public IActionResult OnPostStats(Waiter waiter)
    {
        return RedirectToPage("/Stats");
    }
}
