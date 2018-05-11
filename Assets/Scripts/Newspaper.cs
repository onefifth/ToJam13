using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Newspaper : MonoBehaviour {

    Transform cameraParent;
    Vector3 originalPosition;
    Quaternion originalRotation;
    Vector3 originalScale;
    Animator newsAnimator;
    public Text text;
    string[] headlines = {
        "THIS JUST IN: YOU SUCK",
        "YOUNG REBEL DEFIES CONVENTION",
        "HUMAN STREAKER GETS CARRIED AWAY",
        "CLOTHES FOUND IN PARKING LOT: RAPTURE HAS STARTED?",
        "RED TEAM LOSES",
        "GHOSTS ARE REAL",
        "STREAKING HERO APPREHENDED",
        "YOUNG DUMMY GETS NAKED",
        "FOUND: JORTS",
        "PARKING LOT MAYHEM",
        "HARMLESS FUN HAD IN PARKING LOT"

    };

    int shouldReset = 0;
    public AudioSource sfxSpin1;
    public AudioSource sfxSpin2;
    public AudioSource sfxAppeared;
    Coroutine spinAudioRoutine;

    public AudioMixerSnapshot endgameSnapshot;

    // Use this for initialization
    void Start () {
        RandomizeText();
        newsAnimator = GetComponent<Animator>();
        cameraParent = transform.parent.parent;
        originalPosition = transform.parent.localPosition;
        originalRotation = transform.parent.localRotation;
        originalScale = transform.parent.localScale;
    }
    
    // Update is called once per frame
    void Update () {
        if (shouldReset > 0) {
            --shouldReset;
            if (shouldReset == 0) {
                transform.parent.SetParent(cameraParent);
                transform.parent.localPosition = originalPosition;
                transform.parent.localRotation = originalRotation;
                transform.parent.localScale = originalScale;
            }
        }
    }

    public void ResetNewspaper() {
        RandomizeText();
        newsAnimator.SetTrigger("hide");
        shouldReset = 2;
    }

    public void ShowNews() {
        ResetNewspaper();
        newsAnimator.SetTrigger("appear");
    }

    public void StartSpinSFX() {
		endgameSnapshot.TransitionTo(3.0f);
        spinAudioRoutine = StartCoroutine(DoSpinAudio());
    }

    IEnumerator DoSpinAudio() {

        sfxSpin1.Play();
        float elapsed = 0;
        bool spin2on = false;

        while (true) {

            float tVolume = Mathf.Min(elapsed / 1.0f, 1.0f);
            sfxSpin1.volume = Mathf.Lerp(0, 1, tVolume);
            sfxSpin2.volume = Mathf.Lerp(0, 1, tVolume);

            elapsed += Time.deltaTime;


            if(elapsed > 0.25f & !spin2on) {
                sfxSpin2.Play();
                spin2on = true;
            }
            yield return null;
        }
    }

    public void StopSpinSFX () {
        StopCoroutine(spinAudioRoutine);
        sfxSpin1.Stop();
        sfxSpin2.Stop();
        sfxAppeared.Play();
    }

    void RandomizeText () {

        if(text == null){
            text = GetComponentInChildren<Text>();   
        }
        text.text = headlines[Random.Range(0, headlines.Length)];

    }
}
