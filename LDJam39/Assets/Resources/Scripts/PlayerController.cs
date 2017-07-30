using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

   private  Rigidbody2D myBody;

    public TextManager tm;
    public List<GameObject> guibombs;
    public GameObject bomb;
    public Light light;
    public float speed;
    public int bombCharge;
    public bool immune = true;
    bool acceptInput;
    bool gameStarted = false;
    float elapseTime;
    float elapsedTime;

    public Sprite sub01;
    public Material sub01m;
    public Sprite sub02;
    public Material sub02m;


    void HandleInput()
    {
        if (Input.GetKey(KeyCode.A))
        {

            myBody.angularVelocity = 180 ;
            

         




        }
        else if (Input.GetKey(KeyCode.D))
        {

            myBody.angularVelocity = -180 ;
            
        }





        if (Input.GetKey(KeyCode.W))
        {
            if (myBody.velocity.x < 10)
            {
                myBody.AddForce(transform.right * -speed );
            }

        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (myBody.velocity.x > -10)
            {

                myBody.AddForce(transform.right * speed );

            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (bombCharge > 0)
            {
                SpriteRenderer s = this.GetComponent<SpriteRenderer>();
                Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y, 0);
                pos = pos - transform.right;

                Instantiate(bomb, pos, Quaternion.identity);
                bombCharge--;
                Destroy(guibombs[bombCharge].gameObject);
            }
        }
        
        
    }


	// Use this for initialization
	void Start () {

        myBody = GetComponent<Rigidbody2D>();
        elapseTime = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if (gameStarted)
        {
            if (Input.anyKey && acceptInput)
            {
                HandleInput();
            }

            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {

                myBody.angularVelocity = 0;
            }

            if (immune)
            {
                elapseTime += Time.deltaTime;

                if (elapseTime > 1)
                {
                    immune = false;
                    elapseTime = 0;
                }
            }
        }
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 0.5)
        {
            Animate();
            elapsedTime = 0;
        }


    }

    public void Animate()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr.sprite == sub01)
        {
            sr.sprite = sub02;
            sr.material = sub02m;
        }
        else
        {
            sr.sprite = sub01;
            sr.material = sub01m;
        }
    }

    public void ReduceLight()
    {
        if (!immune && gameStarted)
        {
            light.spotAngle = light.spotAngle - 20;
            immune = true;
            GetComponent<AudioSource>().Play();
            tm.PlayerHit();
        }
        if(light.spotAngle < 5)
        {
            tm.GameOver();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hostile")
        {
            ReduceLight();
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.tag == "Win")
        {
            tm.Win();
        }
    }

    public void GameStart()
    {
        gameStarted = true;
    }
    
    public void SetInput(bool input)
    {
        acceptInput = input;
    }
}
