namespace WalletTracker.Views;

public partial class ReportsPage : ContentPage
{
	public ReportsPage()
	{
		InitializeComponent();

        // var data = new
        // {
        //     cols = new List<object>
        //         {
        //             new { id = "", label = "Year", pattern = "", type = "string" },
        //             new { id = "", label = "Sales", pattern = "", type = "number" },
        //             new { id = "", label = "Expenses", pattern = "", type = "number" }
        //         },
        //     rows = new List<object>
        //         {
        //             new { c = new List<object> { new { v = "2017" }, new { v = 1000 }, new { v = 400 } } },
        //             new { c = new List<object> { new { v = "2018" }, new { v = 1170 }, new { v = 460 } } },
        //             new { c = new List<object> { new { v = "2019" }, new { v = 660 }, new { v = 1120 } } },
        //             new { c = new List<object> { new { v = "2020" }, new { v = 1030 }, new { v = 540 } } }
        //         }
        // };

        // var options = new
        // {
        //     title = "Company Performance",
        //     curveType = "function",
        //     legend = new { position = "bottom" },
        //     backgroundColor = "#f5f5f5",
        //     colors = new[] { "#2196F3", "#FF5722" },
        //     fontSize = 12,
        //     fontName = "Arial",
        //     lineWidth = 3,
        //     pointSize = 5,
        //     hAxis = new
        //     {
        //         title = "Amount",
        //         minValue = 0,
        //         maxValue = 1500,
        //         textStyle = new { fontSize = 10, color = "#333" },
        //         titleTextStyle = new { fontSize = 12, color = "#666" },
        //         gridlines = new { count = 6 }
        //     },
        //     vAxis = new
        //     {
        //         title = "Year",
        //         minValue = 0,
        //         textStyle = new { fontSize = 10, color = "#333" },
        //         titleTextStyle = new { fontSize = 12, color = "#666" }
        //     },
        //     chartArea = new
        //     {
        //         width = "80%",
        //         height = "70%"
        //     },
        //     tooltip = new
        //     {
        //         isHtml = true,
        //         showColorCode = true
        //     },
        //     animation = new
        //     {
        //         duration = 1000,
        //         easing = "out",
        //         startup = true
        //     }
        // };


        // MyGoogleChartLine.Data = data;
        // MyGoogleChartLine.ChartType = "BarChart";
        // MyGoogleChartLine.Options = options;

        // var data2 = new
        // {
        //     cols = new List<object>
        //     {
        //         new { id = "", label = "Task", pattern = "", type = "string" },
        //         new { id = "", label = "Hours per Day", pattern = "", type = "number" }
        //     },
        //     rows = new List<object>
        //     {
        //         new { c = new List<object> { new { v = "Work" }, new { v = 11 } } },
        //         new { c = new List<object> { new { v = "Eat" }, new { v = 2 } } },
        //         new { c = new List<object> { new { v = "Commute" }, new { v = 2 } } },
        //         new { c = new List<object> { new { v = "Watch TV" }, new { v = 2 } } },
        //         new { c = new List<object> { new { v = "Sleep" }, new { v = 7 } } }
        //     }
        // };

        // var options2 = new
        // {
        //     title = "My Daily Activities",
        //     pieHole = 0.4,
        //     animation = "easing",
        // };

        // MyGoogleChartPie.Data = data2;
        // MyGoogleChartPie.ChartType = "PieChart";
        // MyGoogleChartPie.Options = options2;
        // var options3 = new
        // {
        //     title = "My Daily Activities",
        //     orientation = "vertical"
        // };

        // MyGoogleChartBar.Data = data2;
        // MyGoogleChartBar.ChartType = "BarChart";
        // MyGoogleChartBar.Options = options3;
    }
}
