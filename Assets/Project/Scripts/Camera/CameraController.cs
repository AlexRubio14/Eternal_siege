using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Camera camera;

    private float width = 0f;
    private float height = 0f;

    //Camera positions references
    struct CameraReferences
    {
        public float xMin;
        public float xMax;
        public float yMin;
        public float yMax;
    }

    private CameraReferences cameraReferences;

    private float minZoomValue;

    [SerializeField]
    private float maxZoomValue;

    [SerializeField]
    private float offsetBetweenPlayerAndCamera;

    [SerializeField]
    private float zoomOutSpeed;




    private void Awake()
    {
        minZoomValue = camera.orthographicSize;

        CalculateCameraReferences();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayersManager.instance.GetPlayersList().Count == 0)
            return;

        if(CheckIfPlayersAreOutOfCamera())
        {
            ZoomOut();
        }
        else
        {
            ZoomIn();
        }
    }

    private bool CheckIfPlayersAreOutOfCamera()
    {
        foreach(GameObject player in PlayersManager.instance.GetPlayersList())
        {
            if (player.transform.position.x < this.cameraReferences.xMin + offsetBetweenPlayerAndCamera || player.transform.position.x > this.cameraReferences.xMax - offsetBetweenPlayerAndCamera
                || player.transform.position.y < this.cameraReferences.yMin + offsetBetweenPlayerAndCamera || player.transform.position.y > this.cameraReferences.yMax - offsetBetweenPlayerAndCamera)
            {
                return true;
            }
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
