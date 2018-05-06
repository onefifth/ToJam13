using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Newspaper : MonoBehaviour {

    Transform cameraParent;
    Vector3 originalPosition;
    Quaternion originalRotation;
    Vector3 originalScale;
    Animator newsAnimator;

    // Use this for initialization
    void Start () {
        newsAnimator = GetComponent<Animator>();

        cameraParent = transform.parent.parent;
        originalPosition = transform.parent.localPosition;
        originalRotation = transform.parent.localRotation;
        originalScale = transform.parent.localScale;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResetNewspaper() {
        newsAnimator.SetTrigger("hide");
        transform.parent.SetParent(cameraParent);
        transform.parent.localPosition = originalPosition;
        transform.parent.localRotation = originalRotation;
        transform.parent.localScale = originalScale;
    }

    public void ShowNews() {
        ResetNewspaper();
        newsAnimator.SetTrigger("appear");
    }
}
