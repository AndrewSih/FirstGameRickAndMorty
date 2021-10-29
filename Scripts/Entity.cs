using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
   public virtual void GetDamage()
    {
        Die();
    }

    protected virtual void Die()
    {
        Destroy(this.gameObject, 0.3f);
    }
}
