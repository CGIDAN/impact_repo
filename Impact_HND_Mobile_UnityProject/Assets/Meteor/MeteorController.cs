using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour
{
    public Ai ai;

    void Update()
    {
        ai.Think(this);
    }
}
