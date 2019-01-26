using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Playables;

/// <summary>
/// 管理关卡类
/// </summary>
public class Manager : MonoBehaviour
{
    public GameObject m_BackScene;
    public GameObject m_FrontScene;
    public GameObject m_Effect;
    public static Manager instance;
    public PlayableDirector director;
    public GameObject player;
    public GameObject wife;
    public GameObject m_Camera;
    [System.Serializable]
    public struct BubleItem
    {
        public string bubleName;
        public GameObject bubleObject;
    }
    public List<BubleItem> m_bubles;
    [System.Serializable]
    public struct PlotItem
    {
        public string plotName;
        public PlayableAsset asset;
    }
    public List<PlotItem> m_plots;
    public PlayableAsset GetPlot(string plotName)
    {
        return (from i in m_plots where i.plotName == plotName select i.asset).First();
    }
    public List<GameObject> m_YoungRooms;
    public List<GameObject> m_MatureRooms;
    public List<GameObject> m_OldRooms;
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
        player.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        director.stopped += OnDirectorStopped;
        director.paused += OnDirectorStopped;
    }

    void OnDirectorStopped(PlayableDirector director)
    {
        if (director.playableAsset == GetPlot("Awake"))
        {
            wife.SetActive(true);
            player.SetActive(false);
            m_Camera.GetComponent<SimpleCameraFollow>().player = wife;
            m_Camera.GetComponent<SimpleCameraFollow>().isControllable = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeScene(ManagerScene.Front);
        EnterSessionOne();
        //EnterSessionTwo();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopCoroutine("AwakePlot");
            director.Stop();
        }
    }
    public enum ManagerScene
    {
        Front,Back
    }
    public enum Period
    {
        Young,Mature,Old
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
    public void ChangePeriod(Period period)
    {
        foreach (var g in m_YoungRooms) g.SetActive(period == Period.Young);
        foreach (var g in m_MatureRooms) g.SetActive(period == Period.Mature);
        foreach (var g in m_OldRooms) g.SetActive(period == Period.Old);
    }


    void EnterSessionOne()
    {
        StartCoroutine(AwakePlot());
    }
    
    IEnumerator AwakePlot()
    {
        m_Effect.GetComponent<Effect>().startDarkChange();
        yield return new WaitForSeconds(4f);
        m_Effect.GetComponent<Effect>().endDarkChange();
        director.Play(GetPlot("Awake"));
    }

    void EnterSessionTwo()
    {
        player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        m_Camera.GetComponent<SimpleCameraFollow>().isControllable = true;
        ChangePeriod(Period.Old);
    }
}
