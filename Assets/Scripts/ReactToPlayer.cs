using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactToPlayer : MonoBehaviour {
    Player player;
    Animator anim;
    public float reactRadius;

	// Use this for initialization
	void Start () {
        anim = GetComponentInChildren<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
        else
        {
            Vector3 toPlayer = new Vector3(player.transform.position.x, 0, player.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z);

            if (toPlayer.magnitude < reactRadius)
            {
                DoReaction();
            }
        }
	}

    void DoReaction() {

        anim.SetTrigger("kick");
    }
}
