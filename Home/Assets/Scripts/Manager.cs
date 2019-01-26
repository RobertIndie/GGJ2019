using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 管理关卡类
/// </summary>
public class Manager : MonoBehaviour
{
    public GameObject m_BackScene;
    public GameObject m_FrontScene;
    public GameObject m_Effect;
    public static Manager instance;
    [System.Serializable]
    public struct BubleItem
    {
        public string bubleName;
        public GameObject bubleObject;
    }
    public List<BubleItem> m_bubles;
    public GameObject m_prefab_BubleParent;
    public GameObject GetBubleContent(string bubleName)
    {
        GameObject bublePrefab = (from b in m_bubles where b.bubleName == bubleName select b.bubleObject).First();
        GameObject buble = Instantiate(bublePrefab);
        //GameObject bubleParent = Instantiate(m_prefab_BubleParent);
        //buble.transform.parent = bubleParent.GetComponentInChildren<SpriteRenderer>().transform.parent;
        //buble.transform.position = Vector3.zero;
        //return bubleParent;
        return buble;
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
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
        var o = GameObject.FindGameObjectsWithTag("Shadow");
        foreach (var g in o)
        {
            g.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
        }
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
    
    IEnumerator AwakePlot()
    {
        m_Effect.GetComponent<Effect>().startDarkChange();
        yield return new WaitForSeconds(4f);
        m_Effect.GetComponent<Effect>().endDarkChange();
    }
}
