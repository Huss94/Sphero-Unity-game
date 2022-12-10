using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class SpawnController : MonoBehaviour
{

    [System.NonSerialized] public int[] lanes = new int[3] {-1, 0, 1};
    private Object prefab; 
    private GameRules gr;
    private GameObject p;
    private Queue<string> queue = new Queue<string>();
    private List<List<string>> Patterns = new List<List<string>>(); 
    private float last_z_spawn = 0;

    private int min_spawn_distance = 5;


    

    void Start()
    {
        Pattern_creation(Patterns);

        prefab = Resources.Load("Prefabs/Wall");
        gr = GameObject.Find("Game rules").GetComponent<GameRules>();
        p = GameObject.Find("Player");

    }


    // Update is called once per frame
    void Update()
    {
        make_wall_spawn();
    }

    public void make_wall_spawn(){
        float z_spawn = p.transform.position.z + 50;
        if (queue.Count < 1){
            int id = Random.Range(0, Patterns.Count); 
            enqueue_pattern_in_queue(queue, Patterns[id]);
            min_spawn_distance = 10;

        }


        if (Mathf.Abs(z_spawn - last_z_spawn) > min_spawn_distance){
            // Test si la queue n'est pas vide
            string s = queue.Dequeue();

            Spawn(prefab, s, z_spawn);
            last_z_spawn = z_spawn;
            min_spawn_distance = 5;
        }

    }




    Object Spawn(Object pref, string s, float z, float y = 0.5f){
        if (s.Length != 3){
            Debug.LogError("Bad spawning shape");
            return null;
        }

        int val;
        Object c = null;
        for(int i = 0; i < s.Length ; i++){
        
            val = int.Parse(s[i].ToString());
            if (val == 1){
                Vector3 v = new Vector3(lanes[i], y, z);
                c = Instantiate(pref, v, Quaternion.identity);

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
        p1.Add("011"); p1.Add("101"); p1.Add("110");

        List<string> p2 = new List<string>(); 
        p2.Add("110"); p2.Add("011"); p2.Add("110"); p2.Add("101");

        List<string> p3 = new List<string>(); 
        p3.Add("101"); p3.Add("011"); p3.Add("101"); p3.Add("110");

        pattern.Add(p1);
        pattern.Add(p2);
        pattern.Add(p3);


    }

}

