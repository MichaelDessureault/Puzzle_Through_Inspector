using UnityEngine;

[System.Serializable]
public class PuzzleRotation : ScriptableObject {

    private const int enumLength = 4;

    public enum PuzzleRotationState {
        front,
        right,
        back,
        left
    };

    public PuzzleRotationState state;

    public void NextState () {
        state = (PuzzleRotationState) (((int)state + 1) % enumLength);
    }

    public override string ToString() {
        return "State: " + state;
    }
}