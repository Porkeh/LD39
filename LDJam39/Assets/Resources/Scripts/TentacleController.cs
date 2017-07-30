using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleController : MonoBehaviour {

    SpriteRenderer sr;

   

    float elapseTime;

	// Use this for initialization
	void Start () {

        sr = GetComponent<SpriteRenderer>();
        elapseTime = 0;
		
	}
	
	// Update is called once per frame
	void Update () {

        elapseTime += Time.deltaTime;

        if(elapseTime > 0.5)
        {
            sr.flipX = !sr.flipX;
            elapseTime = 0;
        }
	}
}
