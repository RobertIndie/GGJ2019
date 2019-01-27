using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Door2 : Door
{
    public Entity son;

    public Player pieter;

    void Start()
    {
        close = -1;
    }
    
    // Start is called before the first frame update
    public override void Open()
    {
        StartCoroutine(start());
    }

    IEnumerator start()
    {
        pieter.isControllable = false;
        goTo(pieter, -6.65f);
        goTo(son, -4.39f);
        bool falg = true;
        while (falg)
        {
            if (inThayWantToBeLocation(pieter) && inThayWantToBeLocation(son))
            {
                falg = false;
                son.LeftWalk();
//                yield return son.ShowBuble("你好");
//                yield return new WaitForSeconds(2);
//                yield return pieter.ShowBuble("你好");
//                yield return new WaitForSeconds(2);
//                yield return son.ShowBuble("行李");
                yield return new WaitForSeconds(2);
                goTo(son, 6.48f);

                var falg2 = true;
                while (falg2)
                {
                    if (inThayWantToBeLocation(son))
                    {
                        son.gameObject.active = false;
                        pieter.isControllable = true;
                        falg2 = false;
                    }
                    yield return new WaitForSeconds(0.5f);
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    bool inThayWantToBeLocation(Entity entity)
    {
        return Mathf.Abs(entity.moveDest.x - entity.gameObject.transform.position.x) < 0.1;
    }

    void goTo(Entity s, float to)
    {
        s.moveDest = getMove(s.moveDest, to);
    }

    Vector2 getMove(Vector2 v, float to)
    {
        return new Vector2(to, v.y);
    }
}