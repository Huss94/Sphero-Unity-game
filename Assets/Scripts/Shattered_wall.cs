using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shattered_wall : MonoBehaviour
{
    private GameRules gr;
    private GameObject p;
    private AudioSource audiosrc;

    void Start()
    {
        gr = GameObject.Find("Game rules").GetComponent<GameRules>();
        p = GameObject.Find("Player");

        audiosrc = GetComponent<AudioSource>();
        audiosrc.Play();
    }

    // Update is called once per frame
    void Update()
    {
        shouldDestroy();
    }

    private void shouldDestroy(){
        if ((!gr.game_started | transform.position.z - p.transform.position.z < -6) && !audiosrc.isPlaying){
            Destroy(this.gameObject);
        }
    }

}
