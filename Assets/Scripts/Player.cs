using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [System.NonSerialized] public float speed = 0;
    [System.NonSerialized] public bool steel_mode;
    public Texture steel;
    public Texture gold;

    private GameRules gamerules;
    private Rigidbody rb;
    private Vector3 desired_position;
    private AudioSource audiosource;
    private Renderer p_renderer;

    // private Color gold_color = new Color(0.59f,0.4f,0.03f,1f);
    // private Color steel_color = new Color(0.356f,0.345f,0.314f,1f);


    // AUDIO 
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

        steel_mode = false;
        p_renderer = GetComponent<Renderer>();
        // p_renderer.material.SetColor("_Color", gold_color);
        

    }

    // Update is called once per frame
    void Update()
    {



        if (Input.GetKeyDown(KeyCode.A) | Input.GetKeyDown(KeyCode.J)){
            desired_position = Vector3.left;
            audiosource.PlayOneShot(pff);
        }

        if (Input.GetKeyDown(KeyCode.Z) | Input.GetKeyDown(KeyCode.K)){
            desired_position = Vector3.zero;
            audiosource.PlayOneShot(snare);
        }

        if (Input.GetKeyDown(KeyCode.E) | Input.GetKeyDown(KeyCode.L)){
            desired_position = Vector3.right;
            audiosource.PlayOneShot(tss);
        }

        if (gamerules.game_started && Input.GetKeyDown(KeyCode.Space)){
            changeMode();
        }


        if (Input.GetKeyDown(KeyCode.LeftArrow)){
            desired_position = compute_desired_pos(Vector3.left);
            
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)){
            desired_position = compute_desired_pos(Vector3.right);
        }

        // if (Input.GetKeyDown(KeyCode.DownArrow)){
        //     changeMode();
        // }



        Vector3 smooth_mvmt = Vector3.Lerp(transform.position, desired_position, 20*Time.deltaTime);
        transform.position = new Vector3(smooth_mvmt.x, transform.position.y, transform.position.z);
        
    }

    void FixedUpdate(){

        if (gamerules.game_started){
            // On ne cherche pas un mouvement réalise, on veut seulement que la sphere roule.
            float s; 
            if (steel_mode){
                s = speed*1.5f;
            }
            else{
                s = speed;
            }


            rb.velocity = new Vector3(rb.velocity.x,rb.velocity.y,s);

            // Debug.Log("Speed : " + s);
        }

    }

    void changeMode(){
        if (steel_mode){
            // p_renderer.material.SetColor("_Color", gold_color);
            p_renderer.material.SetTexture("_MainTex", gold);
            steel_mode = false;
        }

        else{

            // p_renderer.material.SetColor("_Color", steel_color);
            p_renderer.material.SetTexture("_MainTex", steel);
            steel_mode = true;
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

        

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Wall")){
            if (!steel_mode){
                gamerules.game_over();
            }
            else{
                other.gameObject.GetComponent<Wall>().explode();
                gamerules.add_score();

            }
        }
        
    }
}
