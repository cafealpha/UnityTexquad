using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTexquad : MonoBehaviour {
    public Texture tex;

    // Use this for initialization
    void Start () {
        TEX_VERTEX[] vt ={
            new TEX_VERTEX(-1.0f, -1.0f, 1.25f, 0.0f, 0.0f, -1.0f, 0.0f, 1.0f ),
            new TEX_VERTEX(-1.0f, 1.0f, 1.25f, 0.0f, 0.0f, -1.0f, 0.0f, 0.0f ),
            new TEX_VERTEX(1.0f, 1.0f, 1.25f, 0.0f, 0.0f, -1.0f, 1.0f, 0.0f ),
            new TEX_VERTEX(-1.0f, -1.0f, 1.25f, 0.0f, 0.0f, -1.0f, 0.0f, 1.0f ),
            new TEX_VERTEX(1.0f, 1.0f, 1.25f, 0.0f, 0.0f, -1.0f, 1.0f, 0.0f ),
            new TEX_VERTEX(1.0f, -1.0f, 1.25f, 0.0f, 0.0f, -1.0f, 1.0f, 1.0f )
        };
        
        //인덱스가 없는 버텍스 리스트이므로 인덱스버퍼에 순서대로 넣어주기만 하면 된다.
        int[] triangles = new int[vt.Length];
        for (int i = 0; i < triangles.Length; i++)
        {
            triangles[i] = i;
        }

        //노말과 버텍스 컬러는 입력할 필요 없다.
        //노말은 이미 버텍스 구조체 안에 들어있음.

        //버텍스 리스트 추출
        Vector3[] vtl = new Vector3[vt.Length];
        for (int i = 0; i < vt.Length; i++)
        {
            vtl[i] = new Vector3(vt[i]._x, vt[i]._y, vt[i]._z);
        }

        //노말 추출
        Vector3[] nl = new Vector3[vt.Length];
        for (int i = 0; i < vt.Length; i++)
        {
            nl[i] = new Vector3(vt[i]._nx, vt[i]._ny, vt[i]._nz);
        }

        //uv 세팅
        Vector2[] uv = new Vector2[vt.Length];
        for (int i = 0; i < vt.Length; i++)
        {
            //유니티의 UV좌표는 dx와 세로가 반대로 이루어지므로 -로 뒤집어주어야 한다.
            uv[i] = new Vector2(vt[i]._u, -vt[i]._v);
        }

        //텍스쳐 로드
        tex = null;
        tex = Resources.Load("Image/amazonbox") as Texture;

        //매쉬 생성
        Mesh mesh = new Mesh();
        mesh.vertices = vtl;
        mesh.triangles = triangles;
        mesh.normals = nl;
        mesh.uv = uv;

        //인스턴스화할 게임오브젝트를 만듬.        
        GameObject obj = new GameObject();
        obj.name = "Texquad";

        //매쉬 필터와 렌더러를 붙여주고 캐시함
        MeshFilter mf = obj.AddComponent<MeshFilter>();
        MeshRenderer mr = obj.AddComponent<MeshRenderer>();
        //매쉬 필터에 적용
        mf.mesh = mesh;
        //머티리얼 적용
        Material mat = new Material(Shader.Find("Custom/BasicTextureShader"));
        mat.mainTexture = tex;

        mr.material = mat;
        //Instantiate(obj);

    }
}
