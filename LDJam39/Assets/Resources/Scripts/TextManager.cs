using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour {

    List<string> Dialogues = new List<string>();
    public GameObject text;
    public PlayerController player;
    Text gText;
    float elapsedTime = 0;
    bool intro = true;
    bool gameOver = false;
    int index = 0;
	// Use this for initialization
	void Start () {

        Random.InitState(System.DateTime.Now.Millisecond);
        gText = text.GetComponent<Text>();

        Dialogues.Add("Oh no! We're stuck down here in a badly drawn submarine and need to get out of here!");
        Dialogues.Add("We have to make our way to the surface! Before the light runs out!");
        Dialogues.Add("Yes! And avoid all those very strange looking enemies and bombs, if we get hit the light will go down!");
        Dialogues.Add("Let's get a move on then! And don't forgot those lazily re-used bombs that we can deploy if needed!");

        Dialogues.Add("Ouch! Watch the paint job!");
        Dialogues.Add("Hey! Who turned off the lights!");
        Dialogues.Add("Is this undersea turbulance?!");
        Dialogues.Add("Where did you learn to drive?");
        Dialogues.Add("You know who else has this much damage? My mom!");
	}
	
	// Update is called once per frame
	void Update () {

        elapsedTime += Time.deltaTime;

        if (intro)
        {
            player.SetInput(false);
            gText.text = Dialogues[index];
            
            if (elapsedTime > 2.5)
            {
                index++;
                elapsedTime = 0;
            }
            if(index == 4)
            {
                player.GameStart();
                player.SetInput(true);
                intro = false;
               
            }
        }

        if(elapsedTime > 3)
        {
            text.transform.parent.gameObject.SetActive(false);

            if(gameOver)
            {
                SceneManager.LoadScene(0);
            }
        }

       


    }

    public void Win()
    {
        text.transform.parent.gameObject.SetActive(true);
        gText.text = "Wow! We actually made it out! Don't quit the day job with driving like that though...";
        elapsedTime = 0;
        gameOver = true;
    }

    public void GameOver()
    {
        text.transform.parent.gameObject.SetActive(true);
        gText.text = "Oh no! I can't see anything! This is the end! Thanks for nothing!";
        elapsedTime = 0;
        gameOver = true;
    }

    public void PlayerHit()
    {
        text.transform.parent.gameObject.SetActive(true);
        int i = Random.Range(4, 9);
        gText.text = Dialogues[i];

        elapsedTime = 0;
    }
}
