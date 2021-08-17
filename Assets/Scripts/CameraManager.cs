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
        /*if (Input.GetMouseButtonDown(0))
        {
            DetectCurrentCamera();

            //Vector3 worldPoint = currentCam.ScreenToWorldPoint(Input.mousePosition);
            //RaycastHit2D hit = Physics2D.Raycast(worldPoint, transform.forward, 1000f);
            //Debug.DrawRay(worldPoint, transform.forward * 10, Color.red);
            
        }*/

        
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
