using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PuzzleSolver))]
public class PuzzleSolverEditor : Editor {
    #region Serialized Properties
    SerializedProperty puzzleComponentsProp;
    #endregion

    PuzzleSolver solver;
    GenericScriptableObjectList solvedList = new GenericScriptableObjectList();
    List<string> puzzleUniqueIds = new List<string>();

    private void OnEnable() {
        solver = (PuzzleSolver)target;
        puzzleComponentsProp = serializedObject.FindProperty("puzzleComponents");

        PuzzleSolverHelper helper = solver.GetHelper;
        puzzleUniqueIds = helper.puzzleUniqueIds;
        solvedList = helper.solvedList;
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        // Display the script inside the inspector
        GUI.enabled = false;
        EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((MonoBehaviour)target), typeof(PuzzleSolver), false);
        GUI.enabled = true;

        InsertSpaces(1);
        
        EditorGUILayout.LabelField("Note: Please use the \"Remove This Index\" button for deleting an element");
        if (GUILayout.Button("Add Puzzle Component")) {
            AddEmptyPuzzleComponent();
        }

        InsertSpaces(2);

        for (int i = 0; i < puzzleComponentsProp.arraySize; i++) {
            StartVertical();

            SerializedProperty sp = puzzleComponentsProp.GetArrayElementAtIndex(i);
            EditorGUILayout.PropertyField(sp);

            if (sp.objectReferenceValue != null) {

                Puzzle puzzle = (Puzzle)sp.objectReferenceValue;
                
                PopulateSolvedStateList(puzzle, i);
                
                SerializedObject solvedSO = solvedList.GetSerializedObjectAtIndex(i);

                if (solvedSO == null) {
                    Debug.Log("Duplicate puzzle component has been removed");
                    puzzleComponentsProp.DeleteArrayElementAtIndex(i);
                    continue;
                }
                
                SerializedObject so;
                switch (puzzle.puzzleType) {
                    case PuzzleObjectType.pressurepadObject:
                        InsertRequiredSolvedStateField(solvedSO.FindProperty("active"));
                        so = new SerializedObject(puzzle.GetPuzzlePressurePad());
                        InsertCurrentStateField(so.FindProperty("active"));
                        break;
                    case PuzzleObjectType.rotationObject:
                        InsertRequiredSolvedStateField(solvedSO.FindProperty("state"));
                        so = new SerializedObject(puzzle.GetPuzzleRotation());
                        InsertCurrentStateField(so.FindProperty("state"));
                        break;
                }

                solvedSO.ApplyModifiedProperties();
            }

            if (GUILayout.Button("Remove This Index (" + i.ToString() + ")")) {
                if (sp.objectReferenceValue != null)
                    puzzleComponentsProp.DeleteArrayElementAtIndex(i);
                puzzleComponentsProp.DeleteArrayElementAtIndex(i);

                // Apply the changes for deleting the array element
                serializedObject.ApplyModifiedProperties();

                // then remove the elements from both puzzleUniqueIds and solvedList
                puzzleUniqueIds.RemoveAt(i);
                solvedList.RemoveAt(i);
            }

            EndVertical();
            InsertSpaces(1);
        }

        serializedObject.ApplyModifiedProperties();
    }
    
    private void AddEmptyPuzzleComponent() {
        // Adds an empty component into the list
        var count = puzzleComponentsProp.arraySize++;

        // If an item is found in the previous index it will be duplicated, we dont want this so remove it if found
        if (puzzleComponentsProp.GetArrayElementAtIndex(count).objectReferenceValue != null)
            puzzleComponentsProp.DeleteArrayElementAtIndex(count);

        // Update the serializedObject to allow for null values to be added below
        serializedObject.ApplyModifiedProperties();

        // Add to place holder values (null and empty)
        solvedList.AddNull();
        puzzleUniqueIds.Add(string.Empty);
    }
    

    private void PopulateSolvedStateList(Puzzle puzzle, int index) {
        // check if it's found
        if (puzzleUniqueIds.IndexOf(puzzle.UniqueId) != -1) {
            return;
        }
        
        // Check to see if the size is larger then the index passed if so then replace the id with the new item, else add the item to the list
        if (puzzleUniqueIds.Count > index)
            puzzleUniqueIds[index] = puzzle.UniqueId;
        else
            puzzleUniqueIds.Add(puzzle.UniqueId);
        
        // Create the ScriptableObject based on the puzzleType of the puzzle passed
        switch (puzzle.puzzleType) {
            case PuzzleObjectType.pressurepadObject:
                PuzzlePressurePad pp = CreateInstance(typeof(PuzzlePressurePad)) as PuzzlePressurePad;
                solvedList.CustomReplace(index, pp);
                break;
            case PuzzleObjectType.rotationObject:
                PuzzleRotation pr = CreateInstance(typeof(PuzzleRotation)) as PuzzleRotation;
                solvedList.CustomReplace(index, pr);
                break;
        }
    }

    #region PropertyField Inserts
    void InsertRequiredSolvedStateField(SerializedProperty property) {
        EditorGUILayout.PropertyField(property, new GUIContent("Required Solved State"));
    }

    void InsertCurrentStateField(SerializedProperty property) {
        GUI.enabled = false;
        EditorGUILayout.PropertyField(property, new GUIContent("Current State"));
        GUI.enabled = true;
    }
    #endregion

    #region GUI Helpers
    // Creates a vertical box in the inspector for graphic appearance
    private void StartVertical() {
        GUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel++;
    }

    private void EndVertical() {
        EditorGUI.indentLevel--;
        GUILayout.EndVertical();
    }

    // Inserts space into the inspector based on the amount passed
    private void InsertSpaces(int amount) {
        for (int i = 0; i < amount; i++) {
            EditorGUILayout.Space();
        }
    }
    #endregion
}