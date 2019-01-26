using UnityEngine;
using UnityEngine.UI;

public class Effect : MonoBehaviour
{
    public AnimationCurve darkCurve;
    private float maxTime;
    private float nowTime;
    private bool display;
    private Image image;

     void Start()
    {
        maxTime = darkCurve.keys[darkCurve.keys.Length - 1].time;
        image = GetComponentInChildren<Image>();
    }


     void Update()
    {
        if (display||!display&&nowTime+Time.deltaTime<maxTime)
        {
            nowTime += Time.deltaTime;
            if (nowTime >= maxTime)
                nowTime -= maxTime;
            if (nowTime <= 0&&display)
                nowTime = 0;
            darkChange();
        }
    }

    public void startDarkChange()
    {
        display = true;
    }

    public void endDarkChange()
    {
        display = false;
    }

    void darkChange()
    {
        Debug.Log(nowTime);
        image.color = new Color(0,0,0,darkCurve.Evaluate(nowTime));
    }
}