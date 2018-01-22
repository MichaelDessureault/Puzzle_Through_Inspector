using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PuzzleSolver : MonoBehaviour {
    public PuzzleSaveState puzzleSaveState;
    public List<Puzzle> puzzleComponents;
    [SerializeField] private PuzzleSolverHelper helper = new PuzzleSolverHelper();
    
    public PuzzleSolverHelper GetHelper {
        get { return helper; }
    }
    
    private List<bool> states;
    
    public void Start()  {
        if (puzzleSaveState == null) {
            puzzleSaveState = ScriptableObject.CreateInstance(typeof(PuzzleSaveState)) as PuzzleSaveState;
        }
        
        // All Puzzle components in the puzzleComponents list get the PuzzleSolver value set
        foreach (Puzzle p in puzzleComponents) {
            if (p != null)
                p.SetPuzzleSolver(this);
        }

        CheckSolvedState();
    }
    
    // Called once a puzzle object has been interacted with
    public void CheckSolvedState() {
        UpdatePuzzleStates();

        // if false is found then it's not solved, so invert the return'd value
        if (!states.Contains(false))
            Solved();
    }
    
    /// <summary>
    /// Checks all the stats for the puzzleComponents 
    /// Wrapped in a try catch to prevent crashing issues when index out of range
    /// </summary>
    public void UpdatePuzzleStates () {
        try {
            states = new List<bool>();
            for (int i = 0; i < puzzleComponents.Count; i++) {
                Puzzle puzzle = puzzleComponents[i];

                if (puzzle != null) {
                    PuzzleObjectType type = puzzle.puzzleType;

                    switch (type) {
                        case PuzzleObjectType.pressurepadObject:
                            states.Add (puzzle.GetPuzzlePressurePad().active == helper.solvedList.GetValue<PuzzlePressurePad>(i).active);
                            break;
                        case PuzzleObjectType.rotationObject:
                            states.Add (puzzle.GetPuzzleRotation().state == helper.solvedList.GetValue<PuzzleRotation>(i).state);
                            break;
                    }
                }
            }
        } catch (Exception e) {
            Debug.Log("Update check : " + e.Message);
        }
    }
    
    private void Solved() {
        // Get the Solved Action attached to the gameobject and invoke it if it's found
        var puzzleSolverAction = gameObject.GetComponent(typeof(IPuzzleSolverAction)) as IPuzzleSolverAction;
        if (puzzleSolverAction != null) {
            puzzleSolverAction.SolvedAction();
        }
    }
}