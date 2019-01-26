using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject m_Tip;
    public bool m_IsEntered;
    // Start is called before the first frame update
    void Start()
    {
        m_Tip.GetComponent<MeshRenderer>().sortingOrder = 100;
    }

    // Update is called once per frame
    void Update()
    {
        m_Tip.SetActive(m_IsEntered);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_IsEntered = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        m_IsEntered = false;
    }

}
