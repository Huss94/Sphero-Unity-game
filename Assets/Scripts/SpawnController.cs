using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class SpawnController : MonoBehaviour
{

    [System.NonSerialized] public int[] lanes = new int[3] {-1, 0, 1};
    private Object wall; 
    private Object coin;
    private GameRules gr;
    private GameObject p;
    private Queue<string> queue = new Queue<string>();
    private List<List<string>> Patterns = new List<List<string>>(); 
    private float last_z_spawn = 0;

    private int min_spawn_distance = 5;

    private bool has_begun = false;


    

    void Start()
    {
        Pattern_creation(Patterns);

        wall = Resources.Load("Prefabs/Wall");
        coin = Resources.Load("Prefabs/Coin");
        gr = GameObject.Find("Game rules").GetComponent<GameRules>();
        p = GameObject.Find("Player");

    }


    // Update is called once per frame
    void Update()
    {
        if (gr.game_started){
            if (!has_begun){
                first_spawning();
                has_begun = true;
            }
            make_wall_spawn();
        }
        else{
            has_begun = false;

        }
    }

    void first_spawning(){
        for (int i = 20; i < 55; i+=5){
            make_wall_spawn(i);
        }
    }

    public void make_wall_spawn(int spawn_offset = 50){
        float z_spawn = p.transform.position.z + spawn_offset;
        if (queue.Count < 1){
            int id = Random.Range(0, Patterns.Count); 
            enqueue_pattern_in_queue(queue, Patterns[id]);
            min_spawn_distance = 10;

        }


        if (Mathf.Abs(z_spawn - last_z_spawn) > min_spawn_distance){
            // Test si la queue n'est pas vide
            string s = queue.Dequeue();

            Spawn(s, z_spawn);
            last_z_spawn = z_spawn;
            min_spawn_distance = 5;
        }


    }




    Object Spawn(string s, float z, float y = 0.5f){
        if (s.Length != 3){
            Debug.LogError("Bad spawning shape");
            return null;
        }

        int val;
        Object c = null;
        for(int i = 0; i < s.Length ; i++){
        
            val = int.Parse(s[i].ToString());
            if (val == 1){
                Vector3 v = new Vector3(lanes[i], y+0.5f, z);
                c = Instantiate(wall, v, Quaternion.Euler(-90,0,0));

            }
            else{

                Vector3 v = new Vector3(lanes[i], y, z);
                c = Instantiate(coin, v, Quaternion.Euler(0,90,90));

            }

        }
        return c;
    }

    private void enqueue_pattern_in_queue(Queue<string> queue, List<string> pattern){
        foreach(var s in pattern){
            queue.Enqueue(s);
        }

    }

    void Pattern_creation(List<List<string>> pattern){
        // CREATION DES PATTTERNS DE JEU
        List<string> p1 = new List<string>(); 
        p1.Add("011"); p1.Add("101"); p1.Add("110"); p1.Add("011");

        List<string> p2 = new List<string>(); 
        p2.Add("110"); p2.Add("011"); p2.Add("110"); p2.Add("101");

        List<string> p3 = new List<string>(); 
        p3.Add("101"); p3.Add("011"); p3.Add("101"); p3.Add("110");

        pattern.Add(p1);
        pattern.Add(p2);
        pattern.Add(p3);


    }

}

