using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    [SerializeField] float m_speed = 3.0f;

    [SerializeField] float m_RotResponse = 3.0f;

    Vector3 m_direction;

    Vector3 m_targetPosition;

    public GameObject m_upperBounds;
    public GameObject m_lowerBounds;

    float timer = 0.0f;

    private NotifiedHit m_notifiedHit;

	// Use this for initialization
	void Start () {
        newRanDirection();

        newRanTargetLocation();

        m_notifiedHit = GetComponent<NotifiedHit>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (m_notifiedHit.isHit())
        {
            return;
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }

        //timer += Time.deltaTime;


        //DirectionFix();
        //transform.position += m_direction * m_speed * Time.deltaTime;

        m_direction = m_targetPosition - transform.position;

        if (m_direction.magnitude < 1.0f)
        {
            newRanTargetLocation();
        }

        m_direction.Normalize();
        transform.position += m_direction * m_speed * Time.deltaTime;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(m_direction, Vector3.up), Time.deltaTime * m_RotResponse);
		
	}

    void DirectionFix()
    {
        Vector3 testCase = transform.position + (m_direction * m_speed * Time.deltaTime);
        if (testCase.x > m_upperBounds.transform.position.x)
        {
            m_direction.x = Mathf.Min(m_direction.x, -0.1f);
        }
        else
        if (testCase.x < m_lowerBounds.transform.position.x)
        {
            m_direction.x = Mathf.Max(m_direction.x, 0.1f);
        }

        if (testCase.y > m_upperBounds.transform.position.y)
        {
            m_direction.y = Mathf.Min(m_direction.y, -0.1f);
        }
        else
        if (testCase.y < m_lowerBounds.transform.position.y)
        {
            m_direction.y = Mathf.Max(m_direction.y, 0.1f);
        }

        if (testCase.z > m_upperBounds.transform.position.z)
        {
            m_direction.z = Mathf.Min(m_direction.z, -0.1f);
        }
        else
        if (testCase.z < m_lowerBounds.transform.position.z)
        {
            m_direction.z = Mathf.Max(m_direction.z, 0.1f);
        }
    }

    void newRanDirection()
    {
        m_direction = new Vector3(Random.Range(-100.0f, 100.0f), Random.Range(-10.0f, 10.0f), Random.Range(-100.0f, 100.0f));
        m_direction.Normalize();
    }

    void newRanTargetLocation()
    {
        Vector3 lb = m_lowerBounds.transform.position;
        Vector3 ub = m_upperBounds.transform.position;

        m_targetPosition = new Vector3(Random.Range(lb.x, ub.x), Random.Range(lb.y, ub.y), Random.Range(lb.z, ub.z));
    }
}
