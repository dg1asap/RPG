using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AttackProcess
{
    public class AttackFactory
    {
        private static Attack attack;

        //TODO usunąć sprzężenie czasowe canProduce i make
        public static bool canProduce()
        {
            set();
            return !(attack is NullAttack a);
        }

        private static void set()
        {
            if(Input.GetButtonDown("attack"))
                attack = new MeleeAttack();
            else
                attack = new NullAttack();
        }

        public static Attack make()
        {
            return attack;
        }
    }
}