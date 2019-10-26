using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateController
{
    public static SceneStateController instance;

    private ISceneState m_State;
    private bool m_bRunBegin =false;

    public void SetState(ISceneState state,string loadSceneName)
    {
        LoadScene(loadSceneName);
        m_State = state;
    }

    private void LoadScene(string loadSceneName)
    {
        if (loadSceneName == null || loadSceneName.Length == 0)
            return;

        SceneManager.LoadScene(loadSceneName);

    }

    public void StateUpdate()
    {
        // 是否還在載入
        //if (Application.isLoadingLevel)
        //    return;

        // 通知新的State開始
        if (m_State != null && m_bRunBegin == false)
        {
            m_State.StateBegin();
            m_bRunBegin = true;
        }

        if (m_State != null)
            m_State.StateUpdate();
    }

}
