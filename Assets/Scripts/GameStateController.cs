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
    public static GameStateController Singleton { get; private set; }

    public Player player { get; private set; }
    [SerializeField]
    private BiomeManager biomeManager;
    private Newspaper newspaper;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        newspaper = GameObject.FindObjectOfType<Newspaper>();
        if(Singleton == null)
        {
            Singleton = this;
        }
	}

    // Update is called once per frame
    void Update()
    {
        switch (gState){
            case GameState.TITLE:
                player.canMove = false;
                player.canTurn = false;
                // Update block for main titlescreen.
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    player.StartDisrobe();
                    biomeManager.StartGame();
                    gState = GameState.INTRO;
                }
                break;
            case GameState.INTRO:
                break;
            case GameState.PLAYING:
                break;
            case GameState.NEWSPAPER:
                // Turn the player off.
                // Do camera garbage.
                break;
            default:
                // Nothing.
                break;
        }
    }

    public static void ShowNewspaper() {
		
		print("GO NEWSPAPER");
		Singleton.newspaper.GetComponent<Animator>().SetTrigger("appear");
        gState = GameState.NEWSPAPER;

    }
}
