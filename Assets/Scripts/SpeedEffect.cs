using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEffect : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform t_player;
    void Start()
    {
        t_player =GameObject.Find("Player").GetComponent<Transform>();

        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = t_player.position + new Vector3(0,0,0.7f);

    }
}
