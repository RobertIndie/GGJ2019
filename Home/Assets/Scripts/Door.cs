using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject m_Tip;
    public bool m_IsEntered;
    public Manager.ManagerScene m_DestScene;
    // Start is called before the first frame update
    void Start()
    {
        m_Tip.GetComponent<MeshRenderer>().sortingOrder = 100;
    }

    // Update is called once per frame
    void Update()
    {
        m_Tip.SetActive(m_IsEntered);
        if (m_IsEntered && Input.GetKeyDown(KeyCode.W))
        {
            Open();
        }
    }

    public void Open()
    {
        if (m_DestScene == Manager.ManagerScene.Out)
        {
            Debug.Log("开外面门");
            return;
        }
        Manager.instance.ChangeScene(m_DestScene);
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
