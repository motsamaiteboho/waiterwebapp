using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
namespace WaiterWebApp.Pages;
public class NextWeekScheduleModel : PageModel
{
    private readonly IWaiterManger waiterManger;
    public NextWeekScheduleModel(IWaiterManger pWaiterManger)
    {
        waiterManger = pWaiterManger;
    }
    public IEnumerable<string> ShiftDays = CurrentWeekDays();

    [BindProperty(SupportsGet = true), Required]
    public Waiter waiter { get; set; }

    [BindProperty(SupportsGet = true)]
    public string FeedBackMessage {get; set;} = string.Empty;

    [BindProperty(SupportsGet = true)]
    public string username {get; set;} = string.Empty;

    [BindProperty]
    public List<string> SelectedShiftDays { get; set; } = new List<string>();

    [BindProperty(SupportsGet = true), Required]
    public IEnumerable<Waiter> waiters {get;set;} = Enumerable.Empty<Waiter>();

    [Route("{username}")]
    public void OnGet(string username)
    {
        this.username = username;
        waiters = waiterManger.GetWaitersWeek2();
    }
    public IActionResult OnPostAdd()
    {
        
        if(HttpContext.Session.GetString("username") != null)
        {
            var shiftdays = this.SelectedShiftDays;
        waiter.Name = username;
        string result = waiterManger.AddWeek2(waiter, shiftdays);
        if(shiftdays.Count() > 0 )
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
        return RedirectToPage("./Index");
    }

    public IActionResult OnPostUpdate()
    {
        if(HttpContext.Session.GetString("username") != null)
        {
        var shiftdays = this.SelectedShiftDays;
        waiter.Name = username;
        string result = waiterManger.AddWeek2(waiter, shiftdays);
        if(shiftdays.Count() > 0 )
        {
            if(string.IsNullOrEmpty( waiterManger.updateWeek2(waiter, shiftdays) ))
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
        return RedirectToPage("./Index");
    }

    public IActionResult OnPostBack()
    {
        HttpContext.Session.Remove("username");
        return RedirectToPage("/Schedule", new { username =  username });
    }
    private static IEnumerable<string> CurrentWeekDays()
    {
      DateTime prevMonday = DateTime.Now.AddDays( (int) DateTime.Now.DayOfWeek - 6 );
      prevMonday = prevMonday.AddDays(7);
      IEnumerable<string> dates = Enumerable.Range( 0, 7 ).Select( i => prevMonday.AddDays( i ).DayOfWeek.ToString() + " " + prevMonday.AddDays( i ).ToString("dd-MM-yyyy") );
      return dates;
    }
}
