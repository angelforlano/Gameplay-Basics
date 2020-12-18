using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectionable : MonoBehaviour
{
    public void Collect(PlayerController player)
    {
        player.AddBullets(12);
        Destroy(gameObject);
    }
}