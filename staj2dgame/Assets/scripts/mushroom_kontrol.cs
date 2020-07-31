using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mushroom_kontrol : MonoBehaviour
{
    public AudioSource sword;


    public Sprite[] beklemeAnim;
    float beklemeAnimTime = 0;
    int beklemeAnimSayac = 0;

    

    public Sprite[] olumAnim;
    float olumAnimTime = 0;
    int olumAnimSayac = 0;

    public Sprite[] attackAnim;
    float attackTime = 0;
    int attackSayac = 0;

    public Sprite[] yurumeAnim;
    float yurumeTime = 0;
    int yurumeSayac = 0;

    SpriteRenderer spriteRenderer;
    Rigidbody2D fizik;

    public int ENcan = 100;


    bool hasarAldı = false;
    bool death = false;
    bool attackYapti = false;


    //takip
    public float hiz;
    private Transform target;
    public float distance;


    Vector2 vecMesafe;

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


        if (transform.position.x > target.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
            transform.localScale = new Vector3(1, 1, 1);
    }

    void Update()
    {

        vecMesafe = new Vector2(transform.position.x - target.position.x, 0);
        
        if (death == true)
        {
            Invoke("destroyEn", 5.0f);
        }



    }

    void destroyEn()
    {
        Destroy(gameObject);
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
                attack();
            }

        }





    }

    void animasyon()
    {
        if (ENcan != 0 && hasarAldı == false && attackYapti == false && death == false)
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



        if (ENcan <= 0)
        {
            death = true;
            fizik.velocity = new Vector2(0, 0);
            ENcan = 0;
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            fizik.isKinematic = true;
            olumAnimTime += Time.deltaTime;
            if (olumAnimTime > 0.09f)
            {
                spriteRenderer.sprite = olumAnim[olumAnimSayac++];
                if (olumAnimSayac == olumAnim.Length)
                {
                    olumAnimSayac = olumAnim.Length - 1;

                }

                olumAnimTime = 0;
            }
        }



    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "sword")
        {

            sword.Play();

            hasarAldı = true;
            ENcan -= 10;
            


        }
        else
            hasarAldı = false;
    }

    void benigordumu()
    {
        Vector3 rayYonum = target.transform.position - transform.position;
        ray = Physics2D.Raycast(transform.position, rayYonum, 1000, layermask);
        Debug.DrawLine(transform.position, ray.point, Color.green);
    }

    void attack()
    {

        if (vecMesafe.x <= 0.4f && vecMesafe.x >= 0f)
        {
            if (death == false)
            {
                attackTime += Time.deltaTime;
                if (attackTime > 0.09f)
                {
                    spriteRenderer.sprite = attackAnim[attackSayac++];
                    if (attackSayac == attackAnim.Length)
                    {
                        attackSayac = 0;
                    }
                    attackTime = 0;
                }
                attackYapti = true;
            }
        }
        else if (vecMesafe.x >= -0.4f && vecMesafe.x <= 0f)
        {
            if (death == false)
            {


                attackTime += Time.deltaTime;
                if (attackTime > 0.09f)
                {
                    spriteRenderer.sprite = attackAnim[attackSayac++];
                    if (attackSayac == attackAnim.Length)
                    {
                        attackSayac = 0;
                    }
                    attackTime = 0;
                }
                attackYapti = true;
            }
        }

        else if (vecMesafe.x > 0.4f || vecMesafe.x < -0.4f)
        {


            attackYapti = false;

            if (death == false)
            {
                yurumeTime += Time.deltaTime;
                if (yurumeTime > 0.09f)
                {
                    spriteRenderer.sprite = yurumeAnim[yurumeSayac++];
                    if (yurumeSayac == yurumeAnim.Length)
                    {
                        yurumeSayac = 0;
                    }
                    yurumeTime = 0;
                }
            }
        }
    }

}
