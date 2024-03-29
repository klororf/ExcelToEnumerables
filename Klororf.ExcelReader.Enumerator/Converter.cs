﻿using System;
using System.Data;
using System.Reflection;
using ExcelDataReader;

namespace Klororf.ExcelReader.Enumerator
{
    internal class Converter
    {
        private readonly string Path;
        internal Converter(string path)
        {
            this.Path = path;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        internal DataSet ConvertToDataSet()
        {
            DataSet ds = null;
            using (var stream = File.Open(Path, FileMode.Open, FileAccess.Read))
            {
                ExcelReaderConfiguration erc = new();
                
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    ExcelDataSetConfiguration ed = new ExcelDataSetConfiguration();
                    ed.ConfigureDataTable= (x) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    };
                    ds = reader.AsDataSet(ed);
                }
            }

            return ds;
        }

       
    }
}

