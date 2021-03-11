module LightSource
open System
open OpenTK
open OpenTK.Graphics.OpenGL

// Interface declaration:
type ILightSource =
    abstract shineOn : Vector3d -> Vector3d -> Color -> Color
    abstract source :  Vector3d
    abstract MoveTo : Vector3d -> unit


type PointLight (source:Vector3d, intensivity:float, colorL:Color)=
    
    let mutable source = source
    interface ILightSource with
        member __.shineOn(point: Vector3d) (normalV:Vector3d) (color:Color): Color = 
            let scale = max ((intensivity*Vector3d.Dot(point-source, normalV))/(point-source).LengthSquared) 0.
            //let R = int ((scale * float color.R)*float colorL.R)/255
            //let G = int ((scale * float color.G)*float colorL.G)/255
            //let B = int ((scale * float color.B)*float colorL.B)/255
            let R = int (scale * float color.R)
            let G = int (scale * float color.G)
            let B = int (scale * float color.B)
            Color.FromArgb(int color.A, R, G, B)
        member __.source = 
            source
        member __.MoveTo(pos:Vector3d)=
               source<-pos