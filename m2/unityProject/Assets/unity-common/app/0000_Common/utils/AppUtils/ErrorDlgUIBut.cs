using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorDlgUIBut : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    	
	}


    public void PushOK()
    {
        var parent = gameObject.transform.parent.gameObject;
        GameObject.DestroyImmediate(parent);
    }
}
