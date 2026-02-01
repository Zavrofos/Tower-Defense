using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageSystem
{
    public bool IsStartDestroyAnimation { get; set; }
    void ApplayDamage(int damage);
}
