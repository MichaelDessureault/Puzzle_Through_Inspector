using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class IsRotateable : Mixin {

    public string callBack;

    private bool rotating = false;

    private void OnMouseDown() {
        if (callBack == "" || rotating)
            return;
        
        SendMessage(callBack, SendMessageOptions.DontRequireReceiver);
        StartCoroutine(RotateCoroutine());
    }

    IEnumerator RotateCoroutine() {
        rotating = true;
        int counter = 0;
        while (counter < 18) {
            counter += 1;
            transform.Rotate(0, 5, 0);
            yield return null;
        }
        rotating = false;
    }
}
