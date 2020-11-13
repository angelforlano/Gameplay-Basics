﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int hp = 25;
    [Range(2, 20)] public int warningRange = 8;
    [Range(2, 20)] public int attackRange = 4;
    
    PlayerController playerTarget;
    int currentWayPointIndex;
    List<Transform> wayPoints = new List<Transform>();
    NavMeshAgent agent;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, warningRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();

        StartCoroutine(UpdateLoop());
    }

    void Die()
    {
        GameController.Instance.RemoveEnemy(this);

        gameObject.SetActive(false);
    }

    public void GetDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            Die();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<DamageEntity>() != null)
        {
            var entity = other.GetComponent<DamageEntity>();

            if (entity.damageEnemies)
            {
                GetDamage(entity.damage);

                entity.DestroyEntity();
            }
        }
    }

    void GoToRandomPoint()
    {
        currentWayPointIndex = Random.Range(0, wayPoints.Count);
        agent.SetDestination(wayPoints[currentWayPointIndex].position);
    }

    void GoToLinealPoint()
    {
        currentWayPointIndex++;
        
        if (currentWayPointIndex >= wayPoints.Count)
        {
            currentWayPointIndex = 0;
        }

        agent.SetDestination(wayPoints[currentWayPointIndex].position);
    }

    bool PlayerOnRange(float range)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.CompareTag("Player"))
            {
                playerTarget = colliders[i].gameObject.GetComponent<PlayerController>();
                return true;
            }
        }

        return false;
    }

    IEnumerator UpdateLoop()
    {
        GoToRandomPoint();

        while (true)
        {
            if (playerTarget == null)
            {
                if (Vector3.Distance(transform.position, wayPoints[currentWayPointIndex].position) < 1)
                {
                   GoToLinealPoint(); 
                }
            }

            if(PlayerOnRange(warningRange))
            {
                Debug.Log("Player On Warning Range");
                agent.SetDestination(playerTarget.transform.position);
            }

            if(PlayerOnRange(attackRange))
            {
                Debug.Log("Player On Attack Range");
            }
 
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void SetWayPoints(List<Transform> _wayPoints)
    {
        wayPoints = _wayPoints;
    }
}