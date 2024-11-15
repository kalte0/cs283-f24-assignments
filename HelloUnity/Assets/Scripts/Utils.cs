using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static int maxRange = 100; 
    public static bool RandomPointOnTerrain(Vector3 center, float range, out Vector3 result)
    {
        UnityEngine.AI.NavMeshHit hit;
        while (range < maxRange)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            if (UnityEngine.AI.NavMesh.SamplePosition(randomPoint, out hit, range, UnityEngine.AI.NavMesh.AllAreas))
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
