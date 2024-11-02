using RogueNoodle.GBCamera;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject template = null;
    public float spawnRange = 5;
    public int maxSpawnedObjects = 10;
    private GameObject[] spawnedObjects;
    private GameObject floor = null;
    private float floorHeight; 

    // Start is called before the first frame update
    void Start()
    {
        spawnedObjects = new GameObject[maxSpawnedObjects];
        floor = GameObject.Find("Building/Building/FloorPlatform");
        floorHeight = floor.GetComponent<Collider>().bounds.max.y;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < maxSpawnedObjects; i++ )
        {
            if (spawnedObjects[i] == null)
            {
                Vector3 position = gameObject.transform.position + new Vector3( Random.Range(-spawnRange, spawnRange),
                                                                                0,
                                                                                Random.Range(-spawnRange, spawnRange));

                position.y = floorHeight + Random.Range(0.25f, 0.5f); 
                spawnedObjects[i] = Instantiate(template, position, Quaternion.identity);
                
                Debug.Log("Lower Bound: " + spawnedObjects[i].GetComponent<Collider>().bounds.min);
            }
            else if (!spawnedObjects[i].activeInHierarchy)
            {
                Vector3 position = gameObject.transform.position + new Vector3(Random.Range(-spawnRange, spawnRange),
                                                                                0,
                                                                                Random.Range(-spawnRange, spawnRange));
                position.y = floorHeight + Random.Range(0.25f, 0.5f);
                spawnedObjects[i].transform.position = position;
                spawnedObjects[i].SetActive(true);
                MeshRenderer mr = spawnedObjects[i].GetComponent<MeshRenderer>();
                mr.enabled = true;
                Debug.Log("Respawned");
            }
        }
    
    }
}
