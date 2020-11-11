using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{


    public Transform playerTransform;  

    [SerializeField] private float cameraMoveSpeed; 
    [SerializeField] private float scrollSpeed;
    [SerializeField] private float mousePosMultiplier;
    [SerializeField] private float zoomLowerLimit;
    [SerializeField] private float zoomUpperLimit;

    private float nextUpPoint; //variables for 4 different directions camera can possibly go next
    private float nextDownPoint;
    private float nextRightPoint;
    private float nextLeftPoint;

    private float currentOrthographicHeight;  //half camera height and width in units, to calculate if visible area reached the border
    private float currentOrthographicWidth;

    private bool cameraLock; 

    void Update()
    {
        

        if (Input.GetKeyDown("space"))
        {
            cameraLock = !cameraLock;
        }

        Zooming();

        if (cameraLock)
        {
            transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, -10);
        }
        else
        {
            MoveOnEdges();
        }

    }

    private void Zooming()
    {
        float scrollValue = Input.mouseScrollDelta.y;
        float nextSize = Camera.main.orthographicSize - scrollValue;

        if (scrollValue < 0 && !IsZoomOutable()) //if border is reached, you may no longer zoom out
            return;

        if (scrollValue != 0 && nextSize >= zoomLowerLimit && nextSize <= zoomUpperLimit)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, nextSize, Time.deltaTime * scrollSpeed);
        }
        
    }


    private void MoveOnEdges()
    {
        currentOrthographicHeight = Camera.main.orthographicSize;
        currentOrthographicWidth = currentOrthographicHeight * Camera.main.aspect;

        nextUpPoint = (transform.position + Vector3.up * Time.deltaTime * cameraMoveSpeed).y + currentOrthographicHeight;
        nextDownPoint = (transform.position - Vector3.down * Time.deltaTime * cameraMoveSpeed).y - currentOrthographicHeight;
        nextRightPoint = (transform.position + Vector3.right * Time.deltaTime * cameraMoveSpeed).x + currentOrthographicWidth;
        nextLeftPoint = (transform.position - Vector3.left * Time.deltaTime * cameraMoveSpeed).x - currentOrthographicWidth;

        if (Input.mousePosition.x * mousePosMultiplier >= Screen.width && nextRightPoint < StaticVars.EastEdgeLimit )
        {
            transform.Translate(Vector3.right * Time.deltaTime * cameraMoveSpeed, Space.World);
        }

        if (Input.mousePosition.y * mousePosMultiplier >= Screen.height && nextUpPoint < StaticVars.NorthEdgeLimit)
        {
            transform.Translate(Vector3.up * Time.deltaTime * cameraMoveSpeed, Space.World);
        }

        if (Input.mousePosition.x / mousePosMultiplier <= 30 && nextLeftPoint > -StaticVars.EastEdgeLimit)
        {
            transform.Translate(Vector3.left * Time.deltaTime * cameraMoveSpeed, Space.World);
        }

        if (Input.mousePosition.y / mousePosMultiplier <= 30 && nextDownPoint > -StaticVars.NorthEdgeLimit)
        {
            transform.Translate(Vector3.down * Time.deltaTime * cameraMoveSpeed, Space.World);
        }
    }

    private bool IsZoomOutable()
    {
        if (
            nextRightPoint > StaticVars.EastEdgeLimit   || 
            nextLeftPoint < -StaticVars.EastEdgeLimit   ||
            nextUpPoint > StaticVars.NorthEdgeLimit     || 
            nextDownPoint < -StaticVars.NorthEdgeLimit   )

            return false;

        return true;
    }
    
    
}
