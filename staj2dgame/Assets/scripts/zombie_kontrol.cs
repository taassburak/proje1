﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class zombie_kontrol : MonoBehaviour
{

    public AudioSource sword;

    public Sprite[] yurumeAnim;
    float yurumeAnimTime = 0;
    int yurumeAnimSayac = 0;

    public Sprite[] beklemeAnim;
    float beklemeAnimTime = 0;
    int beklemeAnimSayac = 0;

    public Sprite[] hasarAnim;
    float hasarAnimTime = 0;
    int hasarAnimSayac = 0;

    public Sprite[] olumAnim;
    float olumAnimTime = 0;
    int olumAnimSayac = 0;

    SpriteRenderer spriteRenderer;
    Rigidbody2D fizik;

    public int ENcan = 150;

    
    bool hasarAldı = false;
    bool death = false;


    //takip
    public float hiz;
    private Transform target;
    public float distance;


    RaycastHit2D ray;
    public LayerMask layermask;

    int playerCan;
     
    void Start()
    {
        sword.volume = 0.160f;
        sword.playOnAwake = false;

        spriteRenderer = GetComponent<SpriteRenderer>();
        fizik = GetComponent<Rigidbody2D>();

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    void takip()
    {
        if (Vector2.Distance(transform.position, target.position) > distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, hiz * Time.deltaTime);
            
        }
        if (transform.position.x>target.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
            transform.localScale = new Vector3(1, 1, 1);
    }

    void Update()
    {
        
        
        if (death==true)
        {
            Destroy(gameObject);
        }
    
    }

    private void FixedUpdate()
    {

        

        animasyon();

        benigordumu();

        if (ray.collider.tag == "Player")
        {

            if (ENcan != 0)
            {
                takip();
            }
        }

    }

    void animasyon()
    {
        if (ENcan != 0 && hasarAldı == false )
        {
            beklemeAnimTime += Time.deltaTime;
            if (beklemeAnimTime > 0.09f)
            {
                spriteRenderer.sprite = beklemeAnim[beklemeAnimSayac++];
                if (beklemeAnimSayac == beklemeAnim.Length)
                {
                    beklemeAnimSayac = 0;
                }
                beklemeAnimTime = 0;
            }
        }

        if (ENcan != 0 && hasarAldı == false)
        {
            yurumeAnimTime += Time.deltaTime;
            if (yurumeAnimTime > 0.09f)
            {
                spriteRenderer.sprite = yurumeAnim[yurumeAnimSayac++];
                if (yurumeAnimSayac == yurumeAnim.Length)
                {
                    yurumeAnimSayac = 0;
                }
                yurumeAnimTime = 0;
            }
        }


        if (ENcan==0)
        {
            fizik.velocity = new Vector2(0, 0);
            gameObject.GetComponent<PolygonCollider2D>().enabled=false;
            fizik.isKinematic = true;
            olumAnimTime += Time.deltaTime;
            if (olumAnimTime > 0.09f)
            {
                spriteRenderer.sprite = olumAnim[olumAnimSayac++];
                
                olumAnimTime = 0;
            }
            ENcan = 0;
            if (olumAnimSayac==olumAnim.Length)
            {
                death = true;
            }
            
            
            
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "sword")
        {
            sword.Play();
            hasarAldı = true;
            ENcan -= 20;
            hasarAnimTime += Time.deltaTime;
            if (hasarAnimTime > 0.09f)
            {
                spriteRenderer.sprite = hasarAnim[hasarAnimSayac++];
                if (hasarAnimSayac == hasarAnim.Length)
                {
                    hasarAnimSayac = 0;
                }
                hasarAnimTime = 0;
            }



        }
        hasarAldı = false;
    }

    void benigordumu()
    {
        Vector3 rayYonum = target.transform.position - transform.position;
        ray = Physics2D.Raycast(transform.position, rayYonum, 1000, layermask);
        Debug.DrawLine(transform.position, ray.point, Color.green);
    }
    
}
