namespace ClothMotion
open UnityEngine

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

