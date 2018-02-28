using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour {

    public float Health = 100;

    public void injury(float power)
    {
        Health -= power;

        if (Health <= 0.0f)
        {
            print("Death");

            Destroy(gameObject);
        }
    }
}
