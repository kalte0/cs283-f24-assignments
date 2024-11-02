using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Collectible : MonoBehaviour
{
    private GameObject player; 
    // Start is called before the first frame update
    void Start()
    { 
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        ParticleSystem ps;
        MeshRenderer mr; 
        if (player != null)
        {
            CollectionGame.IncrementScore();
            ps = GetComponent<ParticleSystem>();
            ps.Play();
            mr = GetComponent<MeshRenderer>();
            mr.enabled = false; 
            StartCoroutine(DeactivateAfterAnimation(ps.duration));
        }
        else
        {
            Debug.Log("No object with tag Player found!");
        }

        
         
    }

    IEnumerator DeactivateAfterAnimation(float animationLength)
    {
        yield return new WaitForSeconds(animationLength);
        gameObject.SetActive(false);
    }
}
