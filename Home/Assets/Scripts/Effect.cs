﻿using UnityEngine;
using UnityEngine.UI;

public class Effect : MonoBehaviour
{
    public AnimationCurve darkCurve;
    private float maxTime
    {
        get
        {
            return darkCurve.keys[darkCurve.keys.Length - 1].time;
        }
    }
    private float nowTime;
    private bool display;
    private Image image;
    private bool flag;

    public bool DarkChange
    {
        set
        {
            if (value) startDarkChange();
            else endDarkChange();
        }
    }

     void Start()
    {
        image = GetComponentInChildren<Image>();
        image.transform.localScale = new Vector3(1920f / Screen.width, 1080f / Screen.height, 1);
        image.color = new Color(0, 0, 0, 0.5f);
    }
    
     void Update()
    {
        if (display||!display&&nowTime+Time.deltaTime<maxTime&&flag)
        {
            nowTime += Time.deltaTime;
            nowTime %= maxTime;
            if (nowTime <= 0&&display)
                nowTime = 0;
            darkChange();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            startDarkChange();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            endDarkChange();
        }
    }

    public void startDarkChange()
    {
        display = true;
        flag = true;
    }

    public void endDarkChange()
    {
        display = false;
    }

    void darkChange()
    {
        image.color = new Color(0,0,0,darkCurve.Evaluate(nowTime));
    }
}