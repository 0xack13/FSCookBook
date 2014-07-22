// Load Microsoft Chart Controls for Windows Forms
#r "System.Windows.Forms.DataVisualization.dll"

open System
open System.Windows.Forms
open System.Windows.Forms.DataVisualization.Charting
open System.Drawing

let chart = new Chart(Dock = DockStyle.Fill)
let area = new ChartArea("Main")
chart.ChartAreas.Add(area)
let mainForm = new Form(Visible = true, TopMost = true, 
                        Width = 600, Height = 400)
mainForm.Controls.Add(chart)

// Create series and add it to the chart
let seriesColumns = new Series("RandomYs")
chart.Series.Add(seriesColumns)

// Add 10 random values to the series
let rnd = new Random()
// X-axis
for i in 0 .. 20 do
    //Y-axis
    seriesColumns.Points.Add(float(rnd.Next(100))) |> ignore

// Add main title to the chart
chart.Titles.Add
    (new Title( "Chart Controls", 
                Font = new Font("Verdana", 14.0f)))

// Set gradient background of the chart area
area.BackColor <- Color.White
area.BackSecondaryColor <- Color.LightSteelBlue
area.BackGradientStyle <- GradientStyle.DiagonalRight

// Change appearance and range of axes
area.AxisY.Maximum <- 110.0
area.AxisX.MajorGrid.LineColor <- Color.LightSlateGray
area.AxisY.MajorGrid.LineColor <- Color.LightSlateGray

// Specify common properties of the series
seriesColumns.ChartType <- SeriesChartType.Column
seriesColumns.IsValueShownAsLabel <- true

// Specify custom properties of the column chart
seriesColumns.["PointWidth"] <- "0.6"
seriesColumns.["DrawingStyle"] <- "Cylinder"

// Add a second data series
let seriesLines = 
    new Series("RandomLine", IsValueShownAsLabel = false, 
               BorderWidth = 4, ChartType = SeriesChartType.Line)

// Generate random values for the series
for i in 0 .. 20 do
    seriesLines.Points.Add(float(rnd.Next(100))) |> ignore
chart.Series.Add(seriesLines)