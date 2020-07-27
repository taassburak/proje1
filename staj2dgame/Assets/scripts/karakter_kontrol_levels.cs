using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class karakter_kontrol_levels : MonoBehaviour
{



    public GameObject range;
    public Image SiyahArkaPlan;

    public Sprite[] beklemeAnim;
    public Sprite[] ziplamaAnim;
    public Sprite[] yurumeAnim;
    public Sprite[] olumAnim;
    public Sprite[] attackAnim;
    public Sprite[] blokAnim;



    int blokAnimSayac = 0;
    int olumAnimSayac = 0;
    int beklemeAnimSayac = 0;
    int yurumeAnimSayac = 0;
    int attackAnimSayac = 0;

    float siyaharkaPlanSayac = 0;
    float anaMenuyeDonZaman = 0;

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

    public int can = 20;

    float coin = 0;
    public Text cointext;
    public Text cantext;

    float hiz = 2;
    public float tirmanma = 0;

    float coinYokOlmaTime = 0;
    float canYokOlmaTime = 0;

    bool bloklama = false;

    float hasarYemeTime = 0;
    bool hasarYedim = false;

    GameObject sonrakilevelbuton;
    GameObject t1obj, tutcollider, t2obj, tutcollider2, t3obj, tutcollider3, t4obj, tutcollider4, t1but, t2but, t3but, t4but;




    void Start()
    {


        objdef();


        Time.timeScale = 1;
        sonrakilevelbuton = GameObject.Find("sonrakilevelbuton");

        if (SceneManager.GetActiveScene().buildIndex > PlayerPrefs.GetInt("kacincilevel"))
        {
            PlayerPrefs.SetInt("kacincilevel", SceneManager.GetActiveScene().buildIndex);
        }

        for (int i = 0; i < sonrakilevelbuton.transform.childCount; i++)
        {
            sonrakilevelbuton.transform.GetChild(i).gameObject.SetActive(false);
        }

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
                physics.AddForce(new Vector2(0, 150));
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

        if (can <= 0)
        {
            Time.timeScale = 0.4f;
            cantext.enabled = false;
            siyaharkaPlanSayac += 0.03f;
            SiyahArkaPlan.color = new Color(0, 0, 0, siyaharkaPlanSayac);
            anaMenuyeDonZaman += Time.deltaTime;


            if (anaMenuyeDonZaman > 1)
            {
                SceneManager.LoadScene("anamenu");
            }
        }



    }

    void objdef()
    {
        t1obj = GameObject.FindGameObjectWithTag("t1");
        t1obj.gameObject.SetActive(false);
        t2obj = GameObject.FindGameObjectWithTag("t2");
        t2obj.gameObject.SetActive(false);
        t3obj = GameObject.FindGameObjectWithTag("t3");
        t3obj.gameObject.SetActive(false);
        t4obj = GameObject.FindGameObjectWithTag("t4");
        t4obj.gameObject.SetActive(false);
        tutcollider = GameObject.FindGameObjectWithTag("t1go");
        tutcollider2 = GameObject.FindGameObjectWithTag("t2go");
        tutcollider3 = GameObject.FindGameObjectWithTag("t3go");
        tutcollider4 = GameObject.FindGameObjectWithTag("t4go");

        t1but = GameObject.FindGameObjectWithTag("t1but");
        t1but.gameObject.SetActive(false);
        t2but = GameObject.FindGameObjectWithTag("t2but");
        t2but.gameObject.SetActive(false);
        t3but = GameObject.FindGameObjectWithTag("t3but");
        t3but.gameObject.SetActive(false);
        t4but = GameObject.FindGameObjectWithTag("t4but");
        t4but.gameObject.SetActive(false);
    }

    void charmove()
    {
        // karakteri yürütmek için
        if (can != 0)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vec = new Vector3(horizontal * hiz, physics.velocity.y, 0);
            physics.velocity = vec;

        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        jumpeonce = true;


    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "can")
        {

            if (can >= 70)
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
            else if (can < 70)
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

        if (col.gameObject.tag == "gold")
        {

            coin += 1;
            cointext.text = "ALTIN = " + coin + " / 15";
            col.GetComponent<coins>().enabled = true;
            col.GetComponent<CircleCollider2D>().enabled = false;
            Destroy(col.gameObject);
            
        }

        if (col.gameObject.tag == "patlamaRange")
        {
            can -= 15;
            cantext.text = "CAN = " + can + " / 100";
        }

        if (col.gameObject.tag == "levelbitti")
        {
            Time.timeScale = 0;
            for (int i = 0; i < sonrakilevelbuton.transform.childCount; i++)
            {
                sonrakilevelbuton.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        if (col.gameObject.tag == "t1go")
        {
            t1obj.gameObject.SetActive(true);
            t1but.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        if (col.gameObject.tag == "t2go")
        {
            t2obj.gameObject.SetActive(true);
            t2but.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        if (col.gameObject.tag == "t3go")
        {
            t3obj.gameObject.SetActive(true);
            t3but.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        if (col.gameObject.tag == "t4go")
        {
            t4obj.gameObject.SetActive(true);
            t4but.gameObject.SetActive(true);
            Time.timeScale = 0;
        }



    }

    public void butonsec(int gelenbuton)
    {
        if (gelenbuton == 0)
        {
            Destroy(tutcollider);
            t1obj.gameObject.SetActive(false);
            t1but.gameObject.SetActive(false);
            Time.timeScale = 1;
            Debug.Log("b1");

        }
        if (gelenbuton == 1)
        {
            Destroy(tutcollider2);
            t2obj.gameObject.SetActive(false);
            t2but.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        if (gelenbuton == 2)
        {
            Destroy(tutcollider3);
            t3obj.gameObject.SetActive(false);
            t3but.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        if (gelenbuton == 3)
        {
            Destroy(tutcollider4);
            t4obj.gameObject.SetActive(false);
            t4but.gameObject.SetActive(false);
            Time.timeScale = 1;
        }


    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "axe")
        {
            if (bloklama == false)
            {
                hasarYemeTime += Time.deltaTime;
                if (hasarYemeTime > 1.2f)
                {
                    can -= 10;
                    cantext.text = "CAN = " + can + " / 100";
                    hasarYemeTime = 0;
                }
            }

        }

        if (col.gameObject.tag == "zombi")
        {
            if (bloklama == false)
            {
                hasarYemeTime += Time.deltaTime;
                if (hasarYemeTime > 1f)
                {
                    can -= 15;
                    cantext.text = "CAN = " + can + " / 100";
                    hasarYemeTime = 0;
                }
            }

        }

        if (col.gameObject.tag == "strikerSword")
        {
            if (bloklama == false)
            {
                hasarYemeTime += Time.deltaTime;
                if (hasarYemeTime > 1.2f)
                {
                    can -= 15;
                    cantext.text = "CAN = " + can + " / 100";
                    hasarYemeTime = 0;
                }
            }

        }
        if (col.gameObject.tag == "flyingeye")
        {
            if (bloklama == false)
            {
                hasarYemeTime += Time.deltaTime;
                if (hasarYemeTime > 1f)
                {
                    can -= 5;
                    cantext.text = "CAN = " + can + " / 100";
                    hasarYemeTime = 0;
                }
            }

        }
        if (col.gameObject.tag == "goblin")
        {
            if (bloklama == false)
            {
                hasarYemeTime += Time.deltaTime;
                if (hasarYemeTime > 0.8f)
                {
                    can -= 8;
                    cantext.text = "CAN = " + can + " / 100";
                    hasarYemeTime = 0;
                }
            }

        }
        if (col.gameObject.tag == "ents")
        {
            if (bloklama == false)
            {
                hasarYemeTime += Time.deltaTime;
                if (hasarYemeTime > 1.2f)
                {
                    can -= 8;
                    cantext.text = "CAN = " + can + " / 100";
                    hasarYemeTime = 0;
                }
            }

        }
        if (col.gameObject.tag == "mushroom")
        {
            if (bloklama == false)
            {
                hasarYemeTime += Time.deltaTime;
                if (hasarYemeTime > 1.2f)
                {
                    can -= 5;
                    cantext.text = "CAN = " + can + " / 100";
                    hasarYemeTime = 0;
                }
            }

        }
        if (col.gameObject.tag == "skeleton")
        {
            if (bloklama == false)
            {
                hasarYemeTime += Time.deltaTime;
                if (hasarYemeTime > 1.2f)
                {
                    can -= 10;
                    cantext.text = "CAN = " + can + " / 100";
                    hasarYemeTime = 0;
                }
            }

        }
        if (col.gameObject.tag == "suicy")
        {
            if (bloklama == false)
            {
                hasarYemeTime += Time.deltaTime;
                if (hasarYemeTime > 1.2f)
                {
                    can -= 30;
                    cantext.text = "CAN = " + can + " / 100";
                    hasarYemeTime = 0;
                }
            }

        }

        if (col.gameObject.tag == "ladder" && can != 0)
        {
            if (Input.GetKey("w"))
            {
                physics.velocity = new Vector2(0, tirmanma);
            }
            else if (Input.GetKey("s"))
            {
                physics.velocity = new Vector2(0, -tirmanma);
            }
        }

    }

  

    public void levelarasi(int gelenbutonsecim)
    {
        if (gelenbutonsecim == 0)
        {
            SceneManager.LoadScene(0);
        }
        else if (gelenbutonsecim == 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (gelenbutonsecim == 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

        else if (jumpeonce == false && can != 0)
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

        if (can <= 0)
        {
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            physics.velocity = new Vector2(0, 0);
            can = 0;
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
        if (can != 0)
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
        if (can != 0)
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
                hiz = 1.4f;

            }
            else
                hiz = 2f;
        }


    }

    void kameraKontrol()
    {
        kameraSonPos = kameraIlkPos + transform.position;
        //kamera.transform.position = Vector3.Lerp(kamera.transform.position, kameraSonPos, 0.5f);
        kamera.transform.position = kameraSonPos;
    }
}
