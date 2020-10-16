using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shotPoint;
    [Range(1, 40)] public int magazine = 7;
    [Range(1, 300)] public int ammo = 28;

    public int currentMagazine;
    public int currentAmmo;

    void Awake()
    {
        currentMagazine = magazine;
        currentAmmo = ammo;
    }

    public void Reload()
    {
        if(currentAmmo == 0 && currentMagazine == 0 || currentMagazine == magazine)
            return;

        if (currentMagazine == 0)
        {
            if (currentAmmo >= magazine)
            {
               currentAmmo -= magazine; 
            } else {
                currentAmmo = 0;
            }
        } else {
            currentAmmo -= magazine - currentMagazine;
        }
        
        currentMagazine = magazine;
        Debug.Log("Reload!");
    }

    public void Shot()
    {
        if (currentMagazine > 0)
        {
            currentMagazine--;
            var bullet = Instantiate(bulletPrefab, shotPoint.position, Quaternion.identity); 
            Debug.Log("Shot!");
        }
    }
}
