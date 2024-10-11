using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CameraController : MonoBehaviour
{

    [Header("Zoom Values")]
    [SerializeField] private float zoomFactor;
    [SerializeField] private float maxZoomValue;
    private float minZoomValue;
    private float currentZoom;
    private Vector3 zoomOutDirection;

    [Header("Offsets")]
    [SerializeField, Range(0, 1)] private float offsetZoomOut;
    [SerializeField, Range(0,1)] private float offsetZoomIn;
    [SerializeField] private float zoomOutSpeed;

    [Header("MidPoint")]
    [SerializeField] private Transform midPointTransform;

    private Camera camera;

    public enum CameraState {  ZOOM_OUT, ZOOM_IN, STOP  }

    [SerializeField] private CameraState cameraState;


    private void Awake()
    {
        camera = Camera.main;
        minZoomValue = camera.orthographicSize;
        cameraState = CameraState.STOP;

        zoomOutDirection = -camera.transform.forward;
        minZoomValue = 0;
        currentZoom = minZoomValue;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayersManager.instance.GetPlayersList().Count == 0)
            return;

        CheckIfCharactersAreOutOfCameraView();

        switch (cameraState)
        {
            case CameraState.ZOOM_OUT:
                ZoomOut();
                break;
            case CameraState.ZOOM_IN:
                ZoomIn();
                break;
            case CameraState.STOP:
                break;
            default:
                break;
        }
    }

    void CheckIfCharactersAreOutOfCameraView()
    {
        foreach (GameObject player in PlayersManager.instance.GetPlayersList())
        {
            // Convert the character's world position to viewport position
            Vector3 viewportPos = camera.WorldToViewportPoint(player.transform.position);
            if (CheckIfStopZoom(viewportPos))
            {
                cameraState = CameraState.STOP;
                Debug.Log("Zoom Stop");
                return;
            }
            else if (CheckIfZoomOut(viewportPos))
            {
                Debug.Log("Zoom out");
                cameraState = CameraState.ZOOM_OUT;
            }
            else if (CheckIfZoomIn(viewportPos))
            {
                Debug.Log("Zoom in");
                cameraState = CameraState.ZOOM_IN;
            }
        }
    }

    private bool CheckIfStopZoom(Vector3 viewportPos)
    {
        if(viewportPos.x >= 0 + offsetZoomOut && viewportPos.x <= 0 + offsetZoomIn
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
            || viewportPos.y <= 0 + offsetZoomOut && viewportPos.y >= 1 - offsetZoomOut)
        {
            return true;
        }
        return false;
    }

    private bool CheckIfZoomIn(Vector3 viewportPos)
    {

        if (viewportPos.x >= 0 + offsetZoomOut || viewportPos.x <= 1 - offsetZoomOut
                || viewportPos.y >= 0 + offsetZoomOut && viewportPos.y <= 1 - offsetZoomOut)
        {
            return true;
        }

        return false;
    }

    private void ZoomOut()
    {
        if(currentZoom <= maxZoomValue) 
        {
            camera.transform.position += zoomOutDirection.normalized * zoomFactor * Time.deltaTime;
        }
    }

    private void ZoomIn()
    {
        if (currentZoom <= minZoomValue)
        {
            camera.transform.position += -zoomOutDirection.normalized * zoomFactor * Time.deltaTime;
        }
    }
}
