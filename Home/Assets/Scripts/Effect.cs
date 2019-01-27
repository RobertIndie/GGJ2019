using UnityEngine;
using UnityEngine.UI;

public class Effect : MonoBehaviour
{
    public AnimationCurve darkCurve;

    private float maxTime
    {
        get { return darkCurve.keys[darkCurve.keys.Length - 1].time; }
    }

    private float nowTime;
    private bool display;
    public GameObject m_ImageObject;
    private Image image;
    private bool flag;
    private int loopTimes = -233;


    public float startValue = 0.5f;

    void Awake()
    {
        if (m_ImageObject == null)
            return;
        m_ImageObject.SetActive(true);
        image = m_ImageObject.GetComponent<Image>();
        image.transform.localScale = new Vector3(1920f / Screen.width, 2080f / Screen.height, 1);
        image.color = new Color(0, 0, 0, startValue);
    }

    void Update()
    {
        if (display || !display && nowTime + Time.deltaTime < maxTime && flag)
        {
            nowTime += Time.deltaTime;
            bool loopFlag = false;
            if (nowTime >= maxTime)
            {
                nowTime -= maxTime;
                loopTimes -= 1;
                if (loopTimes != -233 && loopTimes <= 0)
                {
                    display = false;
                    nowTime = maxTime;
                    image.color = new Color(0, 0, 0, startValue);
                    loopFlag = true;
                }
            }

            if (nowTime <= 0 && display)
                nowTime = 0;
            if (!loopFlag)
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
        start(true);
    }

    private void start(bool b)
    {
        if (b)
            loopTimes = -233;
        display = true;
        flag = true;
    }

    public void loopDarkChange(int times)
    {
        loopTimes = times;
        start(false);
    }

    public void endDarkChange()
    {
        display = false;
    }

    void darkChange()
    {
        image.color = new Color(0, 0, 0, darkCurve.Evaluate(nowTime));
    }
}