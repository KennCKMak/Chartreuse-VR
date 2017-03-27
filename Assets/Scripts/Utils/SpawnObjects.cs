using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour 
{

    public Vector3 center;
    public Vector3 size;

    public int quantity;

    public GameObject[] objectsPrefab;

	void Start () 
    {
        for ( int x = 1; x <= quantity; x++)
        {
            Spawn();
        }
	}
	
    void Spawn()
    {
        //Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));  
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0  , Random.Range(-size.z / 2, size.z / 2));  

        GameObject objectToSpawn = objectsPrefab[Random.Range(0, objectsPrefab.Length)];

        Vector3 objectCenter = objectToSpawn.GetComponent<Renderer>().bounds.center;
        Vector3 objectBoundsExtents = objectToSpawn.GetComponent<Renderer>().bounds.extents;
            
        float height = Terrain.activeTerrain.SampleHeight(pos) + Terrain.activeTerrain.GetPosition().y + ( objectBoundsExtents.y /2 ) - objectCenter.y;
        pos.y = height;

        Instantiate(objectToSpawn, pos, objectToSpawn.transform.rotation);
    }

        void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }
}
