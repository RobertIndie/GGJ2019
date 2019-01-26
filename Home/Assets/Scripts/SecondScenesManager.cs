using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SecondScenesManager : MonoBehaviour
{

    public GameObject camera;
    public GameObject photo;
    public GameObject door;
    public GameObject muturePieter;
    public GameObject livingRoomMature;
    public GameObject oldPieter;
    public GameObject livingRoomOld;
    
    public bool startScenesOne;

    private PlayableDirector director;

    void Start()
    {
        director = GetComponent<PlayableDirector>();
    }
    
    void Update()
    {
        if (startScenesOne)
        {
            StartCoroutine(StartScenesOne());
            director.timeUpdateMode = DirectorUpdateMode.GameTime;
            director.Play(director.playableAsset);
            startScenesOne = false;
        }
    }
    
    IEnumerator StartScenesOne()
    {
        camera.GetComponent<SimpleCameraFollow>().smooth = true;
        setPlayer(photo);
        yield return new WaitForSeconds(4);
        photo.gameObject.GetComponent<PhotoShow>().closeUpPhoto();
        yield return new WaitForSeconds(2);
        livingRoomMature.active = true;
        muturePieter.active = true;
        Destroy(oldPieter);
        Destroy(livingRoomOld);
        yield return new WaitForSeconds(2);
        knockingDoor();
        setPlayer(door);
        yield return new WaitForSeconds(2);
        knockingDoor();
        setPlayer(muturePieter);
        yield return new WaitForSeconds(2);
        camera.GetComponent<SimpleCameraFollow>().smooth = false;
    }

    void setPlayer(GameObject gameObject)
    {
        camera.GetComponent<SimpleCameraFollow>().player = gameObject;
    }

    void knockingDoor()
    {
        
    }
    
}
