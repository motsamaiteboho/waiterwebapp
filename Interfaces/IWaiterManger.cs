public interface IWaiterManger
{
    string Add( Waiter waiter, List<string> shiftDays);
    int  NumOfWaiterPerDay(string day);
    string update(Waiter waiter , List<string> newDays);
    IEnumerable<string> NamesOfWaitersPerDay(string? pShiftDay);
    IEnumerable<Waiter> GetWaiters();
    Waiter GetWaiter(string sName);
    void Clear();
    //void Update(Waiter  user);
    //void  Remove(Waiter  user);
}