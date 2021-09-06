using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    float halfWidth;
    float halfHeight;
    public Camera topLeftCam;
    public Camera bottomLeftCam;
    public Camera topRightCam;
    public Camera bottomRightCam;
    public static Camera currentCam;


    // Start is called before the first frame update
    void Start()
    {
        halfWidth = Screen.width * 0.5f;
        halfHeight = Screen.height * 0.5f;
        //currentCam = leftCam;
    }

    // Update is called once per frame
    void Update()
    {
        DetectCurrentCamera();

    }
    private void DetectCurrentCamera()
    {
        if (Input.mousePosition.x < halfWidth && Input.mousePosition.y > halfHeight)
        {
            currentCam = topLeftCam;
        }
        else if(Input.mousePosition.x < halfWidth && Input.mousePosition.y < halfHeight)
        {
            currentCam = bottomLeftCam;
        }
        else if (Input.mousePosition.x > halfWidth && Input.mousePosition.y > halfHeight)
        {
            currentCam = topRightCam;
        }
        else if(Input.mousePosition.x > halfWidth && Input.mousePosition.y < halfHeight)
        {
            currentCam = bottomRightCam;
        }
    }
    
}
