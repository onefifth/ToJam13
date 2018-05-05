using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public int m_screenwidth { get; private set; }
    public Vector3 m_targetPlayerDirection;
    public Vector3 m_actualPlayerDirection;
    public Vector3 m_smoothPlayerDirection;

    private float m_turnSpeed;
    private WorldScroller p_worldScroller;

    public bool m_running { get; private set; }
    public bool m_dashing { get; private set; }
    public bool m_fakeDash { get; private set; }
    public float m_steering { get; private set; }
    public float m_runSpeed { get; private set; }

    public float m_brakingFriction { get; private set; }

    private float m_lastDash { get; set; }

    [SerializeField]
    private float MaxSpeed;

    [SerializeField]
    private AnimationCurve DashAccelCurve;
    [SerializeField]
    private float DashDuration;
    [SerializeField]
    private float DashSpeed;

    [SerializeField]
    private float DiagonalAngle = 35f;
    [SerializeField]
    private float SidewaysAngle = 85f;


    // Use this for initialization
    void Start () {
        m_brakingFriction = 0.8f;
        m_smoothPlayerDirection = Vector3.forward;
        m_actualPlayerDirection = Vector3.forward;
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
            m_targetPlayerDirection = Quaternion.AngleAxis(DiagonalAngle * m_steering, Vector3.up) * m_targetPlayerDirection;
        } else {
            m_targetPlayerDirection.z = Mathf.Abs(m_steering);
            m_targetPlayerDirection = Quaternion.AngleAxis(SidewaysAngle * m_steering, Vector3.up) * m_targetPlayerDirection;
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

        if (m_targetPlayerDirection.sqrMagnitude <= 0f) {
            m_runSpeed *= m_brakingFriction;
        }

        m_runSpeed = Mathf.Clamp(m_runSpeed, 0f, MaxSpeed);

        if (m_dashing && (Time.realtimeSinceStartup - m_lastDash) < DashDuration) {
            m_runSpeed *= DashSpeed * (DashAccelCurve.Evaluate((Time.realtimeSinceStartup - m_lastDash) / DashDuration) + 1f);
            //m_steering *= DashSpeed * 0.8f * (DashAccelCurve.Evaluate((Time.realtimeSinceStartup - m_lastDash) / DashDuration) + 1f);
        } else {
            if (m_running) {
                m_runSpeed *= 1.5f;
            }
            m_dashing = false;
        }

        m_turnSpeed = Mathf.Clamp(MaxSpeed * 1.5f - m_runSpeed, 1f, 50f) * 2f * Time.fixedDeltaTime;

        Debug.Log(m_smoothPlayerDirection);
        m_smoothPlayerDirection = Vector3.RotateTowards(m_smoothPlayerDirection, m_targetPlayerDirection, m_turnSpeed, 0.0f);

        float snappedAngle = Vector3.Angle(Vector3.forward, m_smoothPlayerDirection);
        float absAngle = Mathf.Abs(snappedAngle);
        float sign = Mathf.Sign(Vector3.Dot(m_smoothPlayerDirection, Vector3.right));

        Debug.Log(snappedAngle);

        if (absAngle < DiagonalAngle * 0.5f) {
            snappedAngle = 0f;
        } else if (absAngle < ((SidewaysAngle - DiagonalAngle) * 0.5f + DiagonalAngle)) {
            snappedAngle = DiagonalAngle * sign;
        } else {
            snappedAngle = SidewaysAngle * sign;
        }

        m_actualPlayerDirection = Quaternion.AngleAxis(snappedAngle, Vector3.up) * Vector3.forward;

        //m_actualPlayerDirection = m_smoothPlayerDirection;
    }

    // Update is called once per frame
    void Update () {
        Debug.DrawRay(transform.position, m_actualPlayerDirection * m_runSpeed, Color.green);
        Debug.DrawRay(transform.position, m_smoothPlayerDirection * m_runSpeed * 0.8f, Color.blue);
        Debug.DrawRay(transform.position, m_targetPlayerDirection * m_runSpeed * 0.5f, Color.red);
        

        //WorldScroller.s_movementDirection.x = m_steering;
        //WorldScroller.s_movementDirection.z = -m_runSpeed;

        //transform.localRotation = Quaternion.AngleAxis(m_targetPlayerDirection.x * -35.0f, Vector3.forward);
    }
}
