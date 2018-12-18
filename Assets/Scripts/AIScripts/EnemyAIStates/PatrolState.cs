using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PatrolState : BaseEnemyStateMachine.NPCState
{
    public Transform rightPoint;
    public Transform leftPoint;

    private bool isMovingRight = false;
    private Vector3 rightVector3;
    private Vector3 leftVector3;

    public override void EndState()
    {
    }

    public override void StartState()
    {
        rightVector3 = rightPoint.position;
        leftVector3 = leftPoint.position;
    }

    public override void UpdateState()
    {
        if (isMovingRight)
        {
            enemyStateMachine.associatedCharacterStats.movementMechanics.SetHorizontalInput(1);
            if (enemyStateMachine.transform.position.x > rightVector3.x)
            {
                isMovingRight = false;
            }
        }
        else
        {
            enemyStateMachine.associatedCharacterStats.movementMechanics.SetHorizontalInput(-1);
            if (enemyStateMachine.transform.position.x < leftVector3.x)
            {
                isMovingRight = true;
            }
        }
    }
}
