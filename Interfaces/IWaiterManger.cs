public interface IWaiterManger
{
    string AddWeek1( Waiter waiter, List<string> shiftDays);
    int  NumOfWaiterPerDayWeek1(string day);
    string updateWeek1(Waiter waiter , List<string> newDays);
    IEnumerable<string> NamesOfWaitersPerDayWeek1(string? pShiftDay);
    IEnumerable<Waiter> GetWaitersWeek1();
    Waiter GetWaiterWeek1(string sName);
    void ClearWeek1();
    string AddWeek2( Waiter waiter, List<string> shiftDays);
    int  NumOfWaiterPerDayWeek2(string day);
    string updateWeek2(Waiter waiter , List<string> newDays);
    IEnumerable<string> NamesOfWaitersPerDayWeek2(string? pShiftDay);
    IEnumerable<Waiter> GetWaitersWeek2();
    Waiter GetWaiterWeek2(string sName);
    void ClearWeek2();
    //void Update(Waiter  user);
    //void  Remove(Waiter  user);
}