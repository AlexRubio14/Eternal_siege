using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private enum CameraMovement { NONE, ZOOM_IN, ZOOM_OUT };

    [SerializeField] private CameraMovement camState = CameraMovement.NONE;


    [Header("Players"), SerializeField] private List<Collider> players;

    [Space, Header("Cameras"), SerializeField] private Camera insideCamera;
    [SerializeField] private Camera externalCamera;

    [Header("Cameras Variables"), SerializeField, Range(0, 1)] private float movementSpeed;
    [SerializeField] private float zoomOutSpeed;
    [SerializeField] private float zoomInSpeed;
    [SerializeField] private float XZSpeed;

    [Header("Zoom"), SerializeField] private float maxZoomValue;
    private float minZoomValue = 0.0f;
    private float currentZoomValue;
    [SerializeField] private float zoomValueSpeed;

    [SerializeField] private Transform midPointTransform;
    private Vector3 distanceFromMidPointToCamera;

    private void Awake()
    {
        //Cuando los players se spawneen antes se hara asi

        //foreach(GameObject player in PlayersManager.instance.GetPlayersList())
        //{
        //    Collider collider = player.GetComponent<Collider>();
        //    players.Add(collider);
        //}
        distanceFromMidPointToCamera = transform.position - midPointTransform.position;
        currentZoomValue = minZoomValue;
    }

    public void AddPlayer(GameObject _newPlayer)
    {
        players.Add(_newPlayer.GetComponent<CapsuleCollider>());
    }
    public void RemovePlayer(GameObject _removablePlayer)
    {
        players.Remove(_removablePlayer.GetComponent<CapsuleCollider>());
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayersManager.instance.GetPlayersList().Count == 0)
            return;


        CheckCamState();
        MoveFromMidPoint();
        MoveCamera();
    }

    private void CheckCamState()
    {
        bool zoomIn = true;
        bool zoomOut = false;
        foreach (Collider item in players)
        {
            Plane[] camFrustrum = GeometryUtility.CalculateFrustumPlanes(externalCamera);
            if (!GeometryUtility.TestPlanesAABB(camFrustrum, item.bounds))
            {
                //Si esta fuera de la camara exterior alejamos la cam
                zoomOut = true;
            }
            else
            {
                //Si esta dentro de la camara
                camFrustrum = GeometryUtility.CalculateFrustumPlanes(insideCamera);

                //Comprueba que no haya ningun player en algun borde
                //(lo miramos comprobando que esten dentro del frustrum de la camara interior)
                if (!GeometryUtility.TestPlanesAABB(camFrustrum, item.bounds))
                {
                    //Si no hay ningun player fuera de la camara interna hacer ZOOM_IN 
                    zoomIn = false;
                }
            }
        }


        if (zoomOut)
        {
            camState = CameraMovement.ZOOM_OUT;
        }
        else if (zoomIn)
        {
            camState = CameraMovement.ZOOM_IN;
        }
        else
        {
            camState = CameraMovement.NONE;
        }
    }

    private void MoveCamera()
    {
        Vector3 destinyPos = transform.position;
        if (camState != CameraMovement.NONE)
        {
            float zoomSpeed;
            switch (camState)
            {
                case CameraMovement.ZOOM_IN:
                    if (currentZoomValue > minZoomValue)
                    {

                        currentZoomValue -= zoomValueSpeed;
                        zoomSpeed = -zoomInSpeed;
                        distanceFromMidPointToCamera += -transform.forward * zoomSpeed * Time.deltaTime;
                        destinyPos = midPointTransform.position + distanceFromMidPointToCamera;

                        Vector3 finalPos = Vector3.Lerp
                        (
                        transform.position,
                        destinyPos,
                        movementSpeed * Time.fixedDeltaTime
                        );

                        transform.position = finalPos;
                    }
                    break;

                case CameraMovement.ZOOM_OUT:
                    if (currentZoomValue < maxZoomValue)
                    {
                        currentZoomValue += zoomValueSpeed;
                        zoomSpeed = zoomOutSpeed;
                        distanceFromMidPointToCamera += -transform.forward * zoomSpeed * Time.deltaTime;
                        destinyPos = midPointTransform.position + distanceFromMidPointToCamera;

                        Vector3 finalPos = Vector3.Lerp
                        (
                        transform.position,
                        destinyPos,
                        movementSpeed * Time.fixedDeltaTime
                        );

                        transform.position = finalPos;
                    }
                    break;

                default:
                    break;
            }
        }
    }

    private void MoveFromMidPoint()
    {
        Vector3 destinyPos = midPointTransform.position + distanceFromMidPointToCamera;

        Vector3 finalPos = Vector3.Lerp(
            transform.position,
            destinyPos,
            2 * Time.deltaTime
            );

        transform.position = finalPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
    }

    public void AddPlayerIntoList(Collider collider)
    {
        players.Add(collider);
    }

    public Transform GetMidPoint()
    {
        return midPointTransform;
    }
    
}
