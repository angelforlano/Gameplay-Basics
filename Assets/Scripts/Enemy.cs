using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int hp = 25;
    [Range(2, 20)] public int warningRange = 8;
    [Range(2, 20)] public int attackRange = 4;
    public List<Transform> wayPoints = new List<Transform>();

    int currentWayPointIndex;
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
            // Debug.Log(Vector3.Distance(transform.position, wayPoints[currentWayPointIndex].position));

            if (Vector3.Distance(transform.position, wayPoints[currentWayPointIndex].position) < 1)
            {
                GoToLinealPoint();
            }

            if(PlayerOnRange(warningRange))
            {
                Debug.Log("Player On Warning Range");
            }

            if(PlayerOnRange(attackRange))
            {
                Debug.Log("Player On Attack Range");
            }
            
            
            yield return new WaitForSeconds(1f);
        }
    }
}
