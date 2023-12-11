using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    enum EnemyState
    {
        Alive,
        Dead
    }

    public Transform target;
    private NavMeshAgent navMeshAgent;

    EnemyState enemyState;
    EnemyHealthComponent healthComponent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyState.Alive;
        healthComponent = GetComponent<EnemyHealthComponent>();
    }

    // Update is called once per frame
   public  void EnemyUpdate()
    {
        if (enemyState == EnemyState.Alive)
        {
            navMeshAgent.destination = target.position;
            if (healthComponent.isDead)
            {
                enemyState = EnemyState.Dead;
            }
        }
        else if (enemyState == EnemyState.Dead)
        {
            Debug.Log("Entity is Dead");
        }
    }
}
