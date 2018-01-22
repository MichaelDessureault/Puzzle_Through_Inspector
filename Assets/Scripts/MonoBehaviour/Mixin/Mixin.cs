using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mixin : MonoBehaviour {
    public string key;

    public virtual void OnValidate()
    {
        if (key == null)
            return;

        switch (key.ToLower())
        {
            case "puzzle":
                if (gameObject.GetComponent<Puzzle>() == null)
                    gameObject.AddComponent<Puzzle>();
                break;
        }
    }
}
