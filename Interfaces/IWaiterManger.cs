public interface IWaiterManger
{
    bool Add( Waiter waiter, List<string> shiftDays);
    int  NumOfWaiterPerDay(string day);
    bool update(Waiter waiter , List<string> newDays);
    IEnumerable<string> NamesOfWaitersPerDay(string? pShiftDay);
    //void Update(Waiter  user);
    //void  Remove(Waiter  user);
}