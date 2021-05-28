using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace SqlLiteKc
{
    public interface DataBase
    {
        SQLiteAsyncConnection GetConnection();
    }
}
