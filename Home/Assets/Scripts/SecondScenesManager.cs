using System.Collections;
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

    public Door[] doors;
    
    public AudioSource knockingDoorSource;
    
    public bool startScenesOne;

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
        Destroy(oldPieter);
        Destroy(livingRoomOld);
        knockingDoor();
        muturePieter.active = true;
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
    
}
