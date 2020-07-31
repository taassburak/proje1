using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
# endif




public class asansor_kontrol : MonoBehaviour
{
    public float hiz;
    GameObject[] gidilecekNoktalar;
    bool aradakiMesafeyiBirkereAl = true;
    Vector3 aradakiMesafe;
    int aradakiMesafeSayaci = 0;
    bool ilerigeri = true;


    // Start is called before the first frame update
    void Start()
    {

        gidilecekNoktalar = new GameObject[transform.childCount];

        for (int i = 0; i < gidilecekNoktalar.Length; i++)
        {
            gidilecekNoktalar[i] = transform.GetChild(0).gameObject;
            gidilecekNoktalar[i].transform.SetParent(transform.parent);
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag=="Player")
        {
            noktalaraGit();
        }
    }
    void noktalaraGit()
    {

        if (aradakiMesafeyiBirkereAl)
        {
            aradakiMesafe = (gidilecekNoktalar[aradakiMesafeSayaci].transform.position - transform.position).normalized;
            aradakiMesafeyiBirkereAl = false;
        }
        float mesafe = Vector3.Distance(transform.position, gidilecekNoktalar[aradakiMesafeSayaci].transform.position);
        transform.position += aradakiMesafe * Time.deltaTime * hiz;
        if (mesafe < 0.2f)
        {
            aradakiMesafeyiBirkereAl = true;
            if (aradakiMesafeSayaci == gidilecekNoktalar.Length - 1)
            {
                ilerigeri = false;
            }
            else if (aradakiMesafeSayaci == 0)
            {
                ilerigeri = true;
            }
            if (ilerigeri)
            {
                aradakiMesafeSayaci++;
            }
            else
            {
                aradakiMesafeSayaci--;
            }
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 0.16f);
        }
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }
    }
# endif

}

#if UNITY_EDITOR
[CustomEditor(typeof(asansor_kontrol))]
[System.Serializable]
class boatEditor : Editor
{
    public override void OnInspectorGUI()
    {
        asansor_kontrol MyScript = (asansor_kontrol)target;
        if (GUILayout.Button("ÜRET", GUILayout.MinWidth(100), GUILayout.Width(100)))
        {
            GameObject yeniobjem = new GameObject();
            yeniobjem.transform.parent = MyScript.transform;
            yeniobjem.transform.position = MyScript.transform.position;
            yeniobjem.name = MyScript.transform.childCount.ToString();
        }
        EditorGUILayout.PropertyField(serializedObject.FindProperty("hiz"));
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();

    }
}
#endif
