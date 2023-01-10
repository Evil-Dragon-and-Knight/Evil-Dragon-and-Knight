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
            Debug.LogWarning($"[SetActiveManager] (<color=#ee1b24>WARNING</color>)\nGameObject is null. (index: <color=yellow>{index}</color>)");
            return;
        }
        Debug.Log($"[SetActiveManager] (<color={(gameObj.activeSelf != (state == State.Active) ? "#3ab549" : "yellow")}>DONE</color>)\nGameObject: \"<color=cyan>{FileManager.GetFullPath(gameObj.transform)}</color>\"\nIndex: <color=yellow>{index}</color>\nActive: {(gameObj.activeSelf ? "<color=#3ab549>True</color>" : "<color=#ee1b24>False</color>")} => {(state == State.Active ? "<color=#3ab549>True</color>" : "<color=#ee1b24>False</color>")}");
        gameObj?.SetActive(state == State.Active ? true : false);
    }
}

public class SetActiveManager: MonoBehaviour
{
    [SerializeField] private bool active = true;
    [SerializeField] private List<GameObjList> gameObjList;

    private void Awake()
    {
        if (!active) return;
        Debug.Log($"[SetActiveManager] Init from \"<color=cyan>{FileManager.GetFullPath(gameObject.transform)}</color>\"");
        for (int i = 0; i < gameObjList.Count; i++)
        {
            gameObjList[i].Init(i);
        }
    }
}
