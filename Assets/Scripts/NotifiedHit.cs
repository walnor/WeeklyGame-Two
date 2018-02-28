using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifiedHit : MonoBehaviour {

    bool hit = false;

    float m_timer = 0.0f;
    [SerializeField] float m_TimeDelay = 0.5f;
	
	// Update is called once per frame
	void Update () {

        if (!hit)
        {
            return;
        }

        m_timer += Time.deltaTime;

        if (m_timer > m_TimeDelay)
        {
            m_timer = 0.0f;
            hit = false;
        }
		
	}

    public void notifyHit()
    {
        hit = true;
    }

    public bool isHit()
    {
        return hit;
    }
    
}
