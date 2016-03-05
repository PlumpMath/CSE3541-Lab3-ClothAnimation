namespace ClothMotion
open UnityEngine
open Util

type Edge = 
    { Vertex1: int;
      Vertex2: int; }

type ClothEdges =
    { Edges: Edge list }

type ClothMotion() =
    inherit MonoBehaviour()

    let t = 0.075f
    let mass = 1.0f
    let damping = 0.99f
    let stiffness = 1000.0f

    member this.Start() =
        let mesh = this.GetComponent<MeshFilter>().mesh
        let triangles = mesh.triangles
        let vertices = mesh.vertices

        ()