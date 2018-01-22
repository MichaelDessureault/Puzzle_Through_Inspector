using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSolverDisableAction : MonoBehaviour, IPuzzleSolverAction {
   public void SolvedAction() { 
        gameObject.SetActive(false);
   }
}
