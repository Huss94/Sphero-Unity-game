using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 cam_offset = new Vector3(0, 1, -3f);
    private GameRules gamerules;

    void Start()
    {
        transform.position = new Vector3(0f,0.5f, 0f);
        gamerules = GameObject.Find("Game rules").GetComponent<GameRules>();
        

        GameObject cam = GameObject.Find("Main Camera");
        cam.transform.position = transform.position + cam_offset;
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x >=0){
            transform.position += new Vector3(-1.4f, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x <=0){
            transform.position += new Vector3(1.4f, 0f, 0);
        }
        
    }
        
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Car")){
            gamerules.game_over();
        }
        
    }
}
