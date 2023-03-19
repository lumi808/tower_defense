using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimation : MonoBehaviour
{
    [SerializeField] private Animator _zombieAnimationController;

    public void Attack()
    {
        _zombieAnimationController.SetBool("Attacking", true);
    }

    public void Walking()
    {
        _zombieAnimationController.SetBool("Attacking", false);
    }
}
