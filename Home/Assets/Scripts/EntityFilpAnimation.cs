using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFilpAnimation : MonoBehaviour
{
    public Animator animator;

    public Entity entity;

    void Update()
    {
        animator.SetBool("stop",entity.stop);
        animator.SetBool("leftwalk",entity.leftWalk);
        animator.SetBool("rightwalk",entity.rightWalk);
    }
}
