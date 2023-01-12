using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static void Exit()
    {
        EventLogger(MethodBase.GetCurrentMethod()?.Name);
        Application.Quit();
    }

    private static void EventLogger(string methodName)
    {
#if UNITY_EDITOR
        Debug.Log($"[EventManager] Event Call \"<color=yellow>{methodName}</color>\"");
#endif
    }
}
