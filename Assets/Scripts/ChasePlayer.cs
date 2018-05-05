using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour {

    Player player;
    public float runRate;
    public float chaseRadius;
    Animator anim;
    Rigidbody rb;

	private void Start()
	{
        anim = GetComponentInChildren<Animator>();
        rb = GetComponentInChildren<Rigidbody>();
	}

    void Update()
    {
        anim.SetBool("walking", false);

        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
        else
        {
            Vector3 toPlayer = new Vector3(player.transform.position.x,0,player.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z);

            if (toPlayer.magnitude < chaseRadius)
            {
                print("chase player");
                Vector3 walkDir = toPlayer.normalized;
                Vector3 walkVec = walkDir * runRate * Time.deltaTime;
                transform.position += walkVec;

                anim.SetBool("walking", true);
                anim.SetFloat("xWalk", walkDir.x);
                anim.SetFloat("yWalk", walkDir.z);

            }

        }
    }

	private void OnDrawGizmos()
	{
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
	}
}
