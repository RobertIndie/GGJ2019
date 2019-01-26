using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ShowBuble("robert");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowBuble(string bubleName)
    {
        GameObject go = Manager.instance.GetBuble(bubleName);
        go.transform.parent = transform;
        go.transform.position = Vector3.zero;
    }
}
