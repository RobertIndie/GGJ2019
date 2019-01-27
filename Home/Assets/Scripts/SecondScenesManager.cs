using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondScenesManager : MonoBehaviour
{

    public GameObject camera;
    public GameObject photo;
    public GameObject door;
    public GameObject muturePieter;
    public GameObject livingRoomMature;
    public GameObject oldPieter;
    public GameObject livingRoomOld;
    public GameObject graduation;
    
    public GameObject m_Backs;
    public GameObject m_Fronts;

    public static SecondScenesManager instance;

    public void ChangeScene(Manager.ManagerScene ms)
    {
        switch (ms)
        {
            case Manager.ManagerScene.Back:
                m_Backs.SetActive(true);
                m_Fronts.SetActive(false);
                break;
            case Manager.ManagerScene.Front:
                m_Backs.SetActive(false);
                m_Fronts.SetActive((true));
                break;
        }
    }
    
    public Door[] doors;
    
    public AudioSource knockingDoorSource;
    
    public bool startScenesOne;

    void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
    }
    
    void Update()
    {
        if (startScenesOne)
        {
            StartCoroutine(StartScenesOne());
            startScenesOne = false;
        }
    }
    
    
    
    
    
    IEnumerator StartScenesOne()
    {
        muturePieter.active = true;
        foreach (var door in GameObject.FindGameObjectsWithTag("Door"))
        {
            door.GetComponent<Door>().player = muturePieter;
            door.GetComponent<Door>().close = 1.5f;
        }
        Destroy(oldPieter);
        Destroy(livingRoomOld);
        setPlayer(muturePieter);
        knockingDoor();
        livingRoomMature.active = true;
        yield return new WaitForSeconds(1);
        Entity entity = muturePieter.GetComponent<Entity>();
        entity.ShowBuble("开门");
        foreach (var door in doors)
        {
            door.cancel = true;
            door.cancelBoble = "开门";
        }
    }

    void setPlayer(GameObject gameObject)
    {
        camera.GetComponent<SimpleCameraFollow>().player = gameObject;
    }

    void knockingDoor()
    {
        
    }

    public GameObject getGraduation()
    {
        return graduation;
    }
    
}
