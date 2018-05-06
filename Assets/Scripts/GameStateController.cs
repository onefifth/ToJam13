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

    private Player player;
    [SerializeField]
    private BiomeManager biomeManager;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

	// Update is called once per frame
	void Update () {
		switch(gState) {
            case GameState.TITLE:
                player.canMove = false;
                player.canTurn = false;
                // Update block for main titlescreen.
                if (Input.GetKeyUp(KeyCode.Space)) {
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
}
