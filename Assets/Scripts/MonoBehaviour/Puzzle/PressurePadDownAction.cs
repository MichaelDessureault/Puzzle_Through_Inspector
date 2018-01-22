using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is ment for a basic demo for the pressure pad to see it's working effect
/// </summary>
public class PressurePadDownAction : MonoBehaviour {

    public float yChange = 0.05f;
    bool pressed = false;
    public void Pressed () {
        pressed = !pressed;
        if (pressed) {
            Vector3 pos = transform.position;
            pos.y -= yChange;
            transform.position = pos;
        } else {
            Vector3 pos = transform.position;
            pos.y += yChange;
            transform.position = pos;
        }
    }
}
