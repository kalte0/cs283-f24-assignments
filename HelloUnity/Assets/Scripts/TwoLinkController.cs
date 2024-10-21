using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoLinkController : MonoBehaviour
{
    public Transform target = null;
    public Transform endEffector = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform middle = endEffector.parent;
        Transform top = middle.parent;

        Vector3 r = target.position - top.position;
        Vector3 e = target.position - endEffector.position; 
        Vector3 RCrossE = Vector3.Cross(r, e);

        // calculate the elbow degrees
        float l1 = (top.position - middle.position).magnitude;
        float l2 = (middle.position - endEffector.position).magnitude;
        float elbowDegrees = Mathf.Acos(
                            (Mathf.Pow(r.magnitude, 2) - Mathf.Pow(l1, 2) - Mathf.Pow(l2, 2))
                            / (-2*l1*l2));
        
        //middle.rotation *= Quaternion.AngleAxis(elbowDegrees, RCrossE.normalized);
        Vector3 rotationVector = new Vector3(0, 180 - (elbowDegrees * Mathf.Rad2Deg), 0); 
        middle.localRotation = Quaternion.Euler(rotationVector); 
        Debug.Log("elbowDegrees: " + elbowDegrees * Mathf.Rad2Deg); 

        // calculate the rotation on the shoulder
        float theta = Mathf.Atan2(RCrossE.magnitude, Vector3.Dot(r, r) + Vector3.Dot(r, e));
        Debug.Log(theta); 
        Quaternion rotation = Quaternion.AngleAxis(theta, RCrossE.normalized);
        top.rotation *= rotation;
        

        if ((top.position-endEffector.position).magnitude == (top.position - target.position).magnitude) 
        {
            Debug.Log("Distance from grandparent and end effoctor is same as from grandparent joint and target");
        }
        else
        {
            Debug.Log("Distance from grandparent and end effoctor is NOT same as from grandparent joint and target");
        }
        //Debug.Log("Distance from grandparent and end effector: " + (top.position - endEffector.position).magnitude);
        //Debug.Log("Distance from grandparent join and target: " + (top.position - target.position).magnitude);

        Debug.DrawLine(top.position, middle.position, Color.blue, 0.0f, true);
        Debug.DrawLine(middle.position, endEffector.position, Color.blue, 0.0f, true);
        Debug.DrawLine(top.position, top.position + r, Color.green, 0.0f, true);
        Debug.DrawLine(target.position, target.position + RCrossE, Color.red, 0.0f, true); 
        Debug.DrawLine(endEffector.position, endEffector.position + e, Color.green, 0.0f, true);
    }
}
