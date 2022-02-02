using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkManager : MonoBehaviour
{
    [SerializeField]
    private int playerIndex = 0;
    private Animator animator;

    public int CountAtkClick;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        CountAtkClick = 0;
    }


    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    public void BtnAttack()
    {
        CountAtkClick++;
        if(CountAtkClick == 1)
        {
            animator.SetInteger("AtkPhase", 1);
        }
    }

    public void checkAtkPhase()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack1E"))
        {
            if(CountAtkClick > 1)
            {
                animator.SetInteger("AtkPhase", 2);
            }
            else
            {
                resetAtk();
            }
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack2E"))
        {
            if(CountAtkClick > 2)
            {
                animator.SetInteger("AtkPhase", 3);
            }
            else
            {
                resetAtk();
            }
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack3E"))
        {
            resetAtk();
        }
    }

    private void resetAtk()
    {
        CountAtkClick = 0;
        animator.SetInteger("AtkPhase", 0);
    }
}
