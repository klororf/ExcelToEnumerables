using System;
namespace Klororf.ExcelReader.Enumerator.Exceptions
{
    public class NoMemberException : ArgumentException
    {
        public NoMemberException(string param) : base($"Cannot get Propery {param}")
        {
        }
    }
}

