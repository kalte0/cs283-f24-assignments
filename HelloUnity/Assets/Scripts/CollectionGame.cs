using System.Collections;
using System.Collections.Generic;
using TMPro; 
using UnityEngine;

public class CollectionGame : MonoBehaviour
{

    public static int points = 0;
    public TMP_Text pointsDisplay = null;  

    // Start is called before the first frame update
    void Start()
    {
        points = 0;

        if (pointsDisplay != null)
        {
            pointsDisplay.text = "" + GetScore();
        }
        else
        {
            Debug.Log("No TMP_Text Object to display points total");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (pointsDisplay != null)
        {
            pointsDisplay.text = "" + points;
        }
    }

    public static void IncrementScore()
    {
        points++;
    }

    public static int GetScore()
    {
        return points;
    }

}
