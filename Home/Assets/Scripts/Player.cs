using UnityEngine;

public class Player : Entity
{

    void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(0))
        {
            moveDest = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

}
