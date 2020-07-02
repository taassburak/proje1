using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie_kontrol : MonoBehaviour
{

    public Sprite[] beklemeAnim;
    float beklemeAnimTime = 0;
    int beklemeAnimSayac = 0;

    

    public Sprite[] olumAnim;
    float olumAnimTime = 0;
    int olumAnimSayac = 0;

    SpriteRenderer spriteRenderer;
    Rigidbody2D fizik;

    public int ENcan = 100;


    GameObject zombiparts;
   
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fizik = GetComponent<Rigidbody2D>();
        zombiparts = GameObject.Find("zombie_parts");
        

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(ENcan);
    }
    private void FixedUpdate()
    {
        animasyon();
       
    }

    void animasyon()
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

        if (ENcan==0)
        {
            /*gameObject.GetComponent<SpriteRenderer>().enabled=false;

            for (int i = 0; i < zombiparts.transform.childCount; i++)
            {
                zombiparts.transform.GetChild(i).gameObject.SetActive(true);
                
                
            }*/
        }

    }

    


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "sword")
        {
            ENcan -= 20;

        }
    }
}
