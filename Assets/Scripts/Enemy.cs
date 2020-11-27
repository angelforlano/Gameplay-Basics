using System.Collections;
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
    Animator controller;
    bool died;

    void Start()
    {
        controller = gameObject.GetComponentInChildren<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();

        StartCoroutine(UpdateLoop());
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, warningRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    IEnumerator Die()
    {
        died = true;
        GameController.Instance.RemoveEnemy(this);
        
        controller.SetTrigger("Die");
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }

    public void GetDamage(int damage)
    {
        if (died) return;

        hp -= damage;

        if (hp <= 0)
        {
            StartCoroutine(Die());
        } else {
            controller.SetTrigger("GetHit");
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
                var _player = colliders[i].gameObject.GetComponent<PlayerController>();
                
                if(_player.hp > 0)
                {
                    playerTarget = _player;
                    return true;
                }
            }
        }

        return false;
    }

    IEnumerator UpdateLoop()
    {
        GoToRandomPoint();

        while (hp > 0)
        {
            controller.SetBool("IsWalking", true);

            if (playerTarget == null)
            {
                if (Vector3.Distance(transform.position, wayPoints[currentWayPointIndex].position) < 1)
                {
                    GoToLinealPoint(); 
                } else {
                    agent.SetDestination(wayPoints[currentWayPointIndex].position);
                }
            }

            if(PlayerOnRange(warningRange))
            {
                agent.SetDestination(playerTarget.transform.position);
            } else {
                playerTarget = null;
            }

            if(PlayerOnRange(attackRange))
            {
                var attackID = Random.Range(1, 4);
                controller.SetTrigger("Attack"+attackID);
                
                if (playerTarget.GetDamage(10))
                {
                    playerTarget = null;
                }
                
                yield return new WaitForSeconds(3f);
            }
 
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void SetWayPoints(List<Transform> _wayPoints)
    {
        wayPoints = _wayPoints;
    }
}