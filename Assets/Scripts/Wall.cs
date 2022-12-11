using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // Start is called before the first frame update
    private GameRules gr;
    private GameObject p;

    void Start()
    {
        gr = GameObject.Find("Game rules").GetComponent<GameRules>();
        p = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {
        shouldDestroy();
    }

    private void shouldDestroy(){
        if (!gr.game_started | transform.position.z - p.transform.position.z < -2){
            Destroy(this.gameObject);
        }
    }

    public void explode(){

        var shatered = Resources.Load("Prefabs/shatered_wall");
        var shatered_wall = Instantiate(shatered, transform.position, Quaternion.identity) as GameObject;

        // foreach(Transform t in shatered_wall.transform){
        //     var rb = t.GetComponent<Rigidbody>();
        //     if (rb != null){
        //         rb.AddExplosionForce(300, transform.position, 10);
        //         Debug.Log("Exploeded");
        //     }
            

        // }


        Destroy(gameObject);
    }


}
