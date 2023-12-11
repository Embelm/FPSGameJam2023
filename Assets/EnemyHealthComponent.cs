using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthComponent : MonoBehaviour
{

   [SerializeField] private int maxHealth;
    private int currentHealth;
    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }


    public void DoDamage(int dmg)
    {
        currentHealth -= dmg;
        CheckHealth();
    }

    private void CheckHealth()
    {
        if(currentHealth <= 0)
        {
            isDead = true;
        }
    }

}
