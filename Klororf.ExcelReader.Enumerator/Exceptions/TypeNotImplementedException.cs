using System;
namespace Klororf.ExcelReader.Enumerator.Exceptions
{
    public class TypeNotImplementedException: Exception
    {
        public TypeNotImplementedException(Type type): base($"The type {type.Name} conversion is not suported, please use especific conversion")
        {
        }
    }
}

