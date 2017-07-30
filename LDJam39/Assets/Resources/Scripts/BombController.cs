using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour {

    List<GameObject> terminated;

    GameObject player;
    public GameObject particleBomb;

    Rigidbody2D myBody;

    float time;
    float elapsedTime;
    bool exploding = false;
	// Use this for initialization
	void Start () {

        myBody = GetComponent<Rigidbody2D>();
        terminated = new List<GameObject>();
        time = Time.deltaTime;
        elapsedTime = 0;

        player = GameObject.FindGameObjectWithTag("Player");

        myBody.AddForce(-player.transform.right*100);

       
    
		
	}
	
	// Update is called once per frame
	void Update () {


        Debug.Log(terminated.Count);
        elapsedTime += Time.deltaTime;
        if(elapsedTime  > 2)
        {
            exploding = true;
            foreach(GameObject o in terminated)
            {
                Destroy(o);
               
            }
            Instantiate(particleBomb, this.transform.position,Quaternion.identity);
            Destroy(this.gameObject);

        }
	}


    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collide");
        GameObject other = collision.gameObject;
        if(other.tag != "Player" && other.tag != "Indestructable")
        {
            terminated.Add(other);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!exploding)
        {
            if (terminated.Contains(collision.gameObject))
            {
                terminated.Remove(collision.gameObject);
            }
        }
    }
}
