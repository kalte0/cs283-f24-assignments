using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class Tour : MonoBehaviour
{
    public float cameraSpeed = 30.0f; // in units per second
    private float elapsedTime = 0.0f;
    private float transitionTime;  
    private GameObject MainCameraBasePosition;
    private GameObject MainCamera; 
    private Transform[] ArrayOfTransforms;
    private int currentTransformIndex = 0; // [0, 2] 
    public static bool interpolating; // true while interpolating between two positions. 

    // Start is called before the first frame update
    void Start()
    {
        MainCameraBasePosition = GameObject.Find("CameraBasePosition"); // find the main camera. 
        MainCamera = GameObject.FindWithTag("MainCamera"); 
        ArrayOfTransforms = new Transform[3];
        ArrayOfTransforms[0] = GameObject.Find("DefaultCameraPosition").transform;
        ArrayOfTransforms[1] = GameObject.Find("HallwayCameraPosition").transform;
        ArrayOfTransforms[2] = GameObject.Find("EndCameraPosition").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (MainCamera != null)
        {
            if (!interpolating && Input.GetKeyDown("n"))
            {
                //reset position to its current base position (before w a s d movement + mouse rotation)
                MainCamera.transform.localPosition = Vector3.zero;
                MainCamera.transform.localRotation = Quaternion.identity;

                interpolating = true;
                float distanceToTravel = Vector3.Distance(ArrayOfTransforms[currentTransformIndex].position, ArrayOfTransforms[(currentTransformIndex + 1) % 3].position);
                transitionTime = distanceToTravel / cameraSpeed; 
                
            }
            else if (interpolating) // if we're in the middle of interpolating, update the position accordingly. 
            {

                Transform start = ArrayOfTransforms[currentTransformIndex];
                Transform destination = ArrayOfTransforms[(currentTransformIndex + 1) % 3];

                elapsedTime += Time.deltaTime; // increment the time to move through the animation
                float interpolationRatio = Math.Min(elapsedTime/transitionTime, 1);

                Debug.Log(interpolationRatio);
                MainCameraBasePosition.transform.position = Vector3.Lerp(start.position, destination.position, interpolationRatio);
                MainCameraBasePosition.transform.rotation = Quaternion.Slerp(start.rotation, destination.rotation, interpolationRatio);

                if (interpolationRatio == 1) // if done with the transformation, 
                {
                    interpolating = false; // mark that we're no longer interpolating, so we can hit "n" again. 
                    elapsedTime = 0.0f; 
                    switchPOI(); // change which transform we start from for the next animation. 
                }

            }
        }
    }

    void switchPOI()
    {
        currentTransformIndex++;
        currentTransformIndex %= 3;
        Debug.Log("currentTransformIndex: " + currentTransformIndex);
    }
}
