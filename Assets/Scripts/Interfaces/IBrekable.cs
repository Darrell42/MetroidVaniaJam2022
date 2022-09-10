using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBrekable
{
    public void Breack(float damage);
    public void Breack(float damage, Vector3 direction);
}
