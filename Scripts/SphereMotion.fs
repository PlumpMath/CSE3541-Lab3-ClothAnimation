namespace SphereMotion
open UnityEngine

type SphereMotion() =
    inherit MonoBehaviour()

    let shouldSphereMove (mousePos:Vector3) (transformPos:Vector3) : bool =
        mousePos
        |> ( fun pos -> Camera.main.ScreenPointToRay pos )
        |> ( fun ray -> Vector3.Cross(ray.direction, (transformPos - ray.origin)) )
        |> ( fun crossProduct -> crossProduct.magnitude )
        |> ( fun mag -> mag < 2.5f )

    let sphereScreenPointFromMouse (mousePos:Vector3) (transformPos:Vector3) : Vector3 =
        let transformScreenPoint = Camera.main.WorldToScreenPoint(transformPos)

        transformScreenPoint
        |> ( fun sp -> mousePos - sp )
        |> ( fun offset -> mousePos - offset )
        |> ( fun offsetMousePos -> new Vector3(offsetMousePos.x, offsetMousePos.y, transformScreenPoint.z) )
        |> ( fun spherePos ->
                Camera.main.WorldToScreenPoint spherePos )

    member this.Update() =
        match Input.GetMouseButton(0) with
        | true ->
            let sphereShouldMove = shouldSphereMove Input.mousePosition this.transform.position
            match sphereShouldMove with 
            | true ->
                this.transform.position <- sphereScreenPointFromMouse Input.mousePosition this.transform.position
            | false ->
                let h:float32 = 2.0f * Input.GetAxis("Mouse X")
                Camera.main.transform.RotateAround(new Vector3(0.0f, 0.0f, 0.0f), Vector3.up, h)
        | false -> ()