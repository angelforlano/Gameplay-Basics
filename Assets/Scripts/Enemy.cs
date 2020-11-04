using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int hp = 25;
    public List<Transform> wayPoints = new List<Transform>();

    Transform currentWaypoint;
    NavMeshAgent agent;

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

    IEnumerator UpdateLoop()
    {
        currentWaypoint = wayPoints[Random.Range(0, wayPoints.Count)];
        agent.SetDestination(currentWaypoint.position);

        while (true)
        {
            Debug.Log(Vector3.Distance(transform.position, currentWaypoint.position));
            
            if (Vector3.Distance(transform.position, currentWaypoint.position) < 0.1)
            {
                currentWaypoint = wayPoints[Random.Range(0, wayPoints.Count)];
                agent.SetDestination(currentWaypoint.position);
            }

            yield return new WaitForSeconds(1f);
        }
    }
}
