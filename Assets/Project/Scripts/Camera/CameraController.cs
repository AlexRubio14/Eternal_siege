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
    [SerializeField] private float offsetZoomOut;
    [SerializeField] private float offsetZoomIn;
    [SerializeField] private float zoomOutSpeed;


    [SerializeField] private Camera camera;

    private float width = 0f;
    private float height = 0f;

    struct CameraReferences
    {
        public float xMin;
        public float xMax;
        public float yMin;
        public float yMax;
    }

    private CameraReferences cameraReferences;

    public enum CameraState {  ZOOM_OUT, ZOOM_IN, STOP  }

    [SerializeField] private CameraState cameraState;


    private void Awake()
    {
        minZoomValue = camera.orthographicSize;
        cameraState = CameraState.STOP;

        CalculateCameraReferences();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayersManager.instance.GetPlayersList().Count == 0)
            return;

        CheckCameraState();

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


    private void CheckCameraState()
    {
        foreach (GameObject player in PlayersManager.instance.GetPlayersList())
        {
            if (CheckStopZoom(player))
            {
                cameraState = CameraState.STOP;
            }
            else if (CheckZoomOut(player))
            {
                cameraState = CameraState.ZOOM_OUT;
            }
            else if (CheckZoomIn(player))
            {
                cameraState = CameraState.ZOOM_IN;
            }
        }
    }

    private bool CheckStopZoom(GameObject player)
    {
        if(player.transform.position.x > this.cameraReferences.xMin + offsetZoomOut && player.transform.position.x < this.cameraReferences.xMin + offsetZoomIn
            || player.transform.position.x < this.cameraReferences.xMax - offsetZoomOut && player.transform.position.x > this.cameraReferences.xMax - offsetZoomIn
            || player.transform.position.y > this.cameraReferences.yMin + offsetZoomOut && player.transform.position.y < this.cameraReferences.yMin + offsetZoomIn
            || player.transform.position.y < this.cameraReferences.yMax - offsetZoomOut && player.transform.position.y > this.cameraReferences.yMax - offsetZoomIn)
        {
            return true;
        }
        return false;
    }

    private bool CheckZoomOut(GameObject player)
    {
        if (player.transform.position.x < this.cameraReferences.xMin + offsetZoomOut || player.transform.position.x > this.cameraReferences.xMax - offsetZoomOut
                || player.transform.position.y < this.cameraReferences.yMin + offsetZoomOut || player.transform.position.y > this.cameraReferences.yMax - offsetZoomOut)
        {
            return true;
        }
        return false;
    }

    private bool CheckZoomIn(GameObject player)
    {
        if(player.transform.position.x > this.cameraReferences.xMin + offsetZoomIn || player.transform.position.x < this.cameraReferences.xMax - offsetZoomIn
            || player.transform.position.y > this.cameraReferences.yMin + offsetZoomIn || player.transform.position.y < this.cameraReferences.yMax - offsetZoomIn)
        {
            return true;
        }
        return false;
    }

    private void CalculateCameraReferences()
    {
        height = 2f * camera.orthographicSize;
        width = height * camera.aspect;

        this.cameraReferences.xMin = transform.position.x - width / 2;
        this.cameraReferences.xMax = transform.position.x + width / 2;
        this.cameraReferences.yMin = transform.position.y - height / 2;
        this.cameraReferences.yMax = transform.position.y + height / 2;
    }

    private void ZoomOut()
    {
        if(camera.orthographicSize <= maxZoomValue) 
        {
            camera.orthographicSize += zoomOutSpeed;
            CalculateCameraReferences();
        }
    }

    private void ZoomIn()
    {
        if(camera.orthographicSize >= minZoomValue) 
        {
            camera.orthographicSize -= zoomOutSpeed;
            CalculateCameraReferences();
        }
    }
}
