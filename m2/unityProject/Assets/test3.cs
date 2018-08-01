using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class test3 : MonoBehaviour {

	public static test3 V;

	public string m_animation_folder="Assets/UnityChan/Animations";

	public SimpleAnimation m_sanim;

	public List<AnimationClip> m_clips;

	#if UNITY_EDITOR
	[ContextMenu("Refresh Animation Clips")]
	void SetupSimpleAnimationComponent()
	{
		var folderasset = AssetDatabase.LoadAssetAtPath(m_animation_folder, typeof(UnityEngine.Object));
		Selection.SetActiveObjectWithContext(folderasset,null);
		var listupType = System.Type.GetType("ListAllAnimationClipsInFolder, Assembly-CSharp-Editor");
		var api = listupType.GetMethod("List");
		api.Invoke(null,new object[]{});
	
		var fi = listupType.GetField("m_clips");
		var clips = (List<AnimationClip>)fi.GetValue(null);

		m_clips = clips;
		// foreach(var c in clips)
		// {
		// 	Debug.Log(c.name);
		// 	m_sanim.AddState(c,c.name);
		// }
	}

	[ContextMenu("Setup2")]
	void SetupSimpleAnimationComponent2()
	{
		foreach(var c in m_clips)
		{
			m_sanim.AddState(c, c.name);
		}
	}
	#endif

	#region Framework
	void Start()
	{
		V = this;
		foreach(var c in m_clips)
		{
			m_sanim.AddState(c, c.name);
		}
	}

	void OnGUI()
	{
		GUILayout.Button("Key F : Forward, Key J : Jump");
	}
	#endregion

	#region service
	string m_curstate=null;
	public void Kick(string name)
	{
		m_curstate = name;
		m_sanim.CrossFade(name,0.2f);		
	}
	public bool IsDone()
	{
		var st = m_sanim.GetState(m_curstate);
		if (st==null) return true;
		Debug.Log(st.normalizedTime);
		return (st.normalizedTime >= 1.0f);
	}
	#endregion
}
