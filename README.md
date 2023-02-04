# ExcelToEnumerables

## Project for fill IEnumerables with a excel files

## Usage
```c#
//Get the excel Path
string filePath = "/Users/danielcarnicero/Downloads/SampleData.xlsx";
//Create a Fill Object with the specified type and configure the fill
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
        .ConvertWithExcel(filePath);
 ```
