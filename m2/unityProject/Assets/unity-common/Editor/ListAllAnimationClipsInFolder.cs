using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class ListAllAnimationClipsInFolder {

	public static string m_output;
	public static List<AnimationClip> m_clips = new List<AnimationClip>();

	[MenuItem("Assets/list all animation clip (fbx) in the folder")]
	public static void List()
	{
		if (Selection.assetGUIDs==null || Selection.assetGUIDs.Length!=1 )
		{
			Debug.LogError("Select a folder in the project window");
			return;
		}

		var buf = string.Empty;

		var path = AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs[0]);
		var assetroot = Path.Combine( Application.dataPath ,".." );
		var folderpath = Path.GetFullPath( Path.Combine(assetroot,path) );
		foreach(var fi in (new DirectoryInfo(folderpath)).GetFiles("*.fbx"))
		{
			//Debug.Log(fi.Name);
			var assetlist = AssetDatabase.LoadAllAssetsAtPath(path +"/" + fi.Name );
			if (assetlist!=null)
			{
				foreach(var a in assetlist)
				{
					var clip = a as AnimationClip;
					if (clip!=null) 
					{
						if (clip.name.StartsWith("__")) continue;
						//Debug.Log("-" + a.name);
						if (!string.IsNullOrEmpty(buf)) buf += System.Environment.NewLine;
						buf += a.name;
						m_clips.Add(clip);
					}
				}
			}
		}
		Debug.Log(buf);
		Debug.Log("! the result exists in the clipboad !");
		GUIUtility.systemCopyBuffer = buf;

		m_output = buf;
	}


}
