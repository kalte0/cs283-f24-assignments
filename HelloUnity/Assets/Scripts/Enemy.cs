using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
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
            Debug.Log("Enemy Hit");
            Object.Destroy(gameObject);

            PlayerController.IncrementScore(); 

            GameObject PointsText = GameObject.Find("Canvas/Number");
            TMP_Text number = PointsText.GetComponent<TextMeshProUGUI>();
            number.text = "" + PlayerController.GetLevel();
        }
    }
}
