using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoomerangManager : MonoBehaviour
{
    public static BoomerangManager instance { get; private set; }
    
    PlayerController playerController;

    [SerializeField]GameObject boomerangPrefab;

    public int maxAmmo = 1;
    public int currentAmmo;

    private void Awake()
    {
        instance = this;
        
        playerController = GetComponent<PlayerController>();
        if (playerController == null)
            throw new System.Exception("Couldn't Find Player Controller Script!");
    }

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.attackInput)
        {
            ThrowBoomerang();
        }
    }

    private void ThrowBoomerang()
    {
        if(currentAmmo > 0)
        {
            Instantiate(boomerangPrefab, transform.position, Quaternion.identity);
            currentAmmo--;
        }
    }
           
}

