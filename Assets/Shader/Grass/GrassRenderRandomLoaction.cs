using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GrassRenderRandomLoaction : MonoBehaviour {
    public Mesh grassMesh;
    public Material material;

    public int seed;
    public Vector2 size;
	public Vector3 grassSize = new Vector3(1,1,1);
	public Vector2 randomGrassScale = new Vector2(1,1);

    [Range(1,1000)]
    public int grassNumber;

    public float startHeight = 1000;
    public float grassOffset = 0.0f;

	List<Matrix4x4> materices;
	
	void Start(){
		Random.InitState(seed);
		float randomScale = Random.Range(randomGrassScale.x, randomGrassScale.y);
	    materices = new List<Matrix4x4>(grassNumber);
	    for (int i = 0; i < grassNumber; ++i)
	    {
	        Vector3 origin = transform.position;
	        origin.y = startHeight;
	        origin.x += size.x * Random.Range(-0.5f, 0.5f);
	        origin.z += size.y * Random.Range(-0.5f, 0.5f);
	        Ray ray = new Ray(origin, Vector3.down);
	        RaycastHit hit;
	        if (Physics.Raycast(ray, out hit))
	        {
				if(hit.transform.gameObject.tag != "FogCurtain")
				{
	            origin = hit.point;
	            origin.y += grassOffset - (1.0f-randomScale)/2.0f;

	            materices.Add(Matrix4x4.TRS(origin, Quaternion.identity, grassSize * randomScale));
				}
	        }
	    }
	}
	
	// Update is called once per frame
	void Update ()
	{

	    Graphics.DrawMeshInstanced(grassMesh, 0, material, materices);


	}
}

