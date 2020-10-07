using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyDamage;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            
            var result = player.GetDamage(enemyDamage);

            if (result)
            {
               Debug.Log("Player muerto");

               //... 
            }

            //player.GetSlow();
        }
    }
}
