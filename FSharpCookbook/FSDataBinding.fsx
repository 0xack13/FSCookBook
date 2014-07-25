//#I @"C:\Windows\Microsoft.NET\Framework\v4.0.30319"
#r "System.Windows.Forms.DataVisualization.dll"
open System
open System.Drawing
open System.Windows.Forms
open System.Windows.Forms.DataVisualization.Charting

/// Add data series of the specified chart type to a chart
let addSeries typ (chart:Chart) =
    let series = new Series(ChartType = typ)
    chart.Series.Add(series)
    series

/// Create form with chart and add the first chart series
let createChart typ =
    let chart = new Chart(Dock = DockStyle.Fill, 
                          Palette = ChartColorPalette.Pastel)
    let mainForm = new Form(Visible = true, Width = 700, Height = 500)
    let area = new ChartArea()
    area.AxisX.MajorGrid.LineColor <- Color.LightGray
    area.AxisY.MajorGrid.LineColor <- Color.LightGray
    mainForm.Controls.Add(chart)
    chart.ChartAreas.Add(area)
    chart, addSeries typ chart


let data = 
    [ for i in 0 .. 100 do
          yield 2.0 * sin (float i / 20.0) + sin (float i / 10.0) ]

let chart, series = createChart SeriesChartType.Line
series.BorderWidth <- 3
series.Points.DataBindY(data)

let valuesDoughnut = [ 1033043; 4166741; 732759; 588649; 351659; 35838  ]
let namesDougnut  = [ "Africa"; "Asia"; "Europe"; "South America";
               "North America"; "Oceania" ]

let chartDoughnut, seriesDoughnut = createChart SeriesChartType.Doughnut
seriesDoughnut.Font <- new Font("Verdana", 11.0f)
seriesDoughnut.Points.DataBindXY(namesDougnut, valuesDoughnut)


