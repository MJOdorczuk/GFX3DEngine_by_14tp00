module Face
open System
open OpenTK
open OpenTK.Graphics.OpenGL
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

        GL.Color3 (WD.lights.Head.shineOn a normalV color)
        GL.Vertex3 a

        GL.Color3 (WD.lights.Head.shineOn b normalV color)
        GL.Vertex3 b

        GL.Color3 (WD.lights.Head.shineOn c normalV color)
        GL.Vertex3 c

        GL.End()

    member public __.DrawUnshaded =
        GL.PolygonMode (MaterialFace.Front, PolygonMode.Fill)
        GL.CullFace CullFaceMode.Front 
        GL.Enable EnableCap.CullFace
        GL.Enable EnableCap.DepthTest
        GL.Begin PrimitiveType.TriangleFan

        GL.Color3 color
        GL.Vertex3 a
        GL.Vertex3 b
        GL.Vertex3 c

        GL.End()
    member public __.Flipped = 
        Face(a, c, b, color)