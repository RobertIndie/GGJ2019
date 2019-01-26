using System;
using UnityEngine;
using UnityEngine.Playables;

public class SimpleCameraFollow : MonoBehaviour
{
    public GameObject player;
    public float speed;

    public float maxPosx;

    float lastPlayerXPostion;
    private float playerMoveVector;

    public bool isControllable = false;

    public bool smooth = false;

    private void Start()
    {
    }

    void Update()
    {
        //predictionPlayerMoveVector();
        if (smooth)
            FixCameraPos();
        else if (isControllable)
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
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
        }
        else
        {
            makeCameraFasterThenPlayer();
        }
    }

    void makeCameraFocusingOnPlayer()
    {
        var t = Time.deltaTime * speed;
        if (Mathf.Abs(player.transform.position.x - transform.position.x) >= t)
        {
            transform.position =
                new Vector3(transform.position.x + (player.transform.position.x > transform.position.x ? t : -t),
                    transform.position.y, transform.position.z);
        }
    }

    void makeCameraFasterThenPlayer()
    {
        var newPostion = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y,
            transform.position.z);
        if (Math.Abs(newPostion.x - player.transform.position.x) > maxPosx)
            return;
        transform.position = newPostion;
    }
}