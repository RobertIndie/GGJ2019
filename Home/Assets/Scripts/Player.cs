using UnityEngine;

public class Player : Entity
{
    private void Start()
    {
        base.Start();
    }

    void Update()
    {
        base.Update();
        bool isUsed = false;
        if (Input.GetMouseButtonDown(0) && isControllable)
        {
            //Debug.Log("worldPos:"+worldPos);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Raycastall存放碰撞到的所有物体
            RaycastHit[] hits = Physics.RaycastAll(ray);

            //Debug.Log("hits.Length:"+hits.Length);
            for (int i = 0; i < hits.Length; i++)
            {
                isUsed = true;
                Handle(hits[i]);
            }

            if (!isUsed) moveDest = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    public void Handle(RaycastHit hit)
    {
        if (Mathf.Abs(gameObject.transform.position.x - hit.transform.position.x) < 2)
            switch (hit.collider.name)
            {
                case "photo":

//                    var effectObject = GameObject.FindWithTag("Effect");
//                    Effect effect = effectObject.GetComponent<Effect>();
//                    effect.loopDarkChange(1);
                    var scenes = GameObject.FindObjectOfType<SecondScenesManager>();
                    scenes.startScenesOne = true;
                    
                    
                    
//                    var gameObject = GameObject.Find("Manager");
//                    if (gameObject == null)
//                        break;
//                    gameObject.GetComponent<SecondScenesManager>().startScenesOne = true;
                    break;
            }
    }

    public void DisControl()
    {
        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.name)
        {
            case "3T1":
                Manager.instance.SonComeIn();
                base.idle();
                break;
        }
    }
}