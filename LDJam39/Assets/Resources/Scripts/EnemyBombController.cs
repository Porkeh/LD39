using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBombController : MonoBehaviour {

    float elsapsedTime;
    bool detonating;
    Vector3 playerPos;
    Vector3 velocity;
    bool playerSafe;
    GameObject player;
    public GameObject particleBomb;
    // Use this for initialization
    void Start () {

        elsapsedTime = 0;
        detonating = false;
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
        if(detonating)
        {
            this.transform.position += velocity * 0.5f * Time.deltaTime;
            elsapsedTime += Time.deltaTime;
            Debug.Log("Detonating");
            if(elsapsedTime > 2)
            {
                if(!playerSafe)
                {
                    player.GetComponent<PlayerController>().ReduceLight();
                    
                }
                Instantiate(particleBomb, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (!detonating)
            {
                detonating = true;
                GetComponent<AudioSource>().Play();
                playerPos = collision.gameObject.transform.position;
                velocity = playerPos - transform.position;
                GetComponent<CircleCollider2D>().radius = 1.0f;
                
            }
            playerSafe = false;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerSafe = true;
        }
    }

}
