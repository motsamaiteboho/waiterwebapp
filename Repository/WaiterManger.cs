using Microsoft.Data.Sqlite;
using Dapper;
public class WaiterManger: IWaiterManger
{
    private string _connectionString;
    public WaiterManger(string connectionString)
    {
        _connectionString = connectionString;

        using (var connection = new SqliteConnection(_connectionString))
        {
            string CREATE_WAITER_TABLE = @"create table if not exists waiter (
	            Id integer primary key AUTOINCREMENT,
	            Name text,
	            ShiftDay text
            );";
            connection.Execute(CREATE_WAITER_TABLE);
        }
    }

     public bool Add( Waiter waiter, List<string> shiftDays)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            foreach(var day in shiftDays)
            {
                int waitersperDay = NumOfWaiterPerDay(day);
                
                if(waitersperDay < 3 )
                {
                    var insertsql = @" insert into  waiter (Name, ShiftDay)
	                    values (@Name, @ShiftDay);";

                    var insertparameters = new Waiter()
                    {
                        Name = waiter.Name,
                        ShiftDay = day
                    };
                    connection.Execute(insertsql, insertparameters);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
       
    }

    public int NumOfWaiterPerDay(string? ShiftDay)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            var template = new { ShiftDay = ShiftDay};
            var parameters = new DynamicParameters(template);

            var sql = @"select ShiftDay from waiter where ShiftDay = @ShiftDay";

            var waiters = connection.Query<string>(sql, parameters);

            return waiters.Count();
        }
    }

    public bool update(Waiter waiter , List<string> newDays)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            var template = new { Name = waiter.Name};
            var parameters = new DynamicParameters(template);
            var deletesql = @"DELETE FROM waiter WHERE Name = @Name;";
            connection.Execute(deletesql, parameters);

            foreach(var day in newDays)
            {
                int waitersperDay = NumOfWaiterPerDay(day);
                
                if(waitersperDay < 3 )
                {
                var insertsql = @" insert into  waiter (Name, ShiftDay)
	                    values (@Name, @ShiftDay);";

                var insertparameters = new Waiter()
                {
                    Name = waiter.Name,
                    ShiftDay = day
                 };
                connection.Execute(insertsql, insertparameters);
                }
                 else
                {
                    return false;
                }
            }
        }
        return true;
    }

    public IEnumerable<string> NamesOfWaitersPerDay(string? pShiftDay)
    {
        var template = new { ShiftDay = pShiftDay };
        var parameters = new DynamicParameters(template);
        var sql = @"select Name from waiter where ShiftDay = @ShiftDay";
        using (var connection = new SqliteConnection(_connectionString))
        {
            var waiters= connection.Query<string>(sql, parameters);
            return waiters;
        }
    }

}