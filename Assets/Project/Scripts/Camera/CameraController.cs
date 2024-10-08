using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [Header("Zoom Values")]
    [SerializeField] private float minZoomValue;
    [SerializeField] private float maxZoomValue;


    [Header("Offsets")]
    [SerializeField, Range(0, 1)] private float offsetZoomOut;
    [SerializeField, Range(0, 1)] private float offsetZoomIn;
    [SerializeField] private float zoomOutSpeed;

    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
        minZoomValue = camera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayersManager.instance.GetPlayersList().Count == 0)
            return;

        CheckIfCharactersAreOutOfCameraView();
    }

    void CheckIfCharactersAreOutOfCameraView()
    {
        foreach (GameObject player in PlayersManager.instance.GetPlayersList())
        {
            // Convert the character's world position to viewport position
            Vector3 viewportPos = camera.WorldToViewportPoint(player.transform.position);
            if (CheckIfStopZoom(viewportPos))
            {
                Debug.Log("Stop zoom");
                return;
            }
            else if (CheckIfZoomOut(viewportPos))
            {
                ZoomOut();
            }
            else if (CheckIfZoomIn(viewportPos))
            {
                ZoomIn();
            }
        }
    }

    private bool CheckIfStopZoom(Vector3 viewportPos)
    {
        if (viewportPos.x >= 0 + offsetZoomOut && viewportPos.x <= 0 + offsetZoomIn
            || viewportPos.x <= 1 - offsetZoomOut && viewportPos.x >= 1 - offsetZoomIn
            || viewportPos.y >= 0 + offsetZoomOut && viewportPos.y <= 0 + offsetZoomIn
            || viewportPos.y <= 1 - offsetZoomOut && viewportPos.y >= 1 - offsetZoomIn
            )
        {
            return true;
        }
        return false;
    }

    private bool CheckIfZoomOut(Vector3 viewportPos)
    {
        // Check if the character is within the camera's view horizontally and vertically
        if (viewportPos.x <= 0 + offsetZoomOut || viewportPos.x >= 1 - offsetZoomOut
            || viewportPos.y <= 0 + offsetZoomOut || viewportPos.y >= 1 - offsetZoomOut)
        {
            Debug.Log("Time to zoomOut");
            return true;
        }
        return false;
    }

    private bool CheckIfZoomIn(Vector3 viewportPos)
    {

        if (viewportPos.x >= 0 + offsetZoomOut || viewportPos.x <= 1 - offsetZoomOut
                || viewportPos.y >= 0 + offsetZoomOut || viewportPos.y <= 1 - offsetZoomOut)
        {
            Debug.Log("Time to zoom in");
            return true;
        }

        return false;
    }


    private void ZoomOut()
    {
        Debug.Log("zoom out");
        if(camera.orthographicSize <= maxZoomValue) 
        {
            camera.orthographicSize += zoomOutSpeed;
        }
    }

    private void ZoomIn()
    {
        Debug.Log("zoom in");

        if (camera.orthographicSize >= minZoomValue) 
        {
            camera.orthographicSize -= zoomOutSpeed;
        }
    }
}
