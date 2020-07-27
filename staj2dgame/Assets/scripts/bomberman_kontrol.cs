using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bomberman_kontrol : MonoBehaviour
{
    
    public Sprite[] beklemeAnim;
    float beklemeAnimTime = 0;
    int beklemeAnimSayac = 0;

    

    public Sprite[] patlamaAnim;
    float patlamaTime = 0;
    int patlamaSayac = 0;

    public Sprite[] yurumeAnim;
    float yurumeTime = 0;
    int yurumeSayac = 0;

    SpriteRenderer spriteRenderer;
    Rigidbody2D fizik;

    
    bool patladi = false;

    //takip
    public float hiz;
    private Transform target;
    public float distance;

    
    bool gordumu = false;

    RaycastHit2D ray;
    public LayerMask layermask;

    public GameObject patlamaRange;

    void Start()
    {
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
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void Update()
    {

        

        if (gordumu == true)
        {
            Invoke("attack", 4.0f);
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
            gordumu = true;
            if (patladi==false)
            {
                takip();
            }
        }
        else
        {
            gordumu = false;
        }
           

    }

    void animasyon()
    {
        if (gordumu == false && patladi==false)
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

        if (patladi==false && gordumu==true)
        {
            yurumeTime += Time.deltaTime;
            if (yurumeTime>0.09f)
            {
                spriteRenderer.sprite = yurumeAnim[yurumeSayac++];
                if (yurumeSayac==yurumeAnim.Length)
                {
                    yurumeSayac = 0;
                }
                yurumeTime = 0;
            }
        }
    }

    

    void benigordumu()
    {
        Vector3 rayYonum = target.transform.position - transform.position;
        ray = Physics2D.Raycast(transform.position, rayYonum, 1000, layermask);
        Debug.DrawLine(transform.position, ray.point, Color.green);
    }

    void attack()
    {

        patlamaRange.GetComponent<Collider2D>().enabled = true;
        patlamaTime += Time.deltaTime;
        if (patlamaTime > 0.09f)
        {

            spriteRenderer.sprite = patlamaAnim[patlamaSayac++];
            if (patlamaSayac == patlamaAnim.Length)
            {
                patlamaSayac = patlamaAnim.Length - 1;

            }
            patlamaTime = 0;
        }
        patladi = true;
    }
    
    
           
        
      
            



        


    

}
