using UnityEngine;

[System.Serializable]
public class PuzzlePressurePad : ScriptableObject {
    public bool active;

    public override string ToString() {
        return "Active: " + active;
    }
}