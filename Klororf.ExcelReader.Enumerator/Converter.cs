using System;
using System.Data;
using System.Reflection;
using ExcelDataReader;

namespace Klororf.ExcelReader.Enumerator
{
    public class Converter
    {
        private readonly string Path;
        public Converter(string path)
        {
            this.Path = path;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        public DataSet ConvertToDataSet()
        {
            DataSet ds = null;
            using (var stream = File.Open(Path, FileMode.Open, FileAccess.Read))
            {
                ExcelReaderConfiguration erc = new();
                
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                   
                    // Choose one of either 1 or 2:

                    // 1. Use the reader methods
                    do
                    {
                        while (reader.Read())
                        {
                            // reader.GetDouble(0);
                        }
                    } while (reader.NextResult());

                    // 2. Use the AsDataSet extension method
                    ExcelDataSetConfiguration ed = new ExcelDataSetConfiguration();
                    ed.ConfigureDataTable= (x) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    };
                    ds = reader.AsDataSet(ed);

                    // The result of each spreadsheet is in result.Tables
                }
            }

            return ds;
        }

       
    }
}

