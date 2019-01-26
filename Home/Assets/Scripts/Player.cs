using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    Vector2 moveDest;
    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        moveDest = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            moveDest = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        var t = Time.deltaTime * speed;
        if(Mathf.Abs(moveDest.x - transform.position.x) >= t)
        {
            transform.position = new Vector2(transform.position.x + (moveDest.x > transform.position.x ? t : -t), transform.position.y);
        }
    }
}
