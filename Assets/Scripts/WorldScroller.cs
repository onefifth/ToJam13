using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScroller : MonoBehaviour {

    public static Vector3 s_movementDirection;

    // Use this for initialization
    void Start () {
		
	}

    void FixedUpdate() {

    }

    // Update is called once per frame
    void Update () {
        //Debug.Log(s_movementDirection);
        foreach ( Transform child in transform ) {
            child.localPosition += s_movementDirection * Time.deltaTime;
        }
    }
}
