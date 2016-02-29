namespace SphereMotion
open UnityEngine

type SphereMotion() =
    inherit MonoBehaviour()

    let mutable offset:Vector3 = new Vector3();
    let mutable pressed = false
    let mutable sphereShouldMove = false

    let shouldSphereMove (mousePos:Vector3) (transformPos:Vector3) : bool =
        mousePos
        |> ( fun pos -> Camera.main.ScreenPointToRay pos )
        |> ( fun ray -> Vector3.Cross(ray.direction, (transformPos - ray.origin)) )
        |> ( fun crossProduct -> crossProduct.magnitude )
        |> ( fun mag -> mag < 2.5f )

    let sphereWorldPoint (mousePos:Vector3) (transformPos:Vector3) (offset:Vector3) : Vector3 =
        let tsp = Camera.main.WorldToScreenPoint(transformPos)

        mousePos
        |> ( fun mp -> mp - offset )
        |> ( fun offsetted -> new Vector3(offsetted.x, offsetted.y, tsp.z) )
        |> ( fun newPos -> Camera.main.ScreenToWorldPoint(newPos) )

    member this.Update() =
        if Input.GetMouseButtonDown(0) then
            pressed          <- true
            sphereShouldMove <- shouldSphereMove Input.mousePosition this.transform.position
            offset           <- Input.mousePosition - Camera.main.WorldToScreenPoint(this.transform.position)
        else ()

        if Input.GetMouseButtonUp(0) then
            pressed <- false
        else ()

        match pressed with
        | true ->
            match sphereShouldMove with 
            | true -> this.transform.position <- sphereWorldPoint Input.mousePosition this.transform.position offset
            | false ->
                let h:float32 = 2.0f * Input.GetAxis("Mouse X")
                Camera.main.transform.RotateAround(new Vector3(0.0f, 0.0f, 0.0f), Vector3.up, h)
        | false -> ()