using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class GrassRender : MonoBehaviour {

 	public Mesh grassMesh;
 	public GameObject GroundPlane;
 	Mesh GroundPlaneMesh;
	public Material material;
 
 	[Range(1,1000)]
    public int RandomSeed;
    public Vector2 grassSize = new Vector2(1,1);

   

    public float grassOffset = 0.0f;

	List<Matrix4x4> materices;
	
	void Start(){
	    
	    
	}

	// Update is called once per frame
	void Update ()
	{
		Random.InitState(RandomSeed);
		GroundPlaneMesh = GroundPlane.GetComponent<MeshFilter>().mesh;
	    materices = new List<Matrix4x4>(GroundPlaneMesh.vertices.Length);
	    for (int i = 0; i < GroundPlaneMesh.vertices.Length; ++i)
	    {
	        Vector3 origin = transform.position;
			Quaternion rotation = Quaternion.Euler(0,Random.Range(0.0f, 60.0f), 0);
			origin = Vector3.Scale(GroundPlaneMesh.vertices[i], GroundPlane.transform.localScale);
			float scale = Random.Range(grassSize.x, grassSize.y);

			origin.y += grassOffset - (1.0f-scale)/2.0f;
			origin.x += Random.Range(-0.5f, 0.5f);
			origin.z += Random.Range(-0.5f, 0.5f);

			materices.Add(Matrix4x4.TRS(origin, rotation, Vector3.one * scale));

	    }

		Graphics.DrawMeshInstanced(grassMesh, 0, material, materices);

	}
}
