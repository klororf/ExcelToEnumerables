
// See https://aka.ms/new-console-template for more information
using Klororf.ExcelReader.Enumerator;

Console.WriteLine("Hello, World!");
string filePath = "/Users/danielcarnicero/Downloads/SampleData.xlsx";
try
{
    var enumerator = new Klororf.ExcelReader.Enumerator.Converter(filePath);


    var c = enumerator.ConvertToDataSet();

    var x = new Fill<SalesOrders>("SalesOrders")
        .FillFrom("Region", (x) => x.Region)
        .FillFrom("Rep", x=>x.Rep)
        .FillFrom("Units", x=>x.Units)
        .FillFrom("Unit Cost", x=>x.UnitCost)
        .FillFrom("Total",
                  x => x.Total,
                  (x)=> {
                      var c = int.Parse(x.ToString().Substring(0,1));
                      return c; })
        .Convert(c);
    
}
catch (Exception ex)
{

    string error = ex.Message;
}
static int cs(object x)
{
    return 0;
}


public class SalesOrders : Klororf.ExcelReader.Enumerator.Models.Sheet
{
    public string Region { get; set; }
    public string Rep { get; set; }
    public int Units {get;set;}
    public float UnitCost { get; set; }
    public float Total {get; set; }
}