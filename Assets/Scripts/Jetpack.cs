using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jetpack : MonoBehaviour
{

    PlayerController playerController;
    public Image fuelGauge;

    [SerializeField]float jetpackForce;
    [SerializeField] float maxJetpackFuel;
    [SerializeField] float coolDownTime;

    float currentJetpackFuel;
    bool isJetpacking;
    float coolDownTimer;
    bool outOfFuel;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        if (playerController == null)
            throw new System.Exception("Couldn't Find Player Controller Script!");
    }
    
    void Start()
    {
        fuelGauge.fillAmount = currentJetpackFuel = maxJetpackFuel;
    }
    void Update()
    {
        if(playerController.FlyInput)
        {
            if(currentJetpackFuel > 0)
            {
                isJetpacking = true;

                currentJetpackFuel -= Time.deltaTime;
                fuelGauge.fillAmount = currentJetpackFuel / maxJetpackFuel;
            }
            else
            {
                isJetpacking = false;

                if (!outOfFuel)
                {
                    outOfFuel = true;
                    coolDownTimer = coolDownTime;
                }

            }

        }
        else
        {
            isJetpacking = false;

            if (!outOfFuel)
                Refuel();
        }

        if (outOfFuel)
        {
            coolDownTimer -= Time.deltaTime;
            
            if (coolDownTimer <= 0)
            {                    
                coolDownTimer = coolDownTime;
                
                outOfFuel = false;
            }

        }
        
        

    }

    void Refuel()
    {
        if (currentJetpackFuel < maxJetpackFuel)
        {
            currentJetpackFuel += Time.deltaTime;
            fuelGauge.fillAmount = currentJetpackFuel / maxJetpackFuel;
        }
            
    }

    private void FixedUpdate()
    {
        Hover();
    }

    private void Hover()
    {
        if(isJetpacking)
        playerController.rigidBody2D.velocity = new Vector2(playerController.rigidBody2D.velocity.x, jetpackForce);
    }
}

