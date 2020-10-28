using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Controllers")]
    public bool canMove = true;

    [Header("Player Settings")]
    [Range(0, 100)] public int hp = 100;
    [Range(1, 4)] public float walkSpeed = 3;
    [Range(4, 8)] public float runningSpeed = 6;

    [Header("Player Addos")]
    public Weapon mainWeapon;
    public Animator controller;
    
    [Header("Player Stacks")]
    public int coins;
    public int keys;

    float currentSpeed;

    void Update()
    {
        if (canMove && hp > 0)
        {
           Move(); 
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            mainWeapon.Shot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            mainWeapon.Reload();
        }

        UpdateAnimator();
    }

    void UpdateAnimator()
    {
        controller.SetFloat("Speed", currentSpeed);
    }

    void Move()
    {
        var hMove = Input.GetAxis("Horizontal");
        var vMove = Input.GetAxis("Vertical");

        if(vMove > 0)
        {
            currentSpeed = walkSpeed;
        } else {
            currentSpeed = 0;
        }

        transform.Translate(new Vector3(hMove, 0, vMove) * Time.deltaTime * currentSpeed);
    }

    public void GetSlow()
    {
        speed = walkSpeed / 2;
        Debug.Log("Player Getting Slow");
    }

    public void RecoverSpeed()
    {
        speed = walkSpeed;
        Debug.Log("Player Recover Speed");
    }

    // (simple) > es vacia, no tiene parametros!
    public void AddCoin()
    {
        coins++;
        Debug.Log("Coins > " + coins);
    }

    // Funcion para recivir dano!!!!
    public bool GetDamage(int damage)
    {
        hp -= damage;

        if(hp > 0)
        {
            return false;
        } else {
           return true; 
        }
    }
}