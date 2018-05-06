using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour {

    Player player;
    public float runRate;
    public float chaseRadius;
    public float tackleRadius;
    Animator anim;
    Rigidbody rb;
    public bool idle = true;

    [SerializeField]
    private AnimationCurve tackleAccelCurve;
    [SerializeField]
    private float tackleDuration;
    [SerializeField]
    private float tackleSpeed;
    bool tackling;
    float lastTackle;
    Vector3 tackleDir;

    public AudioSource sfxJump;
    public AudioSource sfxLand;


	private void Start()
	{
        anim = GetComponentInChildren<Animator>();
        rb = GetComponentInChildren<Rigidbody>();
	}

    void Update()
    {
        anim.SetBool("walking", false);

        if (idle)
        {
            if (player == null)
            {
                player = FindObjectOfType<Player>();
            }
            else
            {
                Vector3 toPlayer = new Vector3(player.transform.position.x, 0, player.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z);

                if (toPlayer.magnitude < tackleRadius)
                {
                    StartTackle();
                }
                else if (toPlayer.magnitude < chaseRadius)
                {
                    //print("chase player");
                    Vector3 walkDir = toPlayer.normalized;
                    Vector3 walkVec = walkDir * runRate * Time.deltaTime;
                    transform.position += walkVec;

                    anim.SetBool("walking", true);
                    anim.SetFloat("xWalk", walkDir.x);
                    anim.SetFloat("yWalk", walkDir.z);

                }

            }
        }
        else if (tackling)
        {
            Vector3 tackleVec = tackleDir * (tackleSpeed * (tackleAccelCurve.Evaluate((Time.realtimeSinceStartup - lastTackle) / tackleDuration))) * Time.deltaTime;
            transform.position += tackleVec;
        }

    }

    void StartTackle() 
    {
        idle = false;
        anim.SetTrigger("tackle");


    }

    public void PlayLandSFX() 
    {
        if(sfxLand != null)
        {
            sfxLand.Play();
        }


    }

	public void DoTackle()
    {
        if(player != null)
        {
            SpriteRenderer sr = anim.GetComponent<SpriteRenderer>();
            Vector3 toPlayer = (new Vector3(player.transform.position.x, 0, player.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z)).normalized;

            float angleToPlayer = Vector3.SignedAngle(Vector3.forward, toPlayer, Vector3.up);
            sr.transform.eulerAngles = new Vector3(sr.transform.eulerAngles.x, sr.transform.eulerAngles.y, -angleToPlayer);

            tackleDir = toPlayer;
            anim.SetFloat("yWalk", toPlayer.z);

            lastTackle = Time.realtimeSinceStartup;
            tackling = true;

            if (sfxJump != null)
            {
                sfxJump.Play();
            }
        }

	}

	private void OnDrawGizmos()
	{
        if (UnityEditor.Selection.activeGameObject == gameObject) {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, chaseRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, tackleRadius);
        }
	}
}