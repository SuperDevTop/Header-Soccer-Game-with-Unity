using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientBall : MonoBehaviour
{

    public Animator ani;
    public RuntimeAnimatorController anim1;
    public RuntimeAnimatorController anim2;
    // Use this for initialization
    void Start()
    {
        ani = GetComponent<Animator>();
        if (Loadding.leftOrRight == 0)
        {
            ani.runtimeAnimatorController = anim1 as RuntimeAnimatorController;
        }
        else
        {
            ani.runtimeAnimatorController = anim2 as RuntimeAnimatorController;
        }
    }

   
   
}
