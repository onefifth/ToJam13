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
        DamagePlayer dmg = other.GetComponentInParent<DamagePlayer>();
        if(dmg) {
            if (dmg.Jumpable && player.m_jumping){
                return;
            }
            //print("it has player damage on it");
            playerAnim.TakeHit(other.transform.position, dmg.DamageAmount);
            //player.SetControllable(false);
        }

    }

}
