using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector : MonoBehaviour {

    Player player;
    PlayerAnimator playerAnim;
   
    private void Start()
    {
        player = GetComponent<Player>();
        playerAnim = GetComponentInChildren<PlayerAnimator>();
    }

	public void OnTriggerEnter (Collider other) 
    {
        //print("detected hit");
        if(other.GetComponentInParent<DamagePlayer>())
        {
            //print("it has player damage on it");
            playerAnim.TakeHit(other.transform.position);
            player.SetControllable(false);
        }

    }

}
