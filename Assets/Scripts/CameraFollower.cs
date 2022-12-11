using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{

    private GameObject Player;
    private Player p;
    private Transform p_pos;
    private Vector3 cam_offset = new Vector3(0, 1, -3f);
    private Camera cam;

    void Start()
    {
        Player = GameObject.Find("Player");
        p = Player.GetComponent<Player>();
        p_pos = Player.GetComponent<Transform>();
        cam = GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        Follow_ball();

        ChangeFov();
    }

    void ChangeFov(){
        if (p.steel_mode){
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 75, 5*Time.deltaTime);
        }
        else{

            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 60, 5*Time.deltaTime);
        }

    }


    void Follow_ball(){

        // Vector3 desired_x = new Vector3(p_pos.position.x, 0,0);
        // Vector3 curr_x = new Vector3(transform.position.x, 0, 0);

        Vector3 player_yz = new Vector3(0, p_pos.position.y, p_pos.position.z);


        // Vector3 smoothed_x = Vector3.Lerp(curr_x, desired_x, 20*Time.deltaTime);
        // transform.position = player_yz + cam_offset +smoothed_x;

        transform.position = cam_offset + player_yz;

    }

}
