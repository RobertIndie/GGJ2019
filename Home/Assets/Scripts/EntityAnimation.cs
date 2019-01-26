using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAnimation : MonoBehaviour
{
    public Animator animator;

    public Entity entity;

    void Update()
    {
        
        animator.SetBool("stop",entity.stop);
        animator.SetBool("LeftWalk",entity.leftWalk);
        animator.SetBool("rightWalk",entity.rightWalk);
        
    }
}
