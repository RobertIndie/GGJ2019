using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理关卡类
/// </summary>
public class Manager : MonoBehaviour
{
    public GameObject m_BackScene;
    public GameObject m_FrontScene;
    public static Manager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        ChangeScene(ManagerScene.Front);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public enum ManagerScene
    {
        Front,Back
    }
    public void ChangeScene(ManagerScene ms)
    {
        switch (ms)
        {
            case ManagerScene.Back:
                m_BackScene.SetActive(true);
                m_FrontScene.SetActive(false);
                break;
            case ManagerScene.Front:
                m_FrontScene.SetActive(true);
                m_BackScene.SetActive(false);
                break;
        }
    }
}
