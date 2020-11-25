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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            controller.SetTrigger("Jump");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            controller.SetTrigger("Punch");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            controller.SetTrigger("Combo1");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            controller.SetTrigger("Combo2");
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
        
        controller.SetFloat("VMove", vMove);
        
        if(vMove > 0)
        {
            currentSpeed = walkSpeed;
        } else if (vMove < 0)
        {
            currentSpeed = 1;
        } else {
            currentSpeed = 0;
        }

        transform.Rotate(0, hMove * 120 * Time.deltaTime, 0, Space.World);
        transform.Translate(new Vector3(0, 0, vMove) * Time.deltaTime * currentSpeed);
    }

    public void GetSlow()
    {
        currentSpeed = walkSpeed / 2;
        Debug.Log("Player Getting Slow");
    }

    public void RecoverSpeed()
    {
        currentSpeed = walkSpeed;
        Debug.Log("Player Recover Speed");
    }

    public void AddCoin()
    {
        coins++;
        Debug.Log("Coins > " + coins);
    }

    public bool GetDamage(int damage)
    {
        hp -= damage;

        if(hp > 0)
        {
            return false;
        } else {
            Die();
            return true; 
        }
    }

    void Die()
    {
        controller.SetTrigger("Die");
        HUDController.Instance.SetDiePanel();
    }
}