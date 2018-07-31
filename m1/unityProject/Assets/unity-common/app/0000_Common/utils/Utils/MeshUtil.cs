using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class MeshUtil {

    public static Mesh CreateRectangle(float width, float height, bool bVerticalOrHorizontal)
    {
	    Vector3[] verts    = new Vector3[4];
	    Vector3[] normals  = new Vector3[4];
	    Vector2[] uv       = new Vector2[4];
	    int[]     tri      = new int[6];
	    /*
          v2    v3
           +----+
           |    |
           |    |
           +----+   
          v0    v1
        */
        //verts[0] = new Vector3(0, 0, 0);
        //verts[1] = new Vector3(width, 0, 0);
        //verts[2] = new Vector3(0, 0, height);
        //verts[3] = new Vector3(width, 0, height);
	
        float hw = width  / 2f;
        float hh = height / 2f;

        if (bVerticalOrHorizontal)
        {
   	        verts[0] = new Vector3(-hw, -hh, 0);
	        verts[1] = new Vector3(+hw, -hh, 0);
	        verts[2] = new Vector3(-hw, +hh, 0);
	        verts[3] = new Vector3(+hw, +hh, 0);
        }
        else
        {
   	        verts[0] = new Vector3(-hw,0, -hh);
	        verts[1] = new Vector3(+hw,0, -hh);
	        verts[2] = new Vector3(-hw,0, +hh);
	        verts[3] = new Vector3(+hw,0, +hh);
        }
        for (int i = 0; i < normals.Length; i++) {
		    normals[i] = UnityEngine.Vector3.up;
	    }
	
	    uv[0] = new Vector2(0, 0);
	    uv[1] = new Vector2(1, 0);
	    uv[2] = new Vector2(0, 1);
	    uv[3] = new Vector2(1, 1);
	
	    tri[0] = 0;
	    tri[1] = 2;
	    tri[2] = 3;
	
	    tri[3] = 0;
	    tri[4] = 3;
	    tri[5] = 1;
	
	    Mesh mesh  = new Mesh();
	    mesh.vertices = verts;
	    mesh.triangles = tri;
	    mesh.uv = uv;
	    mesh.normals = normals;
	
	    return mesh;
    }
    public static Mesh CreateRectangle(float width, float height, float ux,float uy, float uw, float uh,  bool bVerticalOrHorizontal)
    {
	    Vector3[] verts    = new Vector3[4];
	    Vector3[] normals  = new Vector3[4];
	    Vector2[] uv       = new Vector2[4];
	    int[]     tri      = new int[6];
	    /*
          v2    v3
           +----+
           |    |
           |    |
           +----+   
          v0    v1
        */
        //verts[0] = new Vector3(0, 0, 0);
        //verts[1] = new Vector3(width, 0, 0);
        //verts[2] = new Vector3(0, 0, height);
        //verts[3] = new Vector3(width, 0, height);
	
        float hw = width  / 2f;
        float hh = height / 2f;

        if (bVerticalOrHorizontal)
        {
   	        verts[0] = new Vector3(-hw, -hh, 0);
	        verts[1] = new Vector3(+hw, -hh, 0);
	        verts[2] = new Vector3(-hw, +hh, 0);
	        verts[3] = new Vector3(+hw, +hh, 0);
        }
        else
        {
   	        verts[0] = new Vector3(-hw,0, -hh);
	        verts[1] = new Vector3(+hw,0, -hh);
	        verts[2] = new Vector3(-hw,0, +hh);
	        verts[3] = new Vector3(+hw,0, +hh);
        }
        for (int i = 0; i < normals.Length; i++) {
		    normals[i] = UnityEngine.Vector3.up;
	    }
	
	    uv[0] = new Vector2(ux     , uy     );
	    uv[1] = new Vector2(ux + uw, uy     ) ;
	    uv[2] = new Vector2(ux     , uy + uh);
	    uv[3] = new Vector2(ux + uw, uy + uh);
	
	    tri[0] = 0;
	    tri[1] = 2;
	    tri[2] = 3;
	
	    tri[3] = 0;
	    tri[4] = 3;
	    tri[5] = 1;
	
	    Mesh mesh  = new Mesh();
	    mesh.vertices = verts;
	    mesh.triangles = tri;
	    mesh.uv = uv;
	    mesh.normals = normals;
	
	    return mesh;
    }
    public static Bounds GetBounds(Mesh mesh)
    {
        Vector3[] verts = mesh.vertices;
        Bounds bo = new Bounds();
        for(int i = 0; i<verts.Length; i++)
        {
            if (i==0) 
            {
                bo = new Bounds(verts[i],Vector3.zero);
            }
            else
            {
                bo.Encapsulate(verts[i]);
            }
        }

        return bo;
    }

    public static Bounds GetBounds(Mesh mesh, Matrix4x4 m)
    {
        Vector3[] verts = mesh.vertices;
        Bounds bo = new Bounds();
        for(int i = 0; i<verts.Length; i++)
        {
            Vector3 nv =m.MultiplyPoint(  verts[i] );

            if (i==0) 
            {
                bo = new Bounds(nv,Vector3.zero);
            }
            else
            {
                bo.Encapsulate(nv);
            }
        }

        return bo;
    }
    public static Bounds GetBounds(Mesh mesh, Transform t)
    {
        Vector3[] verts = mesh.vertices;
        Bounds bo = new Bounds();
        for(int i = 0; i<verts.Length; i++)
        {
            Vector3 nv = t.TransformPoint(  verts[i] );

            if (i==0) 
            {
                bo = new Bounds(nv,Vector3.zero);
            }
            else
            {
                bo.Encapsulate(nv);
            }
        }

        return bo;
    }

    public static Mesh CombineMeshes(Mesh[] meshlist)
    {
        if (meshlist==null || meshlist.Length==0)
        {
            Debug.Log("CombineMeshes : Meshlist does not exist.");
            return null;
        }
        if (meshlist.Length == 1)
        {
            return meshlist[0];
        }


        var newvlist = new List<Vector3>();
        var newtrs   = new List<int>();

        List<Vector2> newuvlist = (meshlist[0].uv!=null && meshlist[0].uv.Length == meshlist[0].vertexCount) ?  new List<Vector2>() : null;

        foreach(var m in meshlist)
        {
            if (m==null) continue;
            var offset = newvlist.Count;

            for(int i = 0; i<m.vertices.Length; i++)
            {
                var v = m.vertices[i];
                newvlist.Add(v);

                if (newuvlist!=null) 
                {
                    var uv = m.uv[i];
                    newuvlist.Add(uv);
                }
            }

            var trs = m.triangles;
            if (trs==null || trs.Length==0)
            {
                trs = m.GetTriangles(0);
            }

            foreach(var i in trs)
            {
                newtrs.Add(i + offset);
            }
        }

        Mesh newmesh = new Mesh();
        newmesh.vertices = newvlist.ToArray();
        newmesh.triangles = newtrs.ToArray();

        if (newuvlist!=null)   newmesh.uv        = newuvlist.ToArray();

        newmesh.RecalculateNormals();
        newmesh.RecalculateBounds();

        return newmesh;
    }

    //メッシュ移動
    public static Mesh Move(Mesh m, Vector3 v)
    {
        List<Vector3> vlist = new List<Vector3>();
        vlist.AddRange(m.vertices);
        for(int i = 0; i < vlist.Count; i++)
        {
            vlist[i] += v;
        }
        m.vertices = vlist.ToArray();
        m.RecalculateBounds();
        m.RecalculateNormals();
        return m;
    }
    /// <summary>
    /// Boundsリストからメッシュを作成。　＃影用
    /// Ｙ値無視
    /// UVはBoundsごとに設定
    /// </summary>
    public static Mesh CreateMeshFromBounds_ignoreY(Bounds[] bolist)
    {
        List<Mesh> meslist = new List<Mesh>();
        foreach(var bo in bolist)
        {
            var m = CreateRectangle(bo.size.x,bo.size.z,false);
            m = Move(m,  VectorUtil.Zero_Y(bo.center));
            meslist.Add(m);
        }
        return CombineMeshes(meslist.ToArray());
    }

    //ベルトメッシュ ボーン付き SkinnedMeshRendererを使う
    public static GameObject CreateBelt(float width, float len, int segment /* 最低２ */) 
    {
        /*
            x=0
            |
   width->| V |<- 
          +---+  <- bone Segment-1
          |   | 
          +---+  <- bone Segment-2
          |   |
           
                
    v     |   |
   ---  v2+---+v3 <- bone 1
          |   |
   ---    +---+  <- bone 0
    ^    v0   v1 
    |
  height = len / (Sengmen -1)
         
         */

        float height = len / (segment-1);

        Vector3[] vlist = new Vector3[segment * 2];
        for(int i = 0; i < vlist.Length; i++)
        {
            var z = (i / 2) * height;
            var hw = width / 2;
            var x = -hw + (i % 2) * width;
            vlist[i] = Vector3.right * x + Vector3.forward * z;
        }

        List<int> triList = new List<int>();
        for(int i = 0; i<segment-1; i++)
        {
            int v0 = i * 2;
            int v1 = v0 + 1;
            int v2 = v0 + 2;
            int v3 = v0 + 3;

            triList.Add(v0);
            triList.Add(v2);
            triList.Add(v1);

            triList.Add(v2);
            triList.Add(v3);
            triList.Add(v1);
        }

        GameObject go = new GameObject("Belt");
        var smrender = go.AddComponent<SkinnedMeshRenderer>();
        Mesh mesh = new Mesh();
        mesh.vertices = vlist;
        mesh.triangles = triList.ToArray();
        smrender.material = new Material(Shader.Find("Diffuse"));

        BoneWeight[] bwList = new BoneWeight[vlist.Length];
        for(int i = 0; i<segment; i++)
        {
            var v0 = i * 2;
            var v1 = v0 + 1;

            bwList[v0].boneIndex0 = i;
            bwList[v0].weight0 = 1;

            bwList[v1].boneIndex0 = i;
            bwList[v1].weight0 = 1;
        }

        mesh.boneWeights = bwList;

        Transform parent = go.transform;
        Transform[] bones = new Transform[segment];
        Matrix4x4[] bindposes = new Matrix4x4[segment];

        for(int i = 0; i<segment; i++)
        {
            bones[i] = new GameObject("bone" + i.ToString()).transform;
            bones[i].parent = parent;
            bones[i].localRotation = Quaternion.Euler(0,0,0);
            bones[i].localPosition = Vector3.forward * vlist[i*2].z;
            bindposes[i] = bones[i].worldToLocalMatrix * parent.localToWorldMatrix;
        }
        mesh.bindposes = bindposes;
        smrender.bones = bones;
        smrender.sharedMesh = mesh;
        return go;

    }

}
