using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {


    public Button btn;
    public Button btn2;
    // Use this for initialization
    void Start () {

        btn = btn.GetComponent<Button>();
        btn.onClick.AddListener(OnStart);
        btn2 = btn2.GetComponent<Button>();
        btn2.onClick.AddListener(OnClose);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnStart()
    {
        SceneManager.LoadScene(1);
    }

    void OnClose()
    {
        Debug.Log("Close");
        Application.Quit();
    }
}
