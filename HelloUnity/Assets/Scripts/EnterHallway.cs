using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterHallway : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Entered Hallway");
            GameObject HallwayCamera = GameObject.Find("HallwayCamera/Pivot/MainCamera2");
            HallwayCamera.tag = "MainCamera";

            GameObject HomeCamera = GameObject.Find("HomeCamera/Pivot/MainCamera1");
            HomeCamera.tag = "Untagged";
        }
    }
}
