﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    
    Rigidbody2D rigidbody2D;
    public Vector2 moveDest;
    public float speed = 1f;
    public GameObject m_buble;
        
    public bool stop;
    public bool rightWalk;
    public bool leftWalk;

    public bool canNotUseMask;
    public bool isControllable = true;
    // Start is called before the first frame update
    protected void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        moveDest = transform.position;
        if(!canNotUseMask)
        GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
    }

    // Update is called once per frame
    protected void Update()
    {
        var t = Time.deltaTime * speed;

        if (Mathf.Abs(moveDest.x - transform.position.x) >= t)
        {
            //transform.position = new Vector2(transform.position.x + (moveDest.x > transform.position.x ? t : -t), transform.position.y);
            rigidbody2D.MovePosition(new Vector2(transform.position.x + (moveDest.x > transform.position.x ? t : -t), transform.position.y));
            if (moveDest.x > transform.position.x)
            {
                RightWalk();
            }
            else
                LeftWalk();
        }
        else
        {
            idle();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ShowBuble("robert"));
        }

    }
    
    protected void idle()
    {
        stop = true;
        rightWalk = false;
        leftWalk = false;
    }

    public void RightWalk()
    {
        stop = false;
        rightWalk = true;
        leftWalk = false;
    }

    public void LeftWalk()
    {
        stop = false;
        rightWalk = false;
        leftWalk = true;
    }

    public void Stop()
    {
        moveDest = transform.position;
    }
    GameObject go;
    public void BeginShowBuble(string bubleName)
    {
        m_buble.SetActive(true);
        go = Manager.instance.GetBubleContent(bubleName);
        go.transform.parent = m_buble.transform;
        go.transform.localPosition = Vector3.zero;
    }

    public void EndShowBuble()
    {
        Destroy(go);
        m_buble.SetActive(false);
    }

    public IEnumerator ShowBuble(string bubleName)
    {
        m_buble.SetActive(true);
        GameObject go = Manager.instance.GetBubleContent(bubleName);
        go.transform.parent = m_buble.transform;
        go.transform.localPosition = Vector3.zero;
        yield return new WaitForSeconds(2f);
        Destroy(go);
        m_buble.SetActive(false);
    }
}
