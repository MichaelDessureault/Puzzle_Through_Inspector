using System;
using UnityEngine;

/// <summary>
/// This component will be attachted to any object that "wants" to be a puzzle
/// Currently there are 2 types of puzzle objects: PressurePad and Rotation objects
/// PressurePad requires an IsTriggerable component and requires Pressed to be called for activation
/// Rotation requires an IsRotatable component and requires Rotate to be called for activation
/// </summary>
[System.Serializable]
public class Puzzle : MonoBehaviour {
    public PuzzleObjectType puzzleType;
    
    PuzzlePressurePad pppObj;
    PuzzleRotation prObj;
    PuzzleSolver puzzleSolver;
    
    [SerializeField] [HideInInspector] string id = string.Empty;
    
    public string UniqueId {
        get {
            if (id.Equals(string.Empty))
                GenerateId();
            return id;
        }
    }

    public void GenerateId() {
        Debug.Log("Generating");
        id = Guid.NewGuid().ToString("N");
    }

    public void Pressed (GameObject obj) {
        if (pppObj != null && puzzleSolver != null) {
            pppObj.active = !pppObj.active; 
            puzzleSolver.CheckSolvedState();
        }
    }

    public void Rotate () {
        if (prObj != null && puzzleSolver != null) {
            prObj.NextState();
            puzzleSolver.CheckSolvedState();
        }
    }

    #region Getters and Setters
    public void SetPuzzleSolver (PuzzleSolver ps) {
        puzzleSolver = ps;
    }

    public PuzzlePressurePad GetPuzzlePressurePad () {
        if (pppObj == null) {
            pppObj = ScriptableObject.CreateInstance(typeof(PuzzlePressurePad)) as PuzzlePressurePad;
        }

        return pppObj;
    }

    public PuzzleRotation GetPuzzleRotation () {
        if (prObj == null) {
            prObj = ScriptableObject.CreateInstance(typeof(PuzzleRotation)) as PuzzleRotation;
        }

        return prObj;
    }
    #endregion


    public override string ToString() {
        return "id: " + id;
    }
}