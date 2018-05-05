using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public int m_screenwidth { get; private set; }
    public Vector3 m_targetPlayerDirection;
    public Vector3 m_actualPlayerDirection;

    private WorldScroller p_worldScroller;

    public bool m_running { get; private set; }
    public bool m_dashing { get; private set; }
    public bool m_fakeDash { get; private set; }
    public float m_steering { get; private set; }
    public float m_runSpeed { get; private set; }

    public float m_breakingFriction { get; private set; }

    private float m_lastDash { get; set; }

    [SerializeField]
    private float MaxSpeed;

    [SerializeField]
    private AnimationCurve DashAccelCurve;
    [SerializeField]
    private float DashDuration;
    [SerializeField]
    private float DashSpeed;


    // Use this for initialization
    void Start () {
        m_breakingFriction = 0.8f;
        m_screenwidth = Screen.width;
        p_worldScroller = GameObject.FindGameObjectWithTag("World").GetComponent<WorldScroller>();
    }

    void FixedUpdate() {
        // Clamped input direction
        m_targetPlayerDirection = Vector3.zero;
        m_steering = Mathf.Clamp(Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")), -1, 1);
        m_targetPlayerDirection.z = Mathf.Clamp(Mathf.RoundToInt(Input.GetAxisRaw("Vertical")), 0, 1);
        m_running = Input.GetButton("Run");

        if (m_targetPlayerDirection.sqrMagnitude > 0f) {
            m_targetPlayerDirection = Quaternion.AngleAxis(35f * m_steering, Vector3.up) * m_targetPlayerDirection;
        } else {
            m_targetPlayerDirection.z = Mathf.Abs(m_steering);
            m_targetPlayerDirection = Quaternion.AngleAxis(85f * m_steering, Vector3.up) * m_targetPlayerDirection;
        }

        if (Input.GetButtonDown("Run")) {
            if (m_targetPlayerDirection.sqrMagnitude > 0f) {
                m_lastDash = Time.realtimeSinceStartup;
                m_dashing = true;
                m_fakeDash = false;
            } else {
                m_fakeDash = m_runSpeed < 0.5f;
            }
        }

        m_runSpeed += m_targetPlayerDirection.magnitude * 4f * Time.fixedDeltaTime;

        if (m_running) {
            m_runSpeed *= 1.5f;
        }

        if (m_targetPlayerDirection.sqrMagnitude <= 0f) {
            m_runSpeed *= m_breakingFriction;
        }

        m_runSpeed = Mathf.Clamp(m_runSpeed, 0f, MaxSpeed);

        if ( m_dashing && ( Time.realtimeSinceStartup - m_lastDash ) < DashDuration) {
            m_runSpeed *= DashSpeed * (DashAccelCurve.Evaluate((Time.realtimeSinceStartup - m_lastDash) / DashDuration) + 1f);
            //m_steering *= DashSpeed * 0.8f * (DashAccelCurve.Evaluate((Time.realtimeSinceStartup - m_lastDash) / DashDuration) + 1f);
        } else {
            m_dashing = false;
        }

        m_actualPlayerDirection = m_targetPlayerDirection;
    }

    // Update is called once per frame
    void Update () {
        Debug.DrawRay(transform.position, m_targetPlayerDirection * m_runSpeed, Color.red);

        //WorldScroller.s_movementDirection.x = m_steering;
        //WorldScroller.s_movementDirection.z = -m_runSpeed;

        //transform.localRotation = Quaternion.AngleAxis(m_targetPlayerDirection.x * -35.0f, Vector3.forward);
    }
}
