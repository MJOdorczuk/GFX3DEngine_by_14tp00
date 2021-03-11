module KeyBinds
open System
open OpenTK
open OpenTK.Input
open WorldData


let OnScroll (e:MouseWheelEventArgs):unit=   
    WD.r <- WD.r * Math.Pow(1.1, float -e.Delta)

let OnMouseMove (e:MouseMoveEventArgs):unit=
    let x = float e.XDelta
    let y = float e.YDelta

    let capBeta beta y=
        let out = beta + y/100.

        if out > Math.PI/2.-0.01 
        then Math.PI/2.-0.01
        elif out < -Math.PI/2.+0.01 
        then -Math.PI/2.+0.01
        else out


    match WD.inputMode with
    | Default -> ()
    | Rotate -> WD.alfa <- WD.alfa + x/100.
                WD.beta <- capBeta WD.beta y
                    
let OnMouseDown (e:MouseButtonEventArgs):unit=
    match e.Button with
    | MouseButton.Right -> WD.inputMode <- Rotate
    | _ -> ()

let OnMouseUp (e:MouseButtonEventArgs):unit=
    match e.Button with
    | MouseButton.Right -> WD.inputMode <- Default
    | _ -> ()

let OnKeyDown (e:KeyboardKeyEventArgs):unit=
    match e.Key with
    | Key.S -> WD.moveV <- Vector3d(1., WD.moveV.Y, WD.moveV.Z)
    | Key.W -> WD.moveV <- Vector3d(-1., WD.moveV.Y, WD.moveV.Z)
    | Key.D -> WD.moveV <- Vector3d(WD.moveV.X, 1., WD.moveV.Z)
    | Key.A -> WD.moveV <- Vector3d(WD.moveV.X, -1., WD.moveV.Z)
    | Key.LShift -> WD.moveV <- Vector3d(WD.moveV.X, WD.moveV.Y, 1.)
    | Key.LControl -> WD.moveV <- Vector3d(WD.moveV.X, WD.moveV.Y, -1.)
    | _ -> ()

let OnKeyUp (e:KeyboardKeyEventArgs):unit=
    match e.Key with
    | Key.S -> WD.moveV <- Vector3d(0., WD.moveV.Y, WD.moveV.Z)
    | Key.W -> WD.moveV <- Vector3d(0., WD.moveV.Y, WD.moveV.Z)
    | Key.D -> WD.moveV <- Vector3d(WD.moveV.X, 0., WD.moveV.Z)
    | Key.A -> WD.moveV <- Vector3d(WD.moveV.X, 0., WD.moveV.Z)
    | Key.LShift -> WD.moveV <- Vector3d(WD.moveV.X, WD.moveV.Y, 0.)
    | Key.LControl -> WD.moveV <- Vector3d(WD.moveV.X, WD.moveV.Y, 0.)
    | _ -> ()