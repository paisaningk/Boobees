using System.Collections;
using System.Collections.Generic;
using Script.Controller;
using UnityEngine;

public class AnimationGun : MonoBehaviour
{
    public Animator animator;
    private static readonly int Pew = Animator.StringToHash("PEW");

    // Start is called before the first frame update
    void Start()
    {
        PlayerController.playerInput.PlayerAction.Attack.performed += context => SetAttack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetAttack()
    {
        animator.SetBool(Pew,true);
        StartCoroutine(WaitForSeconds());
    }
    
    IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool(Pew,false);
        StopCoroutine(WaitForSeconds());
    }
}
