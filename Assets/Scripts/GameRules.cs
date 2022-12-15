using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GameRules : MonoBehaviour
{
    [SerializeField] private float initial_speed = 5;
    [SerializeField] private float max_speed = 15;

    private GameObject plane;
    private Player player;
    private progessBar progbar;
    private int score = 0;

    [System.NonSerialized]
    public bool game_started = false;

    private TextMeshProUGUI score_text;
    private GameObject start_text;

    void Start(){
        score = 0;

        game_started = false;

        plane = Resources.Load<GameObject>("Prefabs/plane");
        player = GameObject.Find("Player").GetComponent<Player>();

        score_text = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        progbar = GameObject.Find("Slider").GetComponent<progessBar>();

        start_text = GameObject.Find("Start");

        player.speed = 0;

    }


    void Update(){ 
        // Security distance control indirectemetn la vitesses d'apparation des voitures
        if(!game_started && Input.GetKeyDown(KeyCode.Space)){
            game_started = true;
            player.speed = initial_speed;
            start_text.SetActive(false);
            score_text.text = "Score : 0";
            player.transform.position = new Vector3(0,0.35f,0);
            progbar.init();
        }


    }

    public void add_score(int s = 1){
        score = score + s;
        score_text.text = "score : " + score;
        progbar.addValue(1);

        // Increasig the speed value 
        player.speed = Mathf.Min(initial_speed + score/10, max_speed);
    }



    public void game_over(){
        start_text.GetComponent<TextMeshProUGUI>().text = "Perdu \n Appuyez sur espace pour commencer la partie !" ;
        start_text.SetActive(true);

        // Start réinitialise toute les valeurs et mets le boolend gamestarted sur false
        this.Start();


    }

    public bool isPlaying(){
        return game_started;
    }

}
