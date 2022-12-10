using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [System.NonSerialized] public float speed = 0;

    private GameRules gamerules;
    private Rigidbody rb;
    private Vector3 desired_position;
    private AudioSource audiosource;
    private AudioClip snare;
    private AudioClip tss;
    private AudioClip pff;

    void Start()
    {

        audiosource = GetComponent<AudioSource>();
        snare = Resources.Load<AudioClip>("audio/snare");
        tss =  Resources.Load<AudioClip>("audio/tss");
        pff =  Resources.Load<AudioClip>("audio/pff");

        transform.position = new Vector3(0f,0.35f, 0f);
        gamerules = GameObject.Find("Game rules").GetComponent<GameRules>();

        rb = GetComponent<Rigidbody>();
        desired_position = transform.position;
        

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow)){
            desired_position = compute_desired_pos(Vector3.left);
            Debug.Log("desired " + desired_position);
            
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)){

            desired_position = compute_desired_pos(Vector3.right);

        }

        Vector3 smooth_mvmt = Vector3.Lerp(transform.position, desired_position, 20*Time.deltaTime);
        transform.position = new Vector3(smooth_mvmt.x, transform.position.y, transform.position.z);
        
    }

    void FixedUpdate(){

        if (gamerules.game_started){
            // rb.AddForce(new Vector3(0,-9.8f,0), ForceMode.Acceleration);
            // rb.MovePosition(transform.position + Vector3.forward * speed * Time.deltaTime);
            // rb.AddForce(new Vector3(0,0,speed));

            // On ne cherche pas un mouvement réalise, on veut seulement que la sphere roule.
            rb.velocity = new Vector3(rb.velocity.x,rb.velocity.y,speed);
            Debug.Log(rb.velocity);

        }

    }

    Vector3 compute_desired_pos(Vector3 mv_direction){
        Vector3 ds_pos = desired_position;
        if (transform.position.x < -0.5){
            if (transform.position.x + mv_direction.x > -0.5){
                ds_pos = Vector3.zero;
                audiosource.PlayOneShot(snare);

            } 
        }

        else if (Mathf.Abs(transform.position.x) < 0.5 ){
            ds_pos = mv_direction;
            if (mv_direction.x > 0){
                audiosource.PlayOneShot(tss);
            }
            else {
                audiosource.PlayOneShot(pff);
            }
        }

        if (transform.position.x > 0.5){

            if (transform.position.x + mv_direction.x < 0.5){
                ds_pos = Vector3.zero;
                audiosource.PlayOneShot(snare);


            }
        }

        return ds_pos;
    }

        
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Wall")){
            gamerules.game_over();
        }
        
    }
}
