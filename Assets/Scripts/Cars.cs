using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cars : MonoBehaviour
{
    // speed in m/s
    private GameRules gamerules;
    private float speed;

    void Start(){

        gamerules = GameObject.Find("Game rules").GetComponent<GameRules>();
    }


    void FixedUpdate() {
        speed = gamerules.GetComponent<GameRules>().get_speed();
        transform.position += new Vector3(0f, 0f,  -speed*Time.deltaTime);

        if (transform.position.z < -4){
            gamerules.add_score();
            Destroy(this.gameObject);
        }

        if (!gamerules.isPlaying()){
            Destroy(this.gameObject);

        }

    }


}
