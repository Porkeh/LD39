using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkController : MonoBehaviour {


    float elapsedTime;
    SpriteRenderer sr;
    public Sprite shark01;
    public Material shark01m;
    public Sprite shark02;
    public Material shark02m;

    enum Direction { up,down,left,right};
    Direction dir;

    Vector2 initialVelocity;
	// Use this for initialization
	void Start () {

        elapsedTime = 0;
        sr = GetComponent<SpriteRenderer>();

        initialVelocity = new Vector2(0, 0);

        dir = Direction.right;
		
	}
	
	// Update is called once per frame
	void Update () {

        elapsedTime += Time.deltaTime;

        if(elapsedTime > 0.5)
        {
            Animate();
            elapsedTime = 0;
        }

        MoveShark();
		
	}

    private void MoveShark()
    {

        switch(dir)
        {
            case Direction.right:
                initialVelocity.x = 1;
                initialVelocity.y = 0;

                break;
            case Direction.left:
                initialVelocity.x = -1;
                initialVelocity.y = 0;
                break;
            case Direction.up:
                initialVelocity.x = 0;
                initialVelocity.y = 1;
                break;
            case Direction.down:
                initialVelocity.x = 0;
                initialVelocity.y = -1;
                break;
        }

        transform.position += new Vector3(initialVelocity.x,initialVelocity.y,0) * Time.deltaTime ;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "Player")
        {

        }
        else
        {
            switch (dir)
            {
                case Direction.right:
                    dir = Direction.up;
                    this.transform.position.Set(transform.position.x - 0.1f, transform.position.y, transform.position.z);

                    break;
                case Direction.left:
                    dir = Direction.down;
                    this.transform.position.Set(transform.position.x + 0.1f, transform.position.y, transform.position.z);
                    break;
                case Direction.up:
                    dir = Direction.left;
                    if (sr != null)
                    {
                        sr.flipX = !sr.flipX;
                    }
                    this.transform.position.Set(transform.position.x, transform.position.y - 0.1f, transform.position.z);
                    break;
                case Direction.down:
                    if (sr != null)
                    {
                        sr.flipX = !sr.flipX;
                    }
                    dir = Direction.right;
                    this.transform.position.Set(transform.position.x, transform.position.y + 0.1f, transform.position.z);
                    break;
            }
        }


    }

    void Animate()
    {
        if(sr.sprite == shark01)
        {
            sr.sprite = shark02;
            sr.material = shark02m;
        }
        else
        {
            sr.sprite = shark01;
            sr.material = shark01m;
        }
       
    }
}
