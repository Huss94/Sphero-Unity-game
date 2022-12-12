// CE SCRIPT SERT A SUPPRIMER ET CREER LES PLANES POUR QU'ON AIT L'IMPRESSION QUE LE TERRAIN SOIT INFINI
using UnityEngine;

public class PlaneBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private bool new_plane_created = false;
    private GameObject plane_prefab;
    private GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        plane_prefab = Resources.Load<GameObject>("Prefabs/plane");
    }

    // Update is called once per frame
    void Update()
    {

        // On ne détruit pas le plan principal 
        if (transform.name != "Principal Plane"){
            shouldDestroy();
        }

        if (!new_plane_created){
            shouldCreate();
        }
        
    }

    void shouldDestroy(){

        if (player.transform.position.z - transform.position.z > 10*transform.localScale.z/2 + 100){
            Destroy(this.gameObject);
        }
    }

    void shouldCreate(){
        if (player.transform.position.z - transform.position.z > 10*transform.localScale.z/2  -200){
            float z_pos = transform.position.z + 10*transform.localScale.z;
            Instantiate(plane_prefab, new Vector3(0,0,z_pos), Quaternion.identity);
            new_plane_created = true;
            
        }
    }
}

