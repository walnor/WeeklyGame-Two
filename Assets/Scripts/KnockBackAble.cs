using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackAble : MonoBehaviour {

    [SerializeField] bool m_ScaleWithPower = true;
    [SerializeField] float m_knockBackPower = 5;

    public void Knockback(float power, Vector3 direction)
    {
        Rigidbody rg = gameObject.GetComponent<Rigidbody>();

        rg.isKinematic = false;

        if (!m_ScaleWithPower) power = 1;

        rg.AddForce(direction.normalized * power * m_knockBackPower, ForceMode.Impulse);
    }
}
