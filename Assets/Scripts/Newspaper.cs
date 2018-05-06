using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        "GHOSTS ARE REAL"

    };

    int shouldReset = 0;

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

    void RandomizeText () {

        if(text == null){
            text = GetComponentInChildren<Text>();   
        }
        text.text = headlines[Random.Range(0, headlines.Length)];

    }
}
