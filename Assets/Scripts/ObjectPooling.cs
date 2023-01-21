using System;
using System.Collections;
using System.Collections.Generic;
using MyBox;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectList : MonoBehaviour
{
    private bool _canExtendSelf;
    private int _initializeCount;
    private List<GameObject> _list;

    public ObjectList(Transform obj, ObjectPoolingItem setting)
    {
        _canExtendSelf = setting.canExtendSelf;
        _initializeCount = setting.initializeCount;
        Transform objTransform = Instantiate(new GameObject($"List/{setting.name}"), obj.position, Quaternion.identity).transform;
        objTransform.parent = obj.transform;
        for (int i = 0; i < _initializeCount; i++)
        {
            GameObject temp = Instantiate(setting.prefab, objTransform.position, Quaternion.identity);
            temp.transform.parent = objTransform.transform;
            temp.SetActive(false);
            if (_list != null) _list.Add(temp);
        }
    }
}

public class ObjectPooling : MonoBehaviour
{
    [DisplayInspector] [SerializeField]
    private List<ObjectPoolingItem> _settings;
    
    private Dictionary<string, ObjectList> _objectList = new Dictionary<string, ObjectList>();

    private void Awake()
    {
        foreach (ObjectPoolingItem setting in _settings)
        {
            // ReSharper disable once Unity.IncorrectMonoBehaviourInstantiation
            _objectList.Add(setting.name, new ObjectList(gameObject.transform, setting));
        }
    }

    public GameObject GetObject(string name)
    {
        //if (_settings.Contains(name)) return;
        return new GameObject("");
    }
}
