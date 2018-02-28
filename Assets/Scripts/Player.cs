using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Rigidbody m_rigidbody;

    Animator m_animator;

    [SerializeField] float m_MaxSpeed = 10.0f;

    [SerializeField] float m_walkAccel = 50.0f;

    [SerializeField] float m_dashPower = 30.0f;

    Vector3 m_Force;
    Vector3 m_ForceAdditional;

    float m_attackTime = 0.0f;
    float m_DashTime = 0.0f;
    bool m_Attacking = false;

    bool m_Dashing = false;

    HitBox m_hitbox;
    private NotifiedHit m_notifiedHit;

    [SerializeField] GameObject m_PauseMenu;

    // Use this for initialization
    void Start () {
        m_rigidbody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();

        m_hitbox = GetComponentInChildren<HitBox>();
        m_notifiedHit = GetComponent<NotifiedHit>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_notifiedHit.isHit())
        {
            return;
        }

        m_ForceAdditional = Vector3.zero;

        if (m_Attacking)
        {
            m_attackTime += Time.deltaTime;
            /*
            if (Input.GetButtonDown("Fire1") && !m_animator.GetBool("AttackAgain"))
            {
                m_animator.SetBool("AttackAgain", true);
                m_attackTime = Mathf.Min(m_attackTime, 0.40f);
            }

            if (m_animator.GetBool("AttackAgain") && m_attackTime > 0.50f && m_attackTime < 0.58f)
            {
                m_hitbox.activate();
            }
            */
            if (m_attackTime > 1.0f)
            {
                m_attackTime = 0.0f;
                m_Attacking = false;
                m_hitbox.deactivate();
            }
            else
            {
                m_Force = Vector3.zero;
                return;
            }
        }

        if (m_Dashing)
        {
            m_DashTime += Time.deltaTime;

            if (m_DashTime > 0.5f)
            {
                m_DashTime = 0.0f;
                m_Dashing = false;
            }
        }

        float ForwardAxis = Input.GetAxis("Vertical");
        float HorizontalAxis = Input.GetAxis("Horizontal");

        m_Force = (transform.forward * m_walkAccel * Time.deltaTime) * ForwardAxis;

        float RT = Input.GetAxis("Target");

        if (RT > 0.5f)
        {
            m_Force += (transform.right * m_walkAccel * Time.deltaTime) * HorizontalAxis;
        }
        else
        {
            Quaternion rotation = m_rigidbody.rotation;
            rotation = Quaternion.Euler(0.0f, m_rigidbody.rotation.eulerAngles.y + (230.0f * (Mathf.Clamp(HorizontalAxis, -0.5f, 0.5f))), 0.0f);
            m_rigidbody.rotation = Quaternion.Lerp(m_rigidbody.rotation, rotation,Time.deltaTime);
        }


        if (Input.GetButtonDown("Fire1"))
        {
            m_animator.SetBool("AttackAgain", false);
            m_animator.SetTrigger("Attack");
            m_Attacking = true;
            m_hitbox.activate();
        }

        if (Input.GetButtonDown("Fire3") && !m_Dashing)
        {
            Vector3 direction = new Vector3(HorizontalAxis , 0.0f, ForwardAxis);
            m_ForceAdditional += direction.normalized * m_dashPower;
            m_ForceAdditional += Vector3.up * 4.0f;

            m_Dashing = true;
        }

        /*
        if (Input.GetButton("Fire2"))
        {
            m_animator.SetBool("Blocking", true);
        }
        else
        {
            m_animator.SetBool("Blocking", false);
        }
        */

    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Submit"))
        {
            if (Time.timeScale != 0.0f)
            {
                Time.timeScale = 0.0f;
                m_PauseMenu.SetActive(true);
            }
        }
    }

    private void LateUpdate()
    {

        if (!m_Dashing && !m_notifiedHit.isHit())
        {
            m_rigidbody.AddForce(m_Force, ForceMode.Impulse);

            Vector3 newVelocity = m_rigidbody.velocity;

            newVelocity.x = Mathf.Clamp(newVelocity.x, -m_MaxSpeed, m_MaxSpeed);
            newVelocity.z = Mathf.Clamp(newVelocity.z, -m_MaxSpeed, m_MaxSpeed);

            m_rigidbody.velocity = newVelocity;

            m_animator.SetFloat("Walk", Mathf.Max(Mathf.Abs(newVelocity.x) / m_MaxSpeed, Mathf.Abs(newVelocity.z) / m_MaxSpeed));
        }
        else
        {
            m_rigidbody.AddRelativeForce(m_ForceAdditional, ForceMode.Impulse);

        }
    }
}
