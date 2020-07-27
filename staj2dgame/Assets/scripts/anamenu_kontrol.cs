using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class anamenu_kontrol : MonoBehaviour
{
    GameObject leveller;
    void Start()
    {
        
        leveller = GameObject.Find("LEVELLER");
        for (int i = 0; i < leveller.transform.childCount; i++)
        {
            leveller.transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < PlayerPrefs.GetInt("kacincilevel"); i++)
        {
            leveller.transform.GetChild(i).GetComponent<Button>().interactable = true;

        }
    }
    public void butonSec(int gelenButon)
    {
        if (gelenButon == 1)
        {
            SceneManager.LoadScene(1);
        }
        else if (gelenButon == 2)
        {
            for (int i = 0; i < leveller.transform.childCount; i++)
            {
                leveller.transform.GetChild(i).gameObject.SetActive(true);
            }

        }
        else if (gelenButon == 3)
        {
            Application.Quit();
        }
        
    }

    public void levelsec(int gelenbutonlevel)
    {
        SceneManager.LoadScene(gelenbutonlevel);
    }

    

}
