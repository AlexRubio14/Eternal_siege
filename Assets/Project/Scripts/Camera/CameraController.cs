using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Camera camera;



    private void Awake()
    {
        camera = GetComponent<Camera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private bool CheckIfPlayersAreOutOfCamera()
    {
        foreach(GameObject player in PlayersManager.instance.GetPlayersList())
        {
            if(player.transform.position.x < camera.transform.position.x)
            {

            }
                
        }

        return true;
    }

}
