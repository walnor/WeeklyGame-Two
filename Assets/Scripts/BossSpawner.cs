using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour {

    [SerializeField] GameObject m_bodySegment;
    [SerializeField] GameObject m_Head;

    [SerializeField] int m_bodyNumber = 5;
    [SerializeField] float m_scale = 10.0f;

    [SerializeField] GameObject m_upperBounds;
    [SerializeField] GameObject m_lowerBounds;

    [SerializeField] UI m_ui;

    // Use this for initialization
    void Start ()
    {
        GameObject toAttach = null;

        GameObject theHead = Instantiate(m_Head, transform.position, Quaternion.identity,transform);
        theHead.transform.localScale *= m_scale;

        theHead.GetComponent<Boss>().m_upperBounds = m_upperBounds;
        theHead.GetComponent<Boss>().m_lowerBounds = m_lowerBounds;

        m_ui.GiveBossOBJ(theHead);

        foreach (Transform tr in theHead.GetComponentsInChildren<Transform>())
        {
            if (tr.gameObject.tag == "BBSEnd")
            {
                toAttach = tr.gameObject;
                break;
            }
        }

        for (int i = 0; i < m_bodyNumber; i++)
        {
            GameObject go = Instantiate(m_bodySegment, transform.position - (Vector3.forward * (i + 1)), Quaternion.identity, transform);

            go.transform.localScale *= m_scale / ((float)m_bodyNumber / ((float) m_bodyNumber - i));

            BossBodySegment bbs = go.GetComponent<BossBodySegment>();
            bbs.setTarget(toAttach);

            bbs.GetComponent<Damageable>().Health = 75 *( 1.0f/ ((float)m_bodyNumber / ((float)m_bodyNumber - i)));

            foreach (Transform tr in go.GetComponentsInChildren<Transform>())
            {
                if (tr.gameObject.tag == "BBSEnd")
                {
                    toAttach = tr.gameObject;
                    break;
                }
            }
        }
		
	}
}
