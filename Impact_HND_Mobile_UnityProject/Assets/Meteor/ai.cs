using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ai : ScriptableObject
{
    public abstract void Think(MeteorController controller);
}
