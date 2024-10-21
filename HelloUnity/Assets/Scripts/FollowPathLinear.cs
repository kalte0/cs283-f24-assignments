using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPathLinear : MonoBehaviour
{
    private Transform[] ArrayOfTransforms;
    public float duration = 3.0F;
    public int positionID = 0;

    // Start is called before the first frame update
    void Start()
    {
        ArrayOfTransforms = new Transform[4];
        ArrayOfTransforms[0] = GameObject.Find("Position1").transform;
        ArrayOfTransforms[1] = GameObject.Find("Position2").transform;
        ArrayOfTransforms[2] = GameObject.Find("Position3").transform;
        ArrayOfTransforms[3] = GameObject.Find("Position4").transform;

        transform.SetPositionAndRotation(ArrayOfTransforms[positionID].position, ArrayOfTransforms[positionID].rotation);
        
        StartCoroutine(DoLerp()); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(DoLerp()); 
        }
    }

    IEnumerator DoLerp()
    {
        // assign the start and end points to be the current position transform to the next position transform, and update the current positionID.  

        Transform start, end;
        if (ArrayOfTransforms[positionID] != null & 
            ArrayOfTransforms[(positionID + 1) % 4] != null)
        {
            Debug.Log(positionID);
            start = ArrayOfTransforms[positionID];

            positionID = (positionID + 1) % 4;
            Debug.Log(positionID);
            end = ArrayOfTransforms[positionID];

            Vector3 facing = new Vector3(end.position.x - start.position.x,
                                         end.position.y - start.position.y,
                                         end.position.z - start.position.z);

            Debug.Log(facing);
            transform.rotation = Quaternion.LookRotation(facing); 
       

            for (float timer = 0; timer < duration; timer += Time.deltaTime)
            {
                //Debug.Log("Timer: " + timer); 
                float u = timer / duration;
                //Debug.Log(u);
                transform.position = Vector3.Lerp(start.position, end.position, u);
                yield return null;
            }
        }
        else
        {
            Debug.Log("One of position Transforms was not set! Should be named Position1, Position2, Position3, and Position4");
        }

        
        
    }

}
