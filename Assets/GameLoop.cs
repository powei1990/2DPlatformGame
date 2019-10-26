using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    // 場景狀態
     //SceneStateController m_SceneStateController = new SceneStateController();
    
    // 
    void Awake()
    {
        // 切換場景不會被刪除
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start() 
    {
        // 設定起始的場景
        SceneStateController.instance.SetState(new StartState(), "");
        //m_SceneStateController.SetState(new StartState(),"");
    }

    // Update is called once per frame
    void Update()
    {
        SceneStateController.instance.StateUpdate();
    }

    //void SetSetate()
    //{

    //}
}
