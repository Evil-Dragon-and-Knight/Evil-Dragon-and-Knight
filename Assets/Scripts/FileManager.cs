using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileManager : MonoBehaviour
{
    public static string GetFullPath(Transform trn)
    {
        string path = "/" + trn.name;
        while (trn.transform.parent != null)
        {
            trn = trn.parent;
            path = "/" + trn.name + path;
        }
        return path;
    }
}
