using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitable : MonoBehaviour
{
    public GameObject m_ObjectOwner;

    public void takeHit(float power, HitBox hit)
    {
        KnockBackAble nba = m_ObjectOwner.GetComponent<KnockBackAble>();

        if (nba)
        {
            Vector3 dir = m_ObjectOwner.transform.position - hit.transform.position;
            nba.Knockback(power, dir);
        }

        KnockUpAble nua = m_ObjectOwner.GetComponent<KnockUpAble>();

        if (nua)
        {
            Vector3 dir = m_ObjectOwner.transform.position - hit.transform.position;
            nua.Knockback(power);
        }

        Damageable da = m_ObjectOwner.GetComponent<Damageable>();

        if (da)
        {
            da.injury(power);
        }

        NotifiedHit not = m_ObjectOwner.GetComponent<NotifiedHit>();

        if (not)
        {
            not.notifyHit();
        }
    }
}
