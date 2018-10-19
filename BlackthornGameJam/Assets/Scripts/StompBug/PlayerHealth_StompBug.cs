using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth_StompBug : SingletonBehaviour<PlayerHealth_StompBug> {

    public int health;
    public ScreenShake shake;

    public void UpdateHealth(int amount)
    {
        health += amount;
        if (health <= 0)
            Dead();
    }

    private void Dead()
    {
        if (shake != null)
            shake.Shake(0.5f, 0.25f);
    }
}
