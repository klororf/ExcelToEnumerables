using System;
namespace Klororf.ExcelReader.Enumerator.Exceptions
{
    public class TableNotExistException: Exception
    {
        public TableNotExistException(string table): base($"The table {table} not exist in File")
        {
        }
    }
}

