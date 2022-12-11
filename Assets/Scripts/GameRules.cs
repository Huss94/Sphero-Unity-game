using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GameRules : MonoBehaviour
{
    [SerializeField] private float initial_speed = 5;
    [SerializeField] private float max_speed = 15;

    private Player player;
    private int score = 0;

    [System.NonSerialized]
    public bool game_started = false;

    private TextMeshProUGUI score_text;
    private GameObject start_text;

    void Start(){
        score = 0;

        game_started = false;

        player = GameObject.Find("Player").GetComponent<Player>();

        score_text = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        start_text = GameObject.Find("Start");

        player.speed = 0;

    }


    void Update(){ 
        // Security distance control indirectemetn la vitesses d'apparation des voitures
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("Game_started = Tre");
            game_started = true;
            player.speed = initial_speed;
            start_text.SetActive(false);
            score_text.text = "Score : 0";
            player.transform.position = new Vector3(0,0.35f,0);
        }

        if (game_started){

        if(Input.GetKeyDown(KeyCode.V)){
            player.speed  = player.speed + 5;
        }


        if(Input.GetKeyDown(KeyCode.M)){
            player.speed  = player.speed - 5;
        }



        }

    }

    private GameObject make_cars_spawn(){
        int[] x_pos = new int[2]; 
        x_pos = random_car_position();
        // GameObject c1 = Instantiate(carPrefab, new Vector3(lanes[x_pos[0]], 0.5f, z_spawn_position), Quaternion.identity);
        // GameObject c2 = Instantiate(carPrefab, new Vector3(lanes[x_pos[1]], 0.5f, z_spawn_position), Quaternion.identity);

        // return c1;
        return this.gameObject;

    }

    private int[] random_car_position(){
        int[] i = new int[] {0,1,2};
        var list_ind = new List<int>(i);
        int[] res = new int[2];

        res[0] = list_ind[Random.Range(0,list_ind.Count)];
        list_ind.RemoveAt(res[0]);
        res[1] = list_ind[Random.Range(0,list_ind.Count)];

        return res;
    }



    public void add_score(int s = 1){
        score = score + s;
        score_text.text = "score : " + score;

        // Increasig the speed value 
        player.speed = Mathf.Min(initial_speed + score/5, max_speed);
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
