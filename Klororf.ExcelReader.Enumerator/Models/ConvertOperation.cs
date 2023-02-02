using System;
using System.Reflection;

namespace Klororf.ExcelReader.Enumerator.Models
{
    public class ConvertOperation
    {
        public string ColumnName { get; set; }
        public PropertyInfo DestinyProperty { get; set; }
        public Func<object,object?>? FunctionToApply { get; set; }

        public ConvertOperation(string columnName, PropertyInfo destiny, Func<object, object?>? function)
        {
            ColumnName = columnName;
            DestinyProperty = destiny;
            FunctionToApply = function;
        }
        public ConvertOperation(string columnName, PropertyInfo destiny) : this(columnName, destiny, null)
        { }
    }
}

