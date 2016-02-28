namespace SphereMotion
open UnityEngine


type SphereMotion() =
    inherit MonoBehaviour()

    let shouldSphereMove (mousePos:Vector3) (transformPos:Vector3) : bool =
        mousePos
        |> ( fun pos -> Camera.main.ScreenPointToRay pos )
        |> ( fun ray -> Vector3.Cross(ray.direction, (transformPos - ray.origin)))
        |> ( fun crossProduct -> crossProduct.magnitude )
        |> ( fun mag -> mag < 2.5f )

    member this.Start() =
        printfn "hello world"

    member this.Update() =

        match Input.GetMouseButtonDown(0) with
        | true ->
            let sphereShouldMove = shouldSphereMove Input.mousePosition this.transform.position
            Debug.Log(sphereShouldMove)
        | false -> printfn "bye ma!"