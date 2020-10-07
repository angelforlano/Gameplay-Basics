using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    public Vector2 area;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(area.x, 1, area.y));
    }

    public Vector3 GetRandomArea()
    {
        return transform.position + new Vector3(Random.Range(-area.x/2 , area.x/2), 1, Random.Range(-area.y/2 , area.y/2));
    }
}
