﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Text ammoText;
    public Animator fadeAnimator;
    public Animator diePanelAnimator;
    public GameObject diePanel;
    public GameObject interactMsg;

    public static HUDController Instance;

    private PlayerController player;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    
    IEnumerator UpdateHUD()
    {
        FadeIn();

        while (true)
        {
            ammoText.text = player.mainWeapon.currentMagazine + "/" + player.mainWeapon.currentAmmo;

            if (player.mainWeapon.currentMagazine <= 3)
            {
                ammoText.color = Color.red;
            } else {
                ammoText.color = Color.white;
            }
            
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void SetPlayer(PlayerController _player)
    {
        player = _player;

        StartCoroutine(UpdateHUD());
    }

    public void FadeIn()
    {
        fadeAnimator.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        fadeAnimator.SetTrigger("FadeOut");
    }

    public void SetDiePanel()
    {
        diePanel.SetActive(true);
        diePanelAnimator.SetTrigger("Open");
    }

    public void PlayAgainBtn()
    {
        LevelManager.Instance.LoadLevel("Level_01");
    }

    public void GoMenuBtn()
    {
        LevelManager.Instance.LoadLevel("Menu");
    }

    public void SetInteractMsg(bool status)
    {
        interactMsg.SetActive(status);
    }
}