using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeController : MonoBehaviour
{
    public Transform Target = null;
    public Transform PointJoint = null; // head 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Pnose = GameObject.Find("Nose").transform.position;
        Vector3 Phead = PointJoint.position;
        Debug.DrawLine(Pnose, Pnose + Vector3.up, Color.black, 0.0f, true);
        Debug.DrawLine(Phead, Phead + Vector3.up, Color.black, 0.0f, true);

        Vector3 targetPosition = Target.position;
        Vector3 headToTarget = targetPosition - Phead;
        Vector3 Ptarget = Phead + Vector3.Project(Pnose-Phead, headToTarget);
        Debug.DrawLine(targetPosition, targetPosition + Vector3.up, Color.black, 0.0f, true);

        Vector3 r = targetPosition - Phead;
        Vector3 e = targetPosition - Pnose;

        /*Debug.Log("nosePosition: " + nosePosition);
        Debug.Log("headPosition: " + headPosition);
        Debug.Log("targetposition: " + targetposition);
        Debug.Log("r: " + r);
        Debug.Log("e: " + e);
        Debug.Log("RCrossE: " + RCrossE);*/

        Vector3 RCrossE = Vector3.Cross(r, e);
        Vector3 axis = RCrossE.normalized;

        float theta = Mathf.Atan2(RCrossE.magnitude, Vector3.Dot(r, r) + Vector3.Dot(r, e));
        Debug.Log(theta); 
        Quaternion rotation = Quaternion.AngleAxis(theta, axis);

        PointJoint.rotation *= rotation; 

        //Debug.DrawLine(Phead, Phead + r, Color.blue, 0.0f, true);
        Debug.DrawLine(Pnose, Pnose + e, Color.green, 0.0f, true);
        Debug.DrawLine(targetPosition, targetPosition + RCrossE, Color.yellow, 0.0f, true);
        Debug.DrawLine(Phead, Phead + Pnose - Phead, Color.white, 0.0f, true);
        Debug.DrawLine(Phead, Phead + headToTarget, Color.red, 0.0f, true);
    }
}
