using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public GameObject m_buble;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ShowBuble("robert"));
        }
    }

    IEnumerator ShowBuble(string bubleName)
    {
        m_buble.SetActive(true);
        GameObject go = Manager.instance.GetBubleContent(bubleName);
        go.transform.parent = m_buble.transform;
        go.transform.localPosition = Vector3.zero;
        yield return new WaitForSeconds(1f);
        Destroy(go);
        m_buble.SetActive(false);
    }
}
