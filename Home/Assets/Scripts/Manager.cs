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
    public GameObject m_workSon;
    public GameObject m_ThirdTriggers;
    public List<GameObject> m_Relatives;
    public GameObject m_BedRoomCollider;
    //[System.Serializable]
    //public struct BubleItem
    //{
    //    public string bubleName;
    //    public GameObject bubleObject;
    //}
    //public List<BubleItem> m_bubles;
    public List<GameObject> m_bubles;
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
    public GameObject m_LivingRoomOld;
    public GameObject m_BedRoomYountWithoutBed;
    public GameObject m_BedRoomOld;
    public GameObject door;
    public GameObject outDoor;
    public GameObject GetBubleContent(string bubleName)
    {
        //GameObject bublePrefab = (from b in m_bubles where b.bubleName == bubleName select b.bubleObject).First();
        //GameObject buble = Instantiate(bublePrefab);
        //GameObject bubleParent = Instantiate(m_prefab_BubleParent);
        //buble.transform.parent = bubleParent.GetComponentInChildren<SpriteRenderer>().transform.parent;
        //buble.transform.position = Vector3.zero;
        //return bubleParent;
        Debug.Log(m_bubles);
        GameObject bublePrefab = (from b in m_bubles where b.name == bubleName select b).First();
        GameObject buble = Instantiate(bublePrefab);
        return buble;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
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
            ChangePeriod(Period.Young);
            //GameObject.Find("BedRoomYoung").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            //GameObject.Find("BedRoomYoung").SetActive(false);
            GameObject.Find("BedRoomYoungWithoutBed").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_BedRoomCollider.SetActive(false);
        ChangeScene(ManagerScene.Front);
        //EnterSessionOne();
        //EnterSessionTwo();
        EnterSessionThree();
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
        Front,Back,Out//外面的门
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
        GameObject.Find("BedRoomYoungWithoutBed").SetActive(false);
        m_Effect.GetComponent<Effect>().startDarkChange();
        yield return new WaitForSeconds(4f);
        m_Effect.GetComponent<Effect>().endDarkChange();
        //GameObject.Find("BedRoomYoungWithoutBed").SetActive(true);
        m_BedRoomYountWithoutBed.SetActive(true);
        director.Play(GetPlot("Awake"));
    }

    void EnterSessionTwo()
    {
        player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        m_Camera.GetComponent<SimpleCameraFollow>().isControllable = true;
        ChangePeriod(Period.Old);
    }

    void ObjectSetMoveDestTo(GameObject g, float x)
    {
        g.GetComponent<Entity>().moveDest = new Vector2(x, g.transform.position.y);
    }

    public void RelativesComeIn()
    {
        m_BedRoomCollider.SetActive(true);
        StartCoroutine(wife.GetComponent<Entity>().ShowBuble("新年快乐"));
        ObjectSetMoveDestTo(m_Relatives[0], -4.7f);
        m_Relatives[0].GetComponent<Entity>().speed = 4;
        ObjectSetMoveDestTo(m_Relatives[1], -5.74f);
        m_Relatives[1].GetComponent<Entity>().speed = 3.5f;
        ObjectSetMoveDestTo(m_Relatives[2], -7.12f);
        m_Relatives[2].GetComponent<Entity>().speed = 3;
        StartCoroutine(showBuble());
        door.GetComponent<Door>().cancel = true;
        door.GetComponent<Door>().cancelBoble = "起床";
        outDoor.GetComponent<Door>().cancelBoble = "起床";
    }

    IEnumerator showBuble()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(m_Relatives[0].GetComponent<Entity>().ShowBuble("新年快乐"));
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(m_Relatives[1].GetComponent<Entity>().ShowBuble("新年快乐"));
        yield return new WaitForSeconds(0.4f);
        StartCoroutine(m_Relatives[2].GetComponent<Entity>().ShowBuble("新年快乐"));
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(wife.GetComponent<Entity>().ShowBuble("起床"));
    }

    void EnterSessionThree()
    {
        ChangePeriod(Period.Mature);
        player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        player.transform.position = new Vector3(2.5f, 0.73f, 0f);
        player.GetComponent<Entity>().moveDest = player.transform.position;
        m_Camera.GetComponent<SimpleCameraFollow>().isControllable = true;
        m_ThirdTriggers.SetActive(true);
    }
    public GameObject EndSon;
    IEnumerator SonComeInCor()
    {
        var entity = m_workSon.GetComponent<Entity>();
        entity.moveDest = new Vector2(-7, 2.84f);
        yield return new WaitUntil(() => Mathf.Abs(entity.transform.position.x - entity.moveDest.x) < 0.1);
        var p = player.GetComponent<Player>();
        entity.BeginShowBuble("职场晋升");
        yield return new WaitForSeconds(2f);
        entity.EndShowBuble();
        p.BeginShowBuble("赞许");
        yield return new WaitForSeconds(2f);
        p.EndShowBuble();
        entity.BeginShowBuble("出国");
        yield return new WaitForSeconds(2f);
        entity.EndShowBuble();
        p.BeginShowBuble("赞许");
        yield return new WaitForSeconds(2f);
        p.EndShowBuble();
        entity.moveDest = new Vector2(2.56f, 2.84f);
        yield return new WaitForSeconds(1f);
        player.GetComponent<Player>().moveDest.x = player.transform.position.x + 0.5f;
        yield return new WaitUntil(() => Mathf.Abs(entity.transform.position.x - entity.moveDest.x)<0.1);
        entity.gameObject.SetActive(false);
        player.GetComponent<Player>().isControllable = true;
        GameObject.Find("3T1")?.SetActive(false);
        yield return new WaitForSeconds(1f);
        EndSon.transform.position = new Vector3(1.76f, 2.96f, 0);
        EndSon.GetComponent<Entity>().moveDest.x = -15;
    }

    public void SonComeIn()
    {
        player.GetComponent<Player>().isControllable = false;
        StartCoroutine(SonComeInCor());
    }

    public void TheEndPlot()
    {
        
    }

    public static void LoadBackScene()
    {
        instance?.m_BedRoomCollider.SetActive(false);
        instance?.ChangeScene(ManagerScene.Front);
        instance?.EnterSessionThree();
    }
}
