using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartState : ISceneState
{
    //private ISceneState sceneState; 

    private Button startButton;

    public override void StateBegin()
    {
        startButton = GameObject.Find("StartButton").GetComponent<Button>();
        startButton.onClick.AddListener(StartGame);
    }

    public override void StateEnd()
    {

    }

    public override void StateUpdate()
    {
    }

    public void StartGame()
    {
        Debug.Log("Start Game!!");
        SceneStateController.instance.SetState(new GameState(),"GameState");
    }
}
