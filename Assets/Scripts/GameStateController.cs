using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public AudioSource introLoop;
    public AudioSource mainLoop;

    public Player player { get; private set; }
    [SerializeField]
    private BiomeManager biomeManager;
    private Newspaper newspaper;
    private CameraAnimator camAnim;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        camAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraAnimator>();

        newspaper = GameObject.FindObjectOfType<Newspaper>();
        if(Singleton == null)
        {
            Singleton = this;
        }

        BeginTitle();
	}

    // Update is called once per frame
    void Update() {
        switch (gState) {
            case GameState.TITLE:
                player.canMove = false;
                player.canTurn = false;
                // Update block for main titlescreen.
                if (!camAnim.IsTransitioning() && Input.anyKeyDown) {
                    player.StartDisrobe();
                    gState = GameState.INTRO;
                    camAnim.CurrentViewpoint = 2;
                }
                break;
            case GameState.INTRO:
                //debug skip disrobing
                //if (Input.anyKeyDown)
                //{
                //player.StartDisrobe();
                //}
                break;
            case GameState.PLAYING:
                break;
            case GameState.NEWSPAPER:
                mainLoop.volume = Mathf.Max(0, mainLoop.volume - 0.1f * Time.deltaTime);

                if (!camAnim.IsTransitioning() && Input.GetKeyUp(KeyCode.Space)) {
                    Singleton.player.ResetPlayer();
                    biomeManager.StartGame();
                    newspaper.transform.parent.parent = null;
                    BeginTitle();
                }
                break;
            case GameState.CREDITS:
                if ((!camAnim.IsTransitioning() && (Input.GetButtonDown("Jump") || Input.GetButtonDown("Run"))) || Input.GetKeyUp(KeyCode.R)) {
                    Scene scene = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(scene.name);
                }
                break;
            default:
                // Nothing.
                break;
        }
    }

    public static void BeginTitle() {
        Singleton.camAnim.CurrentViewpoint = 0;
        Singleton.mainLoop.volume = 0f;
        Singleton.introLoop.Play();
        gState = GameState.TITLE;
    }

    public static void BeginPlaying () {
        Singleton.mainLoop.volume = 1f;
        Singleton.mainLoop.Play();
        Singleton.introLoop.Stop();

        gState = GameState.PLAYING;
    }

    public static void ShowNewspaper() {
        Singleton.player.SetControllable(false);
        Singleton.newspaper.ShowNews();
    }

    public static void OnNewspaperShown() {
        Singleton.camAnim.CurrentViewpoint = 1;
        gState = GameState.NEWSPAPER;
    }

    public void winGame() {
        player.SetControllable(false);
        gState = GameState.CREDITS;
        Singleton.player.ResetPlayer();
        camAnim.CurrentViewpoint = 3;
    }
}
