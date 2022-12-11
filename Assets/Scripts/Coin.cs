using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float speed = 300;
    private GameRules gr;
    private GameObject p;
    private float y_init;
    private AudioClip coin_sound;

    private AudioSource audiosrc;
    private AudioClip coin_audio;

    // Start is called before the first frame update
    void Start()
    {
        
        gr = GameObject.Find("Game rules").GetComponent<GameRules>();
        p = GameObject.Find("Player");
        y_init = transform.position.y;
        coin_sound =  Resources.Load<AudioClip>("audio/coin_sound");
        audiosrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        shouldDestroy();
        rotationAnimation();

        if (Input.GetKeyDown(KeyCode.T) ){
            Debug.Log("sound lpay");
            audiosrc.PlayOneShot(coin_sound);
        }


    }

    public IEnumerator collectedAnimation(){
        
        for (float y = y_init; y < y_init+0.5; y+=0.1f){
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
            yield return null;
        }
        this.gameObject.GetComponent<Renderer>().enabled = false;
        while(audiosrc.isPlaying){
            yield return null;
        }
        Destroy(this.gameObject);


    }
    private void rotationAnimation(){
        Vector3 currentRotation = transform.rotation.eulerAngles + new Vector3(0,1,0)*Time.deltaTime * speed;
        Quaternion s = Quaternion.identity; 
        s.eulerAngles = currentRotation;

        transform.rotation = s;
    }

    private void shouldDestroy(){
        if (!gr.game_started | transform.position.z - p.transform.position.z < -2){
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")){
            if (!other.gameObject.GetComponent<Player>().steel_mode){
                gr.add_score();
                audiosrc.Play();
                StartCoroutine("collectedAnimation");
            }
            else{
                gr.game_over();
        }
     }
    }
}
