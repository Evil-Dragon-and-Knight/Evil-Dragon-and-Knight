using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameObjList
{
    [SerializeField] private bool active;
    [SerializeField] private GameObject gameObj;
    private enum State { Active, Inactive }
    [SerializeField] private State state;

    public void Init(int index)
    {
        if (!active) return;
        if (gameObj == null)
        {
#if UNITY_EDITOR
            Debug.LogWarning($"[SetActiveManager] (<color=#ee1b24>WARNING</color>)\n" +
                             $"GameObject is null. (index: <color=yellow>{index}</color>)");
#endif
            return;
        }
#if UNITY_EDITOR
        Debug.Log($"[SetActiveManager] (<color={(gameObj.activeSelf != (state == State.Active) ? "#3ab549" : "yellow")}>DONE</color>)\n" +
                  $"GameObject: \"<color=cyan>{FileManager.GetFullPath(gameObj.transform)}</color>\"\n" +
                  $"Index: <color=yellow>{index}</color>\n" +
                  $"Active: {(gameObj.activeSelf ? "<color=#3ab549>True</color>" : "<color=#ee1b24>False</color>")} => {(state == State.Active ? "<color=#3ab549>True</color>" : "<color=#ee1b24>False</color>")}");
#endif
        gameObj.SetActive(state == State.Active ? true : false);
    }
}

public class SetActiveManager: MonoBehaviour
{
    [SerializeField] private bool active = true;
    [SerializeField] private List<GameObjList> gameObjList;

    private void Awake()
    {
        if (!active) return;
#if UNITY_EDITOR
        Debug.Log($"[SetActiveManager] Init from \"<color=cyan>{FileManager.GetFullPath(gameObject.transform)}</color>\"");
#endif
        for (var i = 0; i < gameObjList.Count; i++)
        {
            gameObjList[i].Init(i);
        }
    }
}
