using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Find("Tip").gameObject.GetComponent<MeshRenderer>().sortingOrder = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
