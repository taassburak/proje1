﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coins : MonoBehaviour
{
    public Sprite[] coindonus;
    int coindonusSayac=0;
    float coindonusTime=0;
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
        coindonusTime += Time.deltaTime;
        if (coindonusTime>0.05f)
        {
            spriteRenderer.sprite = coindonus[coindonusSayac++];
            if (coindonusSayac==coindonus.Length)
            {
                coindonusSayac = 0;
            }
            coindonusTime = 0;
        }
    }


}
