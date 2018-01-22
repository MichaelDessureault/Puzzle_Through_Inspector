using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTriggerable : Mixin {
    
	public string callBack;
	public string TouchType;

    private bool beenHit;

    private void OnTriggerEnter(Collider other)
    {
        if (callBack == "" || beenHit)
            return;
        
        if (other.gameObject.GetComponent(TouchType) != null) {
            beenHit = true;
            SendMessage(callBack, other.gameObject, SendMessageOptions.DontRequireReceiver);
        }
    }

    private void OnTriggerExit(Collider other) {
        beenHit = false;
    }
     
    public override void OnValidate() {
        base.OnValidate();

        if (!GetComponent<Collider>())
            gameObject.AddComponent<BoxCollider>().isTrigger = true;
        else
            GetComponent<Collider>().isTrigger = true;
    }
}
