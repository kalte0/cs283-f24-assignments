using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Networking.UnityWebRequest;

public class Wander : MonoBehaviour
{
    public GameObject building = null;
    public float range = 2.0f;
    public float maxRange = 100.0f;
    public float remDistance = 0.01f;
    private NavMeshAgent nma = null;

    // Start is called before the first frame update
    void Start()
    {
        if (building != null)
        {
            Vector3 result;
            FindNextPosition(gameObject.transform.position, range, out result);
            if (gameObject.GetComponent<NavMeshAgent>() != null)
            {
                nma = gameObject.GetComponent<NavMeshAgent>();
                nma.SetDestination(result);
                //Debug.Log(nma.destination); 
            }
            else
            {
                Debug.Log("No NavMeshAgent on gameObject!"); 
            }
        }
        else
        {
            Debug.Log("No building selected!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<NavMeshAgent>() != null)
        {
            if (nma.remainingDistance <= remDistance)
            {
                Vector3 nextPosition;
                Vector3 randomPoint = gameObject.transform.position + Random.insideUnitSphere * range;
                randomPoint.y = gameObject.transform.position.y; 

                if (!FindNextPosition(gameObject.transform.position, range, out nextPosition))
                {
                    Debug.Log("Could not find next position within maxRange!"); 
                }
                nma.SetDestination(nextPosition);
                //Debug.Log(nma.destination); 
            }
        }
        else
        {
            Debug.Log("Game Object does not have NavMeshAgent!");
        }
    }

    bool FindNextPosition(Vector3 center, float range, out Vector3 result)
    {
        NavMeshHit hit;
        while (range < maxRange)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range; 
            if (NavMesh.SamplePosition(randomPoint, out hit, range, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }

            range += 1.0f; // expand the search range if cannot find anything. 
        }

        result = Vector3.zero;
        return false; 
    }
}
