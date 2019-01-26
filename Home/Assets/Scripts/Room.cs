using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool m_IsEntered = false;
    public GameObject m_Shadow;
    public AnimationCurve m_ShadowChangeCurve;
    float m_CurveTimeLength
    {
        get
        {
            return m_ShadowChangeCurve.keys[m_ShadowChangeCurve.length - 1].time;
        }
    }
    private float m_ShadowChangeStopTime;
    // Start is called before the first frame update
    void Start()
    {
        m_Shadow.SetActive(true);
        GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_ShadowChangeStopTime > Time.time)
        {
            float a;
            if (m_IsEntered)
            {
                a = m_ShadowChangeCurve.Evaluate(m_ShadowChangeStopTime - Time.time);
            }
            else
            {
                a = m_ShadowChangeCurve.Evaluate(m_CurveTimeLength - (m_ShadowChangeStopTime - Time.time));   
            }
            m_Shadow.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, a);
        }
    }
    
    public void EnterRoom()
    {
        m_IsEntered = true;
        m_ShadowChangeStopTime = Time.time + m_CurveTimeLength;
    }

    public void LeaveRoom()
    {
        m_IsEntered = false;
        m_ShadowChangeStopTime = Time.time + m_CurveTimeLength;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        EnterRoom();
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        LeaveRoom();
    }
}
