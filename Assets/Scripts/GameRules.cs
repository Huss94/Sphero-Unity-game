using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameRules : MonoBehaviour
{
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private float initial_speed = 5;
    [SerializeField] private float max_speed = 20;


    private float[] lanes = new float[3] {-1.4f, 0, 1.4f};
    private float last_time_spawn = 0;
    private GameObject player;
    private GameObject last_car;
    private float z_spawn_position;
    private float security_distance;
    private int score = 0;
    private float speed = 0;
    private bool game_started = false;

    private TextMeshProUGUI score_text;
    private GameObject start_text;

    void Start(){
        speed = 0;
        score = 0;
        game_started = false;

        player = GameObject.Find("Player");
        score_text = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();

        start_text = GameObject.Find("Start");

        z_spawn_position = player.transform.position.z + 20; 
        last_car = make_cars_spawn();

    }


    void Update(){ 
        // Security distance control indirectemetn la vitesses d'apparation des voitures
        if(Input.GetKeyDown(KeyCode.Space)){
            game_started = true;
            speed = initial_speed;
            start_text.SetActive(false);
            score_text.text = "Score : 0";
        }

        if (game_started){
            security_distance = Random.Range(2,4);
            bool cond = true;

            // Si l'objet last _car a disparu, il y a la sécurité du spawn en fonction du temps
            if(last_car){
                cond = Mathf.Abs(z_spawn_position - last_car.transform.position.z) > security_distance+5;
            }
            else{
                cond = Mathf.Abs(Time.fixedTime - last_time_spawn) > Random.Range(1,3);
            }
            if (cond) {
                last_car = make_cars_spawn();
                last_time_spawn = Time.fixedTime;
            }

        }

    }

    private GameObject make_cars_spawn(){
        int[] x_pos = new int[2]; 
        x_pos = random_car_position();
        GameObject c1 = Instantiate(carPrefab, new Vector3(lanes[x_pos[0]], 0.5f, z_spawn_position), Quaternion.identity);
        GameObject c2 = Instantiate(carPrefab, new Vector3(lanes[x_pos[1]], 0.5f, z_spawn_position), Quaternion.identity);

        return c1;

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

        speed = Mathf.Min(initial_speed + score/25, max_speed);
    }

    public float get_speed(){
        return speed;
    }

    public void game_over(){
        start_text.GetComponent<TextMeshProUGUI>().text = "Perdu \n Appuyez sur espace pour commencer la partie !" ;
        start_text.SetActive(true);
        this.Start();

    }

    public bool isPlaying(){
        return game_started;
    }

}
