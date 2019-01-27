using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Door2 : Door
{

    public Entity son;

    public Player pieter;
    
    
    // Start is called before the first frame update
    public override void Open()
    {
        StartCoroutine(start());
    }

    IEnumerator start()
    {
        pieter.isControllable = false;
        goTo(pieter,-6.65f);
        goTo(son,-4.39f);
        return null;
    }



    void goTo(Entity s, float to)
    {
        s.moveDest = getMove(s.moveDest, to);
    }
    
    Vector2 getMove(Vector2 v, float to)
    {
        return new Vector2(to,v.y);
    }
}
