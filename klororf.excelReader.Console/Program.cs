
using Klororf.ExcelReader.Enumerator;
string filePath = "/Users/test/Downloads/SampleData.xlsx";
try
{
    Filler<SalesOrders>.SetFilePath(filePath);
    var x = Filler<SalesOrders>.GetFiller(nameof(SalesOrders))
        .FillFrom("Region", (x) => x.Region)
        .FillFrom("Rep", x=>x.Rep)
        .FillFrom("Units", x=>x.Units)
        .FillFrom("Unit Cost", x=>x.UnitCost)
        .FillFrom("Total",
                  x => x.Total,
                  (x)=> {
                      var c = int.Parse(x.ToString().Substring(0,1));
                      return c; })
        .Convert();
    ;
}
catch (Exception ex)
{

    string error = ex.Message;
}
public class SalesOrders
{
    public string Region { get; set; }
    public string Rep { get; set; }
    public int Units {get;set;}
    public float UnitCost { get; set; }
    public float Total {get; set; }
}