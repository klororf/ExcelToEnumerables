using System;
using System.Linq.Expressions;
using Klororf.ExcelReader.Enumerator.Models;
using System.Reflection;
using System.Data;
using Klororf.ExcelReader.Enumerator.Exceptions;
using Klororf.ExcelReader.Enumerator.Extensions;

namespace Klororf.ExcelReader.Enumerator
{
    public class Fill<TSource>
    {
        private readonly Type type;
        private readonly string fromTable;
        private DataSet DataSet { get; set; }
        public IEnumerable<ConvertOperation> Operations { get; private set; }

        internal Fill(string fromTable)
        {
            this.type = typeof(TSource);
            this.fromTable = fromTable;
            this.Operations = Array.Empty<ConvertOperation>();
        }

        public Fill<TSource> FillFrom<TMember>(string column, Expression<Func<TSource, TMember>> lambda)
        {

            MemberExpression member = lambda.Body as MemberExpression;
            if (member == null)
                throw new NoMemberException(lambda.Body.ToString());
            PropertyInfo propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new NoMemberException(lambda.Body.ToString());
            this.Operations = this.Operations.Append(new(column, propInfo));
            return this;
        }
        public Fill<TSource> FillFrom<TMember>(string column, Expression<Func<TSource, TMember>> lambda,Func<object, object?> func)
        {

            MemberExpression member = lambda.Body as MemberExpression;
            if (member == null)
                throw new NoMemberException(lambda.Body.ToString());
            PropertyInfo propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new NoMemberException(lambda.Body.ToString());
            this.Operations = this.Operations.Append(new(column, propInfo,(Func<object?,object?>)func));
            return this;
        }

        public IEnumerable<TSource> Convert()
        {
            return Convert(this.DataSet);
        }

        private IEnumerable<TSource> Convert(DataSet ds)
        {
            IEnumerable<TSource> ret = Array.Empty<TSource>();
            if (!TableExist(ds, fromTable))
                throw new TableNotExistException(fromTable);
            DataTable dt = ds.Tables[fromTable]!;
            IEnumerable<string> colNot = ColumnsNotExists(dt, Operations.Select(x => x.ColumnName));
            if (colNot.Any())
                throw new ColumnNotExistException(colNot);
            foreach (DataRow row in dt.Rows)
            {
                TSource tsource = (TSource)Activator.CreateInstance(typeof(TSource))!;
                foreach (var op in Operations)
                {
                    object? converted = null;
                    if (op.FunctionToApply is not null)
                        converted = op.FunctionToApply(row[op.ColumnName]);
                    else
                        converted = row[op.ColumnName].CastObject(op.DestinyProperty.PropertyType);
                    op.DestinyProperty.SetValue(tsource, converted);
                }
                ret = ret.Append(tsource);

            }
            return ret;
        }
        private IEnumerable<string> ColumnsNotExists(DataTable dt, IEnumerable<string> columns)
        {
            IEnumerable<string> ret = Array.Empty<string>();
            return columns.Except(dt.Columns.Cast<DataColumn>().Select(x=>x.ColumnName)); 
        }

        private bool TableExist(DataSet ds, string table)
        {
            IEnumerable<DataTable> dts = ds.Tables.Cast<DataTable>();
            return dts.Any(x => x.TableName.Equals(table));
        }

        internal Fill<TSource> FromDataSet(DataSet dataSet)
        {
            this.DataSet = dataSet;
            return this;
        }
    }
}

