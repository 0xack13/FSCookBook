
let inline bresenham fill (x0, y0) (x1, y1) =
  let steep = abs(y1 - y0) > abs(x1 - x0)
  let x0, y0, x1, y1 =
    if steep then y0, x0, y1, x1 else x0, y0, x1, y1
  let x0, y0, x1, y1 =
    if x0 > x1 then x1, y1, x0, y0 else x0, y0, x1, y1
  let dx, dy = x1 - x0, abs(y1 - y0)
  let s = if y0 < y1 then 1 else -1
  let rec loop e x y =
    if x <= x1 then
      if steep then fill y x else fill x y
      if e < dy then
        loop (e-dy+dx) (x+1) (y+s)
      else
        loop (e-dy) (x+1) y
  loop (dx/2) x0 y0


#r @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\PresentationFramework.dll"
open System
open System.Windows
open System.Windows.Media.Imaging

 
[<System.STAThread>]
do
  let rand = System.Random()
  let n = 256
  let pixel = Array.create (n*n) 0uy
  let rand = System.Random().Next
  for _ in 1..100 do
    bresenham (fun x y -> pixel.[x+y*n] <- 255uy) (rand n, rand n) (rand n, rand n)
  let image = Controls.Image(Stretch=Media.Stretch.Uniform)
  let format = Media.PixelFormats.Gray8
  image.Source <-
    BitmapSource.Create(n, n, 1.0, 1.0, format, null, pixel, n)
  Window(Content=image, Title="Bresenham's line algorithm")
  |> (Application()).Run |> ignore
