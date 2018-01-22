using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSolverChangeColourAction : MonoBehaviour, IPuzzleSolverAction {
    public Color color;
    public void SolvedAction() {
        gameObject.GetComponent<MeshRenderer>().material.color = color;
    }
}
