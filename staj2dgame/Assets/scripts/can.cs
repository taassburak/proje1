﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class can : MonoBehaviour
{
    public Sprite[] canDonus;
    int canDonusSayac = 0;
    float canDonusTime = 0;
    SpriteRenderer spriteRenderer;


    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        animasyon();
    }

    void animasyon()
    {
        canDonusTime += Time.deltaTime;
        if (canDonusTime > 0.05f)
        {
            spriteRenderer.sprite = canDonus[canDonusSayac++];
            if (canDonusSayac == canDonus.Length)
            {
                canDonusSayac = 0;
            }
            canDonusTime = 0;
        }
    }


}
