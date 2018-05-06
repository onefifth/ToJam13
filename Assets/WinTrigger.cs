using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponentInParent<Player>() != null) {
            GameStateController.Singleton.winGame();
        }
    }
}
