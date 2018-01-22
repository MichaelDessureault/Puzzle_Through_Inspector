using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class GenericScriptableObjectList {
    [SerializeField] private List<ScriptableObject> _list = new List<ScriptableObject>();
    
    /// <summary>
    /// CustomReplace will attempt to replace the value at the index passed. If the index is out of range it adds the item instead
    /// </summary>
    public void CustomReplace<T> (int i, T obj) where T : ScriptableObject {
        try {
            _list[i] = obj as T;
        } catch (Exception) {
            _list.Add(obj as T);
        }
    }

    /// <summary>
    /// Adds a null value into the list (acts as an empty placeholder)
    /// </summary>
    public void AddNull () {
        _list.Add(null);
    }
    
    public T GetValue<T> (int index) where T : ScriptableObject {
        try {
            return _list[index] as T;
        } catch (Exception) {
            return null;
        }
    }
    
    public void RemoveAt(int index) {
        _list.RemoveAt(index);
    }

    public SerializedObject GetSerializedObjectAtIndex(int index) {
        try {
            return new SerializedObject(_list[index]);
        } catch (Exception) {
            return null;
        }
    }
}
