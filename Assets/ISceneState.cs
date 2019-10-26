using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ISceneState 
{
    abstract public void StateBegin();
    abstract public void StateUpdate();
    abstract public void StateEnd();
}
