using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEvent : MonoBehaviour {

    [SerializeField] GameObject m_player;

    [SerializeField] GameObject m_Cam;
    [SerializeField] GameObject m_UI;
    [SerializeField] GameObject m_LoseText;

    [SerializeField] GameObject[] m_btnToDisable;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (m_player == null)
        {
            m_Cam.SetActive(true);
            m_UI.SetActive(true);
            m_LoseText.SetActive(true);

            foreach (GameObject obj in m_btnToDisable)
            {
                obj.SetActive(false);
            }
        }		
	}
}
