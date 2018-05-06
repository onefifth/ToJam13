using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    TITLE = 1,
    INTRO, 
    PLAYING,
    NEWSPAPER,
    ENDING,
    CREDITS,
}

public class GameStateController : MonoBehaviour {

    public static GameState gState = GameState.TITLE;
    static GameStateController singleton;

    private Player player;
    private Newspaper newspaper;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        newspaper = GameObject.FindObjectOfType<Newspaper>();
        if(singleton == null)
        {
            singleton = this;
        }

	}

    // Update is called once per frame
    void Update()
    {
        switch (gState)
        {
            case GameState.TITLE:
                player.canMove = false;
                player.canTurn = false;
                // Update block for main titlescreen.
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    player.StartDisrobe();
                }
                break;
            case GameState.INTRO:
                break;
            case GameState.PLAYING:
                break;
            case GameState.NEWSPAPER:

                break;
            default:
                // Nothing.
                break;
        }
    }

    public static void ShowNewspaper() {
    
		print("GO NEWSPAPER");
		singleton.newspaper.GetComponent<Animator>().SetTrigger("appear");
        gState = GameState.NEWSPAPER;

    }
}
