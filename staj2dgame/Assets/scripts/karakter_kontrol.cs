using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class karakter_kontrol : MonoBehaviour
{
   


    public GameObject range;

    public Sprite[] beklemeAnim;
    public Sprite[] ziplamaAnim;
    public Sprite[] yurumeAnim;
    public Sprite[] olumAnim;
    public Sprite[] attackAnim;
    public Sprite[] blokAnim;



    int blokAnimSayac = 0;
    int olumAnimSayac = 0;
    int beklemeAnimSayac = 0;
    int ziplamaAnimSayac = 0;
    int yurumeAnimSayac = 0;
    int attackAnimSayac = 0;

    SpriteRenderer spriteRenderer;
    float horizontal = 0;
    float waitingTime = 0;
    float runningTime = 0;
    float olumTime = 0;
    float attackTime = 0;
    float blokTime = 0;
    Rigidbody2D physics;
    Vector3 vec;
    bool jumpeonce = true;
    GameObject kamera;

    Vector3 kameraSonPos;
    Vector3 kameraIlkPos;

    int can = 100;

    float coin = 0;
    public Text cointext;
    public Text cantext;

    float coinYokOlmaTime=0;
    float canYokOlmaTime = 0;

    bool bloklama = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        physics = GetComponent<Rigidbody2D>();

        //kameranın oyuncuyu takip etmesi için bir gameobject oluşturup main kamerayı oluşturduğumuz kamera objesine verdik
        kamera = GameObject.FindGameObjectWithTag("MainCamera");
        kameraIlkPos = kamera.transform.position - transform.position;
        cointext.text = "ALTIN = " + coin + " / 15";
        cantext.text = "CAN = " + can + " / 100";
    }

    
    void Update()
    {
        
        
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpeonce && can != 0)
            {
                physics.AddForce(new Vector2(0, 100));
                jumpeonce = false;
            }
        }
    }
    void FixedUpdate()
    {
        //Debug.Log(can);
        Animation();
        charmove();
        Attack();
        blok();
        kameraKontrol();

    }
    void charmove()
    {
        // karakteri yürütmek için
        if (can != 0)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vec = new Vector3(horizontal * 2, physics.velocity.y, 0);
            physics.velocity = vec;
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
       
        jumpeonce = true;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "zombi")
        {

            if (bloklama==false)
            {
                can -= 5;
                cantext.text = "CAN = " + can + " / 100";
            }

            
        }

        if (col.gameObject.tag == "can")
        {
            
            if (can>=70)
            {
                can = 100;
                cantext.text = "CAN = " + can + " / 100";
                col.GetComponent<CircleCollider2D>().enabled = false;
                col.GetComponent<can>().enabled = true;
                
                canYokOlmaTime += Time.deltaTime;
                if (canYokOlmaTime >= 0.019f)
                {
                    Destroy(col.gameObject);
                }
            }
            else if(can<70)
            {
                can += 10;
                cantext.text = "CAN = " + can + " / 100";
                col.GetComponent<CircleCollider2D>().enabled = false;
                col.GetComponent<can>().enabled = true;
                
                canYokOlmaTime += Time.deltaTime;
                if (canYokOlmaTime >= 0.019f)
                {
                    Destroy(col.gameObject);
                }
            }
            
        }

        if (col.gameObject.tag=="gold")
        {

            coin += 0.5f;
            cointext.text = "ALTIN = " + coin + " / 15";
            col.GetComponent<coins>().enabled = true;
            col.GetComponent<CircleCollider2D>().enabled = false;
            coinYokOlmaTime += Time.deltaTime;
            if (coinYokOlmaTime>=0.019f)
            {
                 Destroy(col.gameObject);
            }
        }
        
    }

    void Animation()
    {
        if (jumpeonce == true)
        {
            if (horizontal == 0 && can != 0)
            {
                waitingTime += Time.deltaTime;
                if (waitingTime > 0.09f)
                {
                    spriteRenderer.sprite = beklemeAnim[beklemeAnimSayac++];
                    if (beklemeAnimSayac == beklemeAnim.Length)
                    {
                        beklemeAnimSayac = 0;
                    }
                    waitingTime = 0;
                }

            }
            else if (horizontal > 0 && can != 0)
            {
                runningTime += Time.deltaTime;
                if (runningTime > 0.1f)
                {
                    spriteRenderer.sprite = yurumeAnim[yurumeAnimSayac++];
                    if (yurumeAnimSayac == yurumeAnim.Length)
                    {
                        yurumeAnimSayac = 0;
                    }
                    runningTime = 0;
                }
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (horizontal < 0 && can != 0)
            {
                runningTime += Time.deltaTime;
                if (runningTime > 0.1f)
                {
                    spriteRenderer.sprite = yurumeAnim[yurumeAnimSayac++];
                    if (yurumeAnimSayac == yurumeAnim.Length)
                    {
                        yurumeAnimSayac = 0;
                    }
                    runningTime = 0;
                }
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

        else if (jumpeonce == false  )
        {
                spriteRenderer.sprite = ziplamaAnim[0];

            if (horizontal > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (horizontal < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

        if (can == 0)
        {
            olumTime += Time.deltaTime;
            if (olumTime > 0.05f)
            {
                spriteRenderer.sprite = olumAnim[olumAnimSayac++];
                if (olumAnimSayac == olumAnim.Length)
                {
                    olumAnimSayac = 0;
                }
                olumTime = 0;
             
            }
        }
    }
    void Attack()
    {
        if (can!=0)
        {
            if (Input.GetButton("Fire1"))
            {
                range.gameObject.GetComponent<CircleCollider2D>().enabled = true;

                attackTime += Time.deltaTime;
                if (attackTime > 0.001f)
                {
                    spriteRenderer.sprite = attackAnim[attackAnimSayac++];
                    if (attackAnimSayac == attackAnim.Length)
                    {
                        attackAnimSayac = 0;

                    }
                    attackTime = 0;
                }

            }
            else
                range.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
        
    }
    void blok()
    {
        if (can!=0)
        {
            if (Input.GetButton("Fire2"))
            {
                blokTime += Time.deltaTime;
                if (blokTime > 0.001f)
                {
                    spriteRenderer.sprite = blokAnim[blokAnimSayac++];
                    if (blokAnimSayac == blokAnim.Length)
                    {
                        blokAnimSayac = blokAnim.Length - 1;
                    }
                    blokTime = 0;
                }
                bloklama = true;

            }
        }
        

    }

    void kameraKontrol()
    {
        kameraSonPos = kameraIlkPos + transform.position;
        //kamera.transform.position = Vector3.Lerp(kamera.transform.position, kameraSonPos, 0.5f);
        kamera.transform.position = kameraSonPos;
    }
}
