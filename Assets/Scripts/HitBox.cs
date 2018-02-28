using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour {

    [SerializeField] GameObject m_ObjectOwner;

    [SerializeField] [Range(1.0f, 100.0f)] float m_power = 10;

    [SerializeField] string m_TagTarget = null;

    bool m_Active = false;

    public void activate()
    {
        m_Active = true;
    }

    public void deactivate()
    {
        m_Active = false;
    }

    public bool isActive()
    {
        return m_Active;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!m_Active)
            return;

        Hitable h_other = other.GetComponent<Hitable>();

        if (h_other)
        {
            if (m_TagTarget != null || m_TagTarget != "")
            {
                if (h_other.m_ObjectOwner != m_ObjectOwner && h_other.m_ObjectOwner.tag == m_TagTarget)
                {
                    print("hit " + other.name + "!");
                    h_other.takeHit(m_power, this);
                    m_Active = false;
                }
            }
            else
            if (h_other.m_ObjectOwner != m_ObjectOwner)
            {
                print("hit " + other.name + "!");
                h_other.takeHit(m_power, this);
                m_Active = false;
            }
        }
    }
}
