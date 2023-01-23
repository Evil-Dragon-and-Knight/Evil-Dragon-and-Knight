using System;
using System.Collections;
using System.Collections.Generic;
using MyBox;
using Unity.VisualScripting;
using UnityEngine;

struct ObjStruct
{
    public ObjectPoolingItem _setting;
    public Transform _objTransform;
    public List<GameObject> _list;

    public int _objCanUse;
    public int _initializeCount;

    public ObjStruct Init(ObjectPoolingItem setting)
    {
        _setting = setting;
        _list = new List<GameObject>();
        
        _objCanUse = 0;
        _initializeCount = 0;

        return this;
    }
    
    public GameObject GetObject()
    {
        GameObject temp = null;
        foreach (var obj in _list)
        {
            if (obj.activeSelf) continue;
            _objCanUse--;
            temp = obj;
            temp.SetActive(true);
            break;
        }
        return temp;
    }
    
    public void InactiveObject()
    {
        foreach (var obj in _list)
        {
            if (!obj.activeSelf) continue;
            obj.SetActive(false);
        }
    }

    public void KillObject()
    {
        foreach (var obj in _list)
        {
            if (!obj.activeSelf) continue;
            obj.GetComponent<Enemy>().Die();
        }
    }
    
    public void Update()
    {
        if (_objCanUse == _initializeCount) return;
        int count = 0;
        foreach (var obj in _list)
        {
            if (!obj.activeSelf) continue;
            count++;
        }
        _objCanUse = count;
    }
}

public class ObjectPooling : Singleton<ObjectPooling>
{
    [DisplayInspector] [SerializeField]
    private List<ObjectPoolingItem> _settings;
    
    private Dictionary<string, ObjStruct> _objectList = new Dictionary<string, ObjStruct>();

    private void Awake()
    {
        foreach (ObjectPoolingItem setting in _settings)
        {
            ObjStruct temp = new ObjStruct().Init(setting);
            temp._objTransform = Instantiate(new GameObject($"List/{setting.name}"), transform.position, Quaternion.identity).transform;
            temp._objTransform.parent = transform;
            for (int i = 0; i < setting.initializeCount; i++)
            {
                CreateObject(temp);
            }
            
            // ReSharper disable once Unity.IncorrectMonoBehaviourInstantiation
            _objectList.Add(setting.name, temp);
        }
    }
    
    private GameObject CreateObject(ObjStruct obj)
    {
        obj._objCanUse++;
        obj._initializeCount++;
        GameObject temp = Instantiate(obj._setting.prefab, obj._objTransform.position, Quaternion.identity);
        obj._list.Add(temp);
        temp.transform.parent = obj._objTransform.transform;
        temp.SetActive(false);
        return temp;
    }

    public GameObject GetObject(string name)
    {
        if (_objectList.ContainsKey(name) == false) return null;
        GameObject temp = _objectList[name].GetObject();
        // ReSharper disable once Unity.PerformanceCriticalCodeNullComparison
        if (temp == null)
        {
            if (_objectList[name]._setting.canExtendSelf)
            {
                temp = CreateObject(_objectList[name]);
                temp.SetActive(true);
                return temp;
            }
        }
        return temp;
    }
    
    public void InactiveEveryActiveObject(string tag)
    {
        foreach (var obj in _settings)
        {
            if (!obj.prefab.CompareTag(tag)) continue;
            _objectList[obj.name].InactiveObject();
        }
    }
    
    public void KillEveryActiveObject(string tag)
    {
        foreach (var obj in _settings)
        {
            if (!obj.prefab.CompareTag(tag)) continue;
            _objectList[obj.name].KillObject();
        }
    }

    private void Update()
    {
        foreach(ObjStruct value in _objectList.Values) {
            value.Update();
        }
    }
}
