module WorldData
open OpenTK
open System


type InputMode =
    | Rotate
    | Default 

type WD() = 
    static member Width = 900 
    static member Height = 900 

    static member val focus = Vector3d(0., 0., 0.) with get, set

    static member val inputMode = Default with get, set

    static member val r = 1. with get, set
    static member val alfa = Math.PI with get, set
    static member val beta = 0. with get, set
    static member val moveV = Vector3d(0., 0., 0.) with get, set       
    static member val lightV = Vector3d(-1.,-1.,-1.).Normalized() with get, set