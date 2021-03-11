module Face
open System
open OpenTK
open OpenTK.Graphics.OpenGL
open OpenTK.Input
open WorldData

type Face (a:Vector3d, b:Vector3d, c:Vector3d, color:Color) =

    let normalV = (Vector3d.Cross(b-a, c-a)).Normalized()

    member public __.A = a 
    member public __.B = b 
    member public __.C = c 
    member public __.Color = color 

    member public __.NormalV = normalV

    member public __.Draw =
        GL.PolygonMode (MaterialFace.Front, PolygonMode.Fill)
        GL.CullFace CullFaceMode.Front 
        GL.Enable EnableCap.CullFace
        GL.Enable EnableCap.DepthTest
        GL.Begin PrimitiveType.TriangleFan

        let scale = Math.Sqrt(Vector3d.Dot(WD.lightV, normalV))

        GL.Color3 (Color.FromArgb(int color.A,int (scale * float color.R), int (scale * float color.G),int (scale * float color.B)))

        GL.Vertex3 a
        GL.Vertex3 b
        GL.Vertex3 c

        GL.End()

    member public __.Flipped = 
        Face(a, c, b, color)