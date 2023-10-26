using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    public int AttackRange;
    public float AttackCoolTime;
    protected bool CanAttack;


    public AttackState(int _AttackRange, float _AttackCoolTime)
    {
        AttackRange = _AttackRange;
        AttackCoolTime = _AttackCoolTime;
        CanAttack = true;
    }

    public virtual void Work(IEnemy characterData, Transform target)
    {
        if (CanAttack == true)
        {
            characterData.navMeshAgent.isStopped = true;
            characterData.animator.SetTrigger("Attack");
            CanAttack = false;
            characterData.navMeshAgent.avoidancePriority = 1;
            StartAttacking(characterData).Forget();
            if (!characterData.Attacking)
            {
                AttackingDecision(characterData).Forget();
            }
        }
    }

    async UniTask StartAttacking(IEnemy characterData)
    {
        await UniTask.Delay((int)(AttackCoolTime * 1000));
        CanAttack = true;
    }

    async UniTask AttackingDecision(IEnemy characterData)
    {
        
        await UniTask.Delay(5);
        characterData.Attacking = true;
        await UniTask.Delay((int)(characterData.animator.GetCurrentAnimatorStateInfo(0).length * 1000));
        characterData.Attacking = false;
    }

}
