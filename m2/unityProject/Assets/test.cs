using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

[SerializeField]
    private SimpleAnimation _simpleAnimation;

	void Start()
	{
		
		_simpleAnimation.CrossFade("walk",1f);
		_simpleAnimation.CrossFade("jump",0.2f);

		_simpleAnimation.Play("Default");
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //_simpleAnimation.Play("Default");
			_simpleAnimation.CrossFade("Default",0.2f);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            //_simpleAnimation.Play("idle");
			_simpleAnimation.CrossFade("idle",0.2f);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
			_simpleAnimation.CrossFade("walk",0.2f);
            //_simpleAnimation.Play("walk");
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            _simpleAnimation.Play("jump");
		
			//_simpleAnimation.CrossFadeQueued("jump",0.2f,QueueMode.CompleteOthers);
        }

		var st = _simpleAnimation.GetState("jump");
		Debug.Log(st.time.ToString() + ": " + st.normalizedTime);
		
		
		
    }
}
