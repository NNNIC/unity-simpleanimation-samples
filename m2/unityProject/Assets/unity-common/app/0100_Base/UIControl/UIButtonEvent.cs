using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonEvent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PushDown()
    {
        MainStateEvent.Push(MainStateEventId.BUTTON, name);
    }

}
