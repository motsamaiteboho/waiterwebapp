using System.ComponentModel.DataAnnotations;
public class Waiter
{
    public int Id {get; set;}
    [Required]
    public string? Name {get; set;}
    public string? ShiftDay {get; set;}
    public List<string> ? ShiftDays {get; set;}
}