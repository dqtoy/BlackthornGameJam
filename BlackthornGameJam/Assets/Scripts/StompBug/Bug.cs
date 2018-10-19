using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour {
    
    public int health;
    public Color color;
    public GameObject deathEffect;
    public GameObject attackedEffect;

    public void TakeDamage(int damage) {
        //if (attackedEffect != null)
        //    Instantiate(attackedEffect, transform.position, Quaternion.identity);
        health -= damage;
        if (health <= 0)
            Dead();
    }

    private void Dead()
    {
        if (deathEffect != null)
        {
            GameObject effectObj = Instantiate(deathEffect, transform.position, Quaternion.identity);
            UpdateParticleColor(effectObj);
        }
        Destroy(gameObject);
    }

    private void UpdateParticleColor(GameObject effectObj)
    {
        ParticleSystem.MainModule main = effectObj.GetComponent<ParticleSystem>().main;
        main.startColor = color;

        foreach (Transform child in effectObj.transform)
        {
            ParticleSystem ps = child.GetComponent<ParticleSystem>();
            if (ps == null)
                continue;
            
            main = ps.main;
            main.startColor = color;
        }
    }
}
