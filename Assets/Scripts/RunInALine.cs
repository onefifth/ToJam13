using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunInALine : MonoBehaviour
{

    Player player;
    public float runRate;
    Animator anim;
    //Rigidbody rb;
    public bool active = false;
    public float wakeupRadius;

    public Vector3 runDir; 

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        //rb = GetComponentInChildren<Rigidbody>();
    }

    void Update()
    {
        //anim.SetBool("walking", false);

        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
        else
        {

            if(Vector3.Distance(transform.position, player.transform.position) < wakeupRadius) 
            {
                active = true;    
            }
            else {

                active = false;
            }

            if (active)
            {
                Vector3 walkDir = runDir.normalized;
                Vector3 walkVec = walkDir * runRate * Time.deltaTime;
                transform.position += walkVec;

                anim.SetBool("walking", true);
                float xDir = walkDir.x;
                if(xDir < 0) {
                    xDir = -1.0f;
                }
                print(xDir);
                anim.SetFloat("yWalk", xDir);
				anim.SetFloat("xWalk", walkDir.x);


            }
        }

    }

    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeGameObject == gameObject)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, wakeupRadius);
        }   
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, runDir);

    }
}