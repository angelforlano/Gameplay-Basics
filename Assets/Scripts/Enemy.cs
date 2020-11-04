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

    IEnumerator UpdateLoop()
    {
        GoToRandomPoint();

        while (true)
        {
            // Debug.Log(Vector3.Distance(transform.position, wayPoints[currentWayPointIndex].position));

            // Cambiar Point!
            if (Vector3.Distance(transform.position, wayPoints[currentWayPointIndex].position) < 0.1)
            {
                GoToLinealPoint();
            }

            Collider[] colliders = Physics.OverlapSphere(transform.position, warningRange);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.CompareTag("Player"))
                {
                    Debug.Log("Player Found");
                } else {
                    Debug.Log("Player Not Found");
                }
            }
            
            yield return new WaitForSeconds(1f);
        }
    }
}
