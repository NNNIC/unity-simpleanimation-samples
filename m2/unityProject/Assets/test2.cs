using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class test2 : MonoBehaviour {

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
		foreach(var c in m_clips)
		{
			m_sanim.AddState(c, c.name);
		}
	}

	void OnGUI()
	{
		if (m_clips == null) return;
		var num = 12;
		var open = false;
		for(var i =0; i<m_clips.Count; i++)
		{
			var c = m_clips[i];

			if ( i % num == 0) {
				GUILayout.BeginHorizontal();
				open = true;
			}
			if (GUILayout.Button(c.name))
			{
				m_sanim.CrossFade(c.name,0.2f);
			}
			if (i % num == num-1) {
				GUILayout.EndHorizontal();
				open = false;
			}
		}
		if (open) {
				GUILayout.EndHorizontal();
		}
	}
	#endregion

}
