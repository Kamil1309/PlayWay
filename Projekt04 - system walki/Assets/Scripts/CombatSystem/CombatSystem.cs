using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace combat{
    public class CombatSystem : MonoBehaviour
    {
        protected float damage;
        protected float life;

        public virtual void TakeDamage(float inDamage){}
        public virtual void DealDamage(float inDamage){}
    }
}
