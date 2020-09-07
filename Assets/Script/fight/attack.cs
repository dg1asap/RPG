using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AttackProcess
{
    public abstract class Attack : MonoBehaviour
    {
        public abstract void execute();
    }

    public class NullAttack : Attack
    {
        public override void execute()
        {
            Debug.Log("Something went wrong : You created NullAttack");
        }
    }
}