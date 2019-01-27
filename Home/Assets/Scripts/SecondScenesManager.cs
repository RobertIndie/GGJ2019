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
    public GameObject otherFront;
    public static SecondScenesManager instance;

    public void ChangeScene(Manager.ManagerScene ms)
    {
        switch (ms)
        {
            case Manager.ManagerScene.Back:
                m_Backs.SetActive(false);
                m_Fronts.SetActive(true);
                break;
            case Manager.ManagerScene.Front:
                m_Backs.SetActive(true);
                m_Fronts.SetActive(false);
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(change());
        }
    }

    IEnumerator change()
    {
        var effectObject = GameObject.FindWithTag("Effect");
        Effect effect = effectObject.GetComponent<Effect>();
        effect.loopDarkChange(1);
        yield return new WaitForSeconds(0.7f);
        Manager.falg = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }


    public IEnumerator StartScenesOne()
    {
        m_Fronts = otherFront;
        foreach (var door in doors)
        {
            door.close = 1.5f;
        }

        var effectObject = GameObject.FindWithTag("Effect");
        oldPieter.GetComponent<Entity>().isControllable = false;
//        yield return oldPieter.GetComponent<Entity>().ShowBuble("儿子");
//        yield return new WaitForSeconds(2.3f);
        Effect effect = effectObject.GetComponent<Effect>();
        effect.loopDarkChange(1);
        yield return new WaitForSeconds(0.7f);
        var graduation = getGraduation().GetComponent<Graduation>();
        graduation.showPhoto();
        yield return new WaitForSeconds(3);
        effect.loopDarkChange(2);
        yield return new WaitForSeconds(0.7f);
        graduation.hidePhoto();
        muturePieter.active = true;
        foreach (var door in doors)
        {
            door.player = muturePieter;
        }

        oldPieter.SetActive(false);
        livingRoomOld.SetActive(false);
        setPlayer(muturePieter);
        knockingDoor();
        livingRoomMature.active = true;
        yield return new WaitForSeconds(1);
        Entity entity = muturePieter.GetComponent<Entity>();
        StartCoroutine(entity.ShowBuble("开门"));
        foreach (var door in doors)
        {
            door.player = muturePieter;
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