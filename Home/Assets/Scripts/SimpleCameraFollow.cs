using System;
using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{
    public GameObject player;  
    public float speed;  

    public float maxPosx;

    float lastPlayerXPostion;
    private float playerMoveVector;
        
    void Update()
    {
        //predictionPlayerMoveVector();
        //FixCameraPos();
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("M");
            //创建一个射线，该射线从主摄像机中发出，而发出点是鼠标
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //创建一个射线信息集
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Open Door1");
                //如果点击的是sprite目标
                if (hit.transform.tag == "DoorHandle")
                {
                    Debug.Log("Open Door");
                }
            }
        }
    }

    void predictionPlayerMoveVector()
    {
        float nowPlayerXPostion = player.transform.position.x;
        playerMoveVector = nowPlayerXPostion - lastPlayerXPostion;
        lastPlayerXPostion = nowPlayerXPostion;
    }

    void FixCameraPos()
    {
        float playerPosX = player.transform.position.x; 
        float camaraPosX = transform.position.x;
        float i = playerPosX - camaraPosX;
        if (playerMoveVector == 0)
        {
            makeCameraFocusingOnPlayer();
        }else
        {
            makeCameraFasterThenPlayer();
        }
            
    }

    void makeCameraFocusingOnPlayer()
    {
        var t = Time.deltaTime * speed;
        if(Mathf.Abs(player.transform.position.x - transform.position.x) >= t)
        {
            transform.position = new Vector3(transform.position.x + (player.transform.position.x > transform.position.x ? t : -t), transform.position.y,transform.position.z);
        }
    }
        
    void makeCameraFasterThenPlayer()
    {
        var newPostion = new Vector3(transform.position.x+speed*Time.deltaTime,transform.position.y,transform.position.z);
        if (Math.Abs(newPostion.x - player.transform.position.x) > maxPosx)
            return;
        transform.position = newPostion;
    }

}
