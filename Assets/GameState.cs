using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : ISceneState
{
    public override void StateBegin()
    {
        SceneManager.LoadScene("Stage1");
    }

    public override void StateEnd()
    {
        
    }

    public override void StateUpdate()
    {
       
    }


}

