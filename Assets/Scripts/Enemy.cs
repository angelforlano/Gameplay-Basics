using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int hp = 25;
    public List<Transform> wayPoints = new List<Transform>();

    int currentWayPointIndex;
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
            Debug.Log(Vector3.Distance(transform.position, wayPoints[currentWayPointIndex].position));

            if (Vector3.Distance(transform.position, wayPoints[currentWayPointIndex].position) < 0.1)
            {
                GoToLinealPoint();
            }

            yield return new WaitForSeconds(1f);
        }
    }
}
