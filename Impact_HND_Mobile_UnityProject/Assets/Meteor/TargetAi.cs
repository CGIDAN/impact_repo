using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="MeteorTarget")]
public class TargetAi : Ai
{
    public string targetTag;
    public override void Think(MeteorController controller)
    {
        GameObject target = GameObject.FindGameObjectWithTag(targetTag);
        if (target)
        {
            var movement = controller.gameObject.GetComponent<MeteorMovement>();
            if (movement)
            {
                movement.MoveTowardsTarget(target.transform.position);
            }

        }
    }
}
