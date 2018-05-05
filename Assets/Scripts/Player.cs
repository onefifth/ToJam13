using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public int m_screenwidth { get; private set; }
    public int m_targetPlayerRotation { get; private set; }

    // Use this for initialization
    void Start () {
        m_screenwidth = Screen.width;
        m_targetPlayerRotation = 0;

    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
            if ( Input.mousePosition.x > m_screenwidth / 2.0 ) {
                TurnPlayer(1);      // Right side of screen
            } else {
                TurnPlayer(-1);     // Left side of screen
            }
        }
	}

    // Negative is to the players right
    void TurnPlayer( int direction ) {
        m_targetPlayerRotation += direction;
        m_targetPlayerRotation = Mathf.Clamp(m_targetPlayerRotation, 0, 2);
        
        transform.localRotation = Quaternion.AngleAxis(m_targetPlayerRotation * -25.0f, Vector3.forward);
    }
}
