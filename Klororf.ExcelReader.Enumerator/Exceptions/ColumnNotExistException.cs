using System;
namespace Klororf.ExcelReader.Enumerator.Exceptions
{
    public class ColumnNotExistException: Exception
    {
        public ColumnNotExistException(IEnumerable<string> cols): base($"The columns {string.Join(",",cols)} not exist in the table")
        {
        }
    }
}

