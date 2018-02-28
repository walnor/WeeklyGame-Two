using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    [SerializeField] Text m_PlayerHealth;
    [SerializeField] Text m_BossHealth;

    [SerializeField] GameObject m_Player;
    [SerializeField] GameObject m_Boss;

	
	// Update is called once per frame
	void Update ()
    {
        if (m_Player)
        {
            m_PlayerHealth.text = "Health: " + m_Player.GetComponent<Damageable>().Health;
        }

        if (m_Boss)
        {
            m_BossHealth.text = "Boss Health: " + m_Boss.GetComponent<Damageable>().Health;
        }
        else
        {
            m_BossHealth.text = "Dead...*cry*";
        }
    }

    public void GiveBossOBJ(GameObject boss)
    {
        m_Boss = boss;
    }
}
