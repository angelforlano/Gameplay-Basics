using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEntity : MonoBehaviour
{
    [Range(1, 100)] public int damage = 10;
    public bool damagePlayer = true;
    public bool damageEnemies = true;

    public void DestroyEntity()
    {
        gameObject.SetActive(false);
    }    
}