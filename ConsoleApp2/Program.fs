// Learn more about F# at http://fsharp.org
open OpenTK
open OpenTK.Graphics.OpenGL
open KeyBinds
open WorldData
open Graphics
open System

let mutable t = 0.;

let ClearFrame window=
    GL.Enable EnableCap.Blend
    GL.BlendFunc(0, BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusDstAlpha)
    Color.Black
       |> GL.ClearColor
    ClearBufferMask.ColorBufferBit
       |> GL.Clear

let UpdateCamera() = 
    GL.MatrixMode MatrixMode.Projection

    GL.LoadIdentity()

    let c = 1.
    
    GL.Frustum (c,-c,c,-c,c,-c)
    Matrix4d.CreatePerspectiveFieldOfView((float)Math.PI * (70./180.), 1., 0.2, 256.0)
    |> ref
    |> GL.LoadMatrix

    GL.MatrixMode MatrixMode.Modelview 
    Matrix4d.LookAt(WD.focus+Vector3d(cos WD.alfa * -WD.r * cos WD.beta,  cos WD.beta * WD.r * sin WD.alfa, sin WD.beta * WD.r), WD.focus, Vector3d.UnitZ)
    |> ref
    |> GL.LoadMatrix 

let UpdateFocus (d:float) = 
    let x = d*(cos WD.alfa)*WD.moveV.X-d*(sin WD.alfa)*WD.moveV.Y+WD.focus.X
    let y = d*(sin WD.alfa)*WD.moveV.X+d*(cos WD.alfa)*WD.moveV.Y+WD.focus.Y
    let z = d*WD.moveV.Z+WD.focus.Z

    WD.focus <- Vector3d(x,y,z)

let OnRenFrame (window:GameWindow) (e:FrameEventArgs)=
    GL.Clear (ClearBufferMask.ColorBufferBit ||| ClearBufferMask.DepthBufferBit); 
    
    GFX.DrawLine (Vector3d(1., 0., 0.)) (Vector3d(0., 0., 0.)) Color.Red 2.
    GFX.DrawLine (Vector3d(0., 1., 0.)) (Vector3d(0., 0., 0.)) Color.Green 2.
    GFX.DrawLine (Vector3d(0., 0., 1.)) (Vector3d(0., 0., 0.)) Color.Blue 2.

    t<-t+e.Time

    GFX.DrawCube Vector3d.Zero 0.1 Color.Wheat
    GFX.DrawPlane (Vector3d(0.,0.,1.)) (Vector3d(0.,0.,-0.05)) Color.Yellow

    WD.lightV <- Vector3d(cos t,sin t,-1.).Normalized()

    GFX.DrawLine Vector3d.Zero -WD.lightV Color.Yellow 2.

    GL.Flush()
    UpdateCamera()
    UpdateFocus 0.001
    window.SwapBuffers()

[<EntryPoint>]
let main argv =
    let window = new GameWindow(WD.Width,WD.Height, Graphics.GraphicsMode.Default, "abc", GameWindowFlags.FixedWindow)
    window.RenderFrame.Add (OnRenFrame window) 
    window.MouseWheel.Add OnScroll
    window.MouseMove.Add OnMouseMove
    window.MouseDown.Add OnMouseDown
    window.MouseUp.Add OnMouseUp 
    window.KeyDown.Add OnKeyDown
    window.KeyUp.Add OnKeyUp

    window.Run()
    0 // return an integer exit code