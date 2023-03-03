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
            string CREATE_WAITER_TABLE = @"create table if not exists waiterweek1 (
	            Id integer primary key AUTOINCREMENT,
	            Name text,
	            ShiftDay text
            );";
            connection.Execute(CREATE_WAITER_TABLE);
             string CREATE_WAITER_TABLE2 = @"create table if not exists waiterweek2 (
	            Id integer primary key AUTOINCREMENT,
	            Name text,
	            ShiftDay text
            );";
            connection.Execute(CREATE_WAITER_TABLE2);
        }
    }
    public IEnumerable<Waiter> GetWaitersWeek1()
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            var waiters = connection.Query<Waiter>(@"select * from waiterweek1");
            return waiters;
        }
    }
public IEnumerable<Waiter> GetWaitersWeek2()
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            var waiters = connection.Query<Waiter>(@"select * from waiterweek2");
            return waiters;
        }
    }

    public Waiter GetWaiterWeek1(string Name)
    {
        var template = new Waiter { Name = Name };
        var parameters = new DynamicParameters(template);
        var sql = @"select * from waiterweek1 where Name = @Name";
        using (var connection = new SqliteConnection(_connectionString))
        {
            var waiter = connection.QueryFirstOrDefault<Waiter>(sql, parameters);
            return waiter;
        }
    }
    public Waiter GetWaiterWeek2(string Name)
    {
        var template = new Waiter { Name = Name };
        var parameters = new DynamicParameters(template);
        var sql = @"select * from waiterweek2 where Name = @Name";
        using (var connection = new SqliteConnection(_connectionString))
        {
            var waiter = connection.QueryFirstOrDefault<Waiter>(sql, parameters);
            return waiter;
        }
    }

     public string AddWeek1( Waiter waiter, List<string> shiftDays)
    {
        string days = "";
        using (var connection = new SqliteConnection(_connectionString))
        {
            foreach(var day in shiftDays)
            {
                int waitersperDay = NumOfWaiterPerDayWeek1(day);
                
                if(waitersperDay < 3 )
                {
                    var insertsql = @" insert into  waiterweek1 (Name, ShiftDay)
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
                    days += day + "," ;
                }
            }
            return days;
        }
       
    }
    public string AddWeek2( Waiter waiter, List<string> shiftDays)
    {
        string days = "";
        using (var connection = new SqliteConnection(_connectionString))
        {
            foreach(var day in shiftDays)
            {
                int waitersperDay = NumOfWaiterPerDayWeek2(day);
                
                if(waitersperDay < 3 )
                {
                    var insertsql = @" insert into  waiterweek2 (Name, ShiftDay)
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
                    days += day + "," ;
                }
            }
            return days;
        }
       
    }

    public int NumOfWaiterPerDayWeek1(string? ShiftDay)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            var template = new { ShiftDay = ShiftDay};
            var parameters = new DynamicParameters(template);

            var sql = @"select ShiftDay from waiterweek1 where ShiftDay = @ShiftDay";

            var waiters = connection.Query<string>(sql, parameters);

            return waiters.Count();
        }
    }
    public int NumOfWaiterPerDayWeek2(string? ShiftDay)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            var template = new { ShiftDay = ShiftDay};
            var parameters = new DynamicParameters(template);

            var sql = @"select ShiftDay from waiterweek2 where ShiftDay = @ShiftDay";

            var waiters = connection.Query<string>(sql, parameters);

            return waiters.Count();
        }
    }

    public string updateWeek1(Waiter waiter , List<string> newDays)
    {
        string days = "";
        using (var connection = new SqliteConnection(_connectionString))
        {
            var template = new { Name = waiter.Name};
            var parameters = new DynamicParameters(template);
            var deletesql = @"DELETE FROM waiterweek1 WHERE Name = @Name;";
            connection.Execute(deletesql, parameters);

            foreach(var day in newDays)
            {
                int waitersperDay = NumOfWaiterPerDayWeek1(day);
                
                if(waitersperDay < 3 )
                {
                var insertsql = @" insert into  waiterweek1 (Name, ShiftDay)
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
                   days += day + "," ;
                }
            }
        }
        return days;
    }
    public string updateWeek2(Waiter waiter , List<string> newDays)
    {
        string days = "";
        using (var connection = new SqliteConnection(_connectionString))
        {
            var template = new { Name = waiter.Name};
            var parameters = new DynamicParameters(template);
            var deletesql = @"DELETE FROM waiterweek2 WHERE Name = @Name;";
            connection.Execute(deletesql, parameters);

            foreach(var day in newDays)
            {
                int waitersperDay = NumOfWaiterPerDayWeek2(day);
                
                if(waitersperDay < 3 )
                {
                var insertsql = @" insert into  waiterweek2 (Name, ShiftDay)
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
                   days += day + "," ;
                }
            }
        }
        return days;
    }

    public IEnumerable<string> NamesOfWaitersPerDayWeek1(string? pShiftDay)
    {
        var template = new { ShiftDay = pShiftDay };
        var parameters = new DynamicParameters(template);
        var sql = @"select Name from waiterweek1 where ShiftDay = @ShiftDay";
        using (var connection = new SqliteConnection(_connectionString))
        {
            var waiters= connection.Query<string>(sql, parameters);
            return waiters;
        }
    }
    
    public IEnumerable<string> NamesOfWaitersPerDayWeek2(string? pShiftDay)
    {
        var template = new { ShiftDay = pShiftDay };
        var parameters = new DynamicParameters(template);
        var sql = @"select Name from waiterweek2 where ShiftDay = @ShiftDay";
        using (var connection = new SqliteConnection(_connectionString))
        {
            var waiters= connection.Query<string>(sql, parameters);
            return waiters;
        }
    }
     public void ClearWeek1()
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            var sql = @"delete from waiterweek1;";
            connection.Execute(sql);
            
        }
    }
    public void ClearWeek2()
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
         
            var sql2 = @"delete from waiterweek2;";
            connection.Execute(sql2);
        }
    }

}