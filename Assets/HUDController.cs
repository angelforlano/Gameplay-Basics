using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Text ammoText;
    public Animator fadeAnimator;
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
            Debug.Log("Update HUD");
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
}