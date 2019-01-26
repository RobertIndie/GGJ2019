using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public GameObject m_buble;
    // Start is called before the first frame update
    void Start()
    {
        ShowBuble("robert");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowBuble(string bubleName)
    {
        GameObject go = Manager.instance.GetBubleContent(bubleName);
        go.transform.parent = m_buble.transform;
        go.transform.localPosition = Vector3.zero;
    }
}
