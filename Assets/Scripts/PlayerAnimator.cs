using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    private Player player;

    public Animator anim;
    bool idle = false;
    float walkRate = 2.0f;
    float hitDistance = 2.0f;

	// Use this for initialization
	void Start () {
        player = transform.parent.GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update () {
        anim.SetFloat("xWalk", player.m_actualPlayerDirection.x);
        anim.SetFloat("yWalk", player.m_actualPlayerDirection.z);

        anim.speed = Mathf.Clamp(player.m_runSpeed * 0.4f, 1, 4);

        if (idle) {
            anim.SetBool("walking", player.m_runSpeed > 1f);
        }

        //if(Input.GetKey(KeyCode.E))
        //{
        //    TakeHit(player.m_actualPlayerDirection.x);
        //}

	}

    public void StartDisrobe() {
        anim.SetTrigger("disrobe");
    }

    public void TakeHit(Vector3 damageOrigin)
    {
        if (!player.isDead) {
            anim.SetTrigger("hit");
            anim.SetFloat("hitX", transform.position.x - damageOrigin.x);
            idle = false;
            player.isDead = true;
        }

        //add motion
    }

    //called by SMB_Idle upon entering idle animation
    public void OnIdle() {
        idle = true;
    }

    //called upon exiting Disrobe animation
    public void DropTrou() {
        GameObject clothes = (GameObject)Instantiate(Resources.Load("PlayerClothes"), transform.position, transform.rotation);
        clothes.transform.localScale = transform.lossyScale;
        player.OnDisrobeComplete();
    }
}
