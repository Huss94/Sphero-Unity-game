using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGround : MonoBehaviour
{
    public float yspeed = 1;
    private MeshRenderer mesh;
    private GameRules gamerules;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        gamerules = GameObject.Find("Game rules").GetComponent<GameRules>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gamerules.isPlaying()){
            // transform.position += new Vector3(0, 0, yspeed*Time.deltaTime);
            mesh.material.mainTextureOffset += new Vector2(0, yspeed*Time.deltaTime);
        }
    }
}
