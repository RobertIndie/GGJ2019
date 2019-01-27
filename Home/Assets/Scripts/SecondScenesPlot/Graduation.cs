using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graduation : MonoBehaviour
{
    public GameObject photo;
    
    public void showPhoto()
    {
        photo.active = true;
    }

    public void hidePhoto()
    {
        photo.active = false;
    }
    
}
