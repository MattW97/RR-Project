using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour {

    public float startingHealth;
    public float currentHealth;

    private Canvas worldSpaceUICanvas;
    private Image healthBar;

    private Camera mainCam;

    private void Start()
    {
        currentHealth = startingHealth;

        worldSpaceUICanvas = gameObject.transform.Find("WorldSpaceUI").GetComponent<Canvas>();

        // Recursivly searches all children to find the health bar
        Image[] allChildren = GetComponentsInChildren<Image>();
        foreach (Image child in allChildren)
        {
            if (child.gameObject.name == "HealthBar")
            {
                healthBar = child;
            }
        }

        worldSpaceUICanvas.gameObject.SetActive(false);

        mainCam = Camera.main;
    }

    private void Update()
    {

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            if (!GetComponent<PlayerController>().ragdolling)
            {
                GetComponent<PlayerController>().Ragdoll(true);
                worldSpaceUICanvas.gameObject.SetActive(false);
            }
        }

        if(GetComponent<PlayerController>().isDead)
        {
            worldSpaceUICanvas.gameObject.SetActive(false);
        }

        if(currentHealth > startingHealth)
        {
            currentHealth = startingHealth;
        }

        if(gameObject.GetComponent<PlayerController>().canControl && !worldSpaceUICanvas.isActiveAndEnabled)
        {
            worldSpaceUICanvas.gameObject.SetActive(true);
        }

        worldSpaceUICanvas.transform.LookAt(transform.position + mainCam.transform.rotation * Vector3.forward, mainCam.transform.rotation * Vector3.up);
        healthBar.fillAmount = currentHealth / 100;
    }

    public void DamagePlayer(float damageAmount)
    {
        currentHealth = currentHealth - damageAmount;
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth = currentHealth + healAmount;
    }
}
