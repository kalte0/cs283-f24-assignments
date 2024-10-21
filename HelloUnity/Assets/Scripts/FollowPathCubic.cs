using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPathCubic : MonoBehaviour
{
    public float duration = 3.0F;
    public int positionID = 0;
    public bool DeCasteljau = false;
    private Vector3[] ctrlpts = new Vector3[4]; 

    // Start is called before the first frame update
    void Start()
    {
        ctrlpts[0] = GameObject.Find("Position1").transform.position;
        ctrlpts[1] = GameObject.Find("Position2").transform.position;
        ctrlpts[2] = GameObject.Find("Position3").transform.position;
        ctrlpts[3] = GameObject.Find("Position4").transform.position;
        Debug.Log(ctrlpts[0]);
        Debug.Log(ctrlpts[1]);
        Debug.Log(ctrlpts[2]);
        Debug.Log(ctrlpts[3]);

        //transform.SetPositionAndRotation(ArrayOfTransforms[positionID].position, ArrayOfTransforms[positionID].rotation);

        // StartCoroutine(DoLerp());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           StartCoroutine(DoSlerp());
        }
    }

    void OnDrawGizmos() // visualize Bezier curve
    {
        Gizmos.color = Color.blue;
        //Gizmos.DrawLineStrip(curve, false);
        if (ctrlpts != null)
        {
            print(ctrlpts[0]); 
            foreach (Vector3 p in ctrlpts)
            {
                Gizmos.DrawSphere(p, 0.2f);
            }
        }
    }

    Vector3 PositionOnCurve(float t)
    {
        Vector3 positionAtTime = (float)Mathf.Pow(1 - t, 3) * ctrlpts[0] +
                                 (float)Mathf.Pow(3 * t * (1 - t), 2) * ctrlpts[1] +
                                 (float)3 * t * t * (1 - t) * ctrlpts[2] +
                                 (float)t * t * t * ctrlpts[3];
        return positionAtTime;
    }

    Vector3 RotationOnCurve(float t)
    {
        if (t + 0.05 >= 1)
        {
            return PositionOnCurve(t) - PositionOnCurve((float) (t - 0.05)); 
        }
        return PositionOnCurve((float) (t + 0.05)) - PositionOnCurve(t);
    }

    Vector3 DeCasteljauPosition(float t)
    {
        Vector3 b0 = ctrlpts[0];
        Vector3 b1 = ctrlpts[1];
        Vector3 b2 = ctrlpts[2];
        Vector3 b3 = ctrlpts[3]; 

        Vector3 b10 = Vector3.Lerp(b0, b1, t);
        Vector3 b11 = Vector3.Lerp(b1, b2, t);
        Vector3 b12 = Vector3.Lerp(b2, b3, t);

        Vector3 b20 = Vector3.Lerp(b10, b11, t);
        Vector3 b21 = Vector3.Lerp(b11, b12, t);

        Vector3 b30 = Vector3.Lerp(b20, b21, t);

        return b30; 
    }

    Vector3 DeCasteljauRotation(float t)
    {
        if (t + 0.05 >= 1)
        {
            return DeCasteljauPosition(t) - DeCasteljauPosition((float)(t - 0.05));
        }
        return DeCasteljauPosition((float)(t + 0.05)) - DeCasteljauPosition(t);
    }

    IEnumerator DoSlerp()
    {
    
        if (ctrlpts[0] != Vector3.zero)
        { 
            for (float timer = 0; timer < duration; timer += Time.deltaTime)
            {
                float u = timer / duration;
                if (DeCasteljau)
                {
                    transform.position = DeCasteljauPosition(u);
                    transform.rotation = Quaternion.LookRotation(DeCasteljauRotation(u));
                }
                else
                {
                    transform.position = PositionOnCurve(u);
                    transform.rotation = Quaternion.LookRotation(RotationOnCurve(u));
                }
                yield return null;
            }
        }
        else
        {
            Debug.Log("One of position Transforms was not set! Should be named Position1, Position2, Position3, and Position4");
        }

    }



}
