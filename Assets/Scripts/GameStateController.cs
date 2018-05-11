using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

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
    public AudioMixerSnapshot defaultSnapshot;
    double introLoopBegunTime;

    public Player player { get; private set; }
    [SerializeField]
    private BiomeManager biomeManager;
    private Newspaper newspaper;
    private CameraAnimator camAnim;


	// Use this for initialization
	void Start () {

        float portraitX;
        float portraitY;
        if(Screen.currentResolution.width > Screen.currentResolution.height * 3/4) {
            portraitX = Screen.currentResolution.height * 3/4;
            portraitY = Screen.currentResolution.height;
        }
        else {
            portraitX = Screen.currentResolution.width;
            portraitY = Screen.currentResolution.width * 4/3;
        }

        Screen.SetResolution(Mathf.RoundToInt(portraitX),Mathf.RoundToInt(portraitY),Screen.fullScreen);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        camAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraAnimator>();

        newspaper = FindObjectOfType<Newspaper>();
        if(Singleton == null)
        {
            Singleton = this;
        }

        BeginTitle();
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        switch (gState) {
            case GameState.TITLE:
                player.canMove = false;
                player.canTurn = false;
                // Update block for main titlescreen.
                if (!camAnim.IsTransitioning() && Input.anyKeyDown) {
                    player.StartDisrobe();
                    PlayMainSongDelayed();
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
                mainLoop.volume = Mathf.Max(0, mainLoop.volume - 0.75f * Time.deltaTime);

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
        Singleton.introLoopBegunTime = AudioSettings.dspTime;
		Singleton.defaultSnapshot.TransitionTo(0.0f);
        gState = GameState.TITLE;
    }

    public static void BeginPlaying () {
        //Singleton.mainLoop.volume = 1f;
        //Singleton.mainLoop.Play();
        //Singleton.introLoop.Stop();

        gState = GameState.PLAYING;
    }

    void PlayMainSongDelayed() {
        mainLoop.volume = 1;

        float beatLength = 60f / 170f; // 170 is the bpm
        float barLength = beatLength * 16f;
        float interruptInterval = barLength;

        double curElapsed = AudioSettings.dspTime - introLoopBegunTime;
        double curIntervalPoint = curElapsed % interruptInterval;
        double waitTime = interruptInterval - curIntervalPoint;
        waitTime -= barLength / 16f; //the organ comes in just before the bar

        introLoop.SetScheduledEndTime(AudioSettings.dspTime + waitTime);
        mainLoop.PlayScheduled(AudioSettings.dspTime + waitTime);
    
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
