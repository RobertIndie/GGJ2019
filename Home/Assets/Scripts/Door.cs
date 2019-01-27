using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject m_Tip;
    public bool m_IsClosed;
    public Manager.ManagerScene m_DestScene;

    public bool cancel;

    public Entity wife;
    
    public string cancelBoble;

    public float close = 1.5f;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        m_Tip.GetComponent<MeshRenderer>().sortingOrder = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(gameObject.transform.position.x - player.transform.position.x) < close)
            m_IsClosed = true;
        else m_IsClosed = false;
        m_Tip.SetActive(m_IsClosed);
        if (m_IsClosed && Input.GetKeyDown(KeyCode.W))
        {
            if (!cancel)
                Open();
            else 
                StartCoroutine(wife.ShowBuble(cancelBoble));
        }
    }

    public virtual void Open()
    {
        if (m_DestScene == Manager.ManagerScene.Out)
        {
            Debug.Log("开外面门");
            Manager.instance.RelativesComeIn();
            return;
        }
        
        Manager.instance?.ChangeScene(m_DestScene);
        SecondScenesManager.instance?.ChangeScene(m_DestScene);
    }

    
}