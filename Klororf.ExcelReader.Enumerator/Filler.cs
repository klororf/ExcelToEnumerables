using System;
using System.Data;

namespace Klororf.ExcelReader.Enumerator
{
    public static class Filler<TSource> where TSource : class
    {
        public static String FilePath { get; private set; }
        public static void SetFilePath(string filePath) => Filler<TSource>.FilePath = filePath;
        public static Fill<TSource> GetFiller(string table)
        {
            Converter enumerator = new Converter(Filler<TSource>.FilePath);
            DataSet dataSet = enumerator.ConvertToDataSet();
            return new Fill<TSource>("SalesOrders")
                            .FromDataSet(dataSet);


        }
    }
}

