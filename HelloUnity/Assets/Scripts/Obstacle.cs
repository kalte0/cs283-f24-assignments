using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private GameObject playerObj = null; 

    // Start is called before the first frame update
    void Start()
    {
        if (playerObj == null)
        {
            playerObj = GameObject.Find("Player");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, playerObj.transform.position) <= 3) 
        {
            playerObj.transform.position = new Vector3(0, 0, 0);
        }
    }
}
