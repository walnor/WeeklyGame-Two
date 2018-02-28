using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBodySegment : MonoBehaviour {

    [SerializeField] GameObject m_target;
    [SerializeField] GameObject m_lookTo;
    [SerializeField] float m_response = 1.0f;

    [SerializeField] float m_minDistance = 0.2f;

    [SerializeField] float m_bulletSpawnChance = 0.3f;

    [SerializeField] float m_spawnTime = 1.0f;

    float m_bElipsedTime = 0.0f;

    [SerializeField] GameObject m_Bullet;


    private NotifiedHit m_notifiedHit;

    private void Start()
    {
        m_notifiedHit = GetComponent<NotifiedHit>();
    }

    // Update is called once per frame
    void Update () {

        if (m_target && m_target.gameObject.tag != "BBSEnd")
            m_target = null;

        if (m_target == null)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().useGravity = true;

            GetComponent<Rigidbody>().freezeRotation = false;

            GameObject toBreak = null;

            foreach (Transform tr in GetComponentsInChildren<Transform>())
            {
                if (tr.gameObject.tag == "BBSEnd")
                {
                    toBreak = tr.gameObject;
                    toBreak.tag = "Untagged";
                    break;
                }
            }

            Vector3 deathKnockback = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
            deathKnockback.Normalize();

            deathKnockback *= 20;

            GetComponent<Rigidbody>().AddForce(deathKnockback, ForceMode.Impulse);

            Destroy(this);
            return;
        }

        spawnBullet();

        if (m_notifiedHit.isHit())
        {
            GetComponent<Rigidbody>().isKinematic = false;
            return;
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }

        if (m_target)
        {
            if((m_target.transform.position - transform.position).magnitude > m_minDistance)
                transform.position = Vector3.Lerp(transform.position, m_target.transform.position, Time.deltaTime * m_response);


            Vector3 targetDir = transform.position - m_lookTo.transform.position;

            float step = 3 * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
            Debug.DrawRay(transform.position, newDir, Color.red);
            transform.rotation = Quaternion.LookRotation(newDir);

            //transform.LookAt(m_target.transform);

        }		
	}

    private void spawnBullet()
    {
        m_bElipsedTime += Time.deltaTime;

        if (m_bElipsedTime >= m_spawnTime)
        {
            m_bElipsedTime = 0.0f;


            if (Random.Range(0.0f, 1.0f) <= m_bulletSpawnChance)
            {
                GameObject go = Instantiate(m_Bullet);
                go.transform.position = transform.position + (Vector3.up * 2);
                go.GetComponent<Bullet>().m_speed = Random.Range(5.0f, 25.0f);
            }
        }

    }

    public void setTarget(GameObject go)
    {
        m_target = go;
        m_lookTo = go;
    }
}
