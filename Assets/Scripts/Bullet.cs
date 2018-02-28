using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    HitBox m_hit;

    public float m_speed = 1.0f;
    [SerializeField] float m_hight = 10.0f;

    Vector3 m_targetPosition = Vector3.zero;

    bool m_hightReached = false;

    float ranXup;
    float ranZup;

    // Use this for initialization
    void Start () {

        m_hit = GetComponent<HitBox>();

        ranXup = Random.Range(-4.0f, 4.0f);
        ranZup = Random.Range(-4.0f, 4.0f);

    }
	
	// Update is called once per frame
	void Update () {

        if (!m_hightReached)
        {
            m_hightReached = floatUp();
            return;
        }

        if ((transform.position - m_targetPosition).magnitude <= 0.15f)
            Destroy(gameObject);

        FlyAtPlayer();
		
	}

    bool floatUp()
    {
        if (transform.position.y >= 10.0f)
        {
            foreach (Collider co in Physics.OverlapSphere(transform.position, 1000.0f))
            {
                if (co.gameObject.tag == "Player")
                {
                    m_targetPosition = co.gameObject.transform.position;
                }
            }
            m_hit.activate();
            return true;
        }
        Vector3 moveTo = Vector3.up;
        moveTo.x += ranXup;
        moveTo.z += ranZup;
        transform.position += moveTo * Time.deltaTime * m_speed;

        return false;

    }

    void FlyAtPlayer()
    {
        Vector3 dir = m_targetPosition - transform.position;
        dir.Normalize();

        transform.position += dir * Time.deltaTime * m_speed;

        if (!m_hit.isActive())
        {
            Destroy(gameObject);
        }

    }
}
