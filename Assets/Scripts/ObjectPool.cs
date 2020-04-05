using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField]
    bool shouldParentToThisObject = false;

    [SerializeField]
    int initialSize;

    [SerializeField]
    T objectPrefab;

    List<T> objects = new List<T>();

    protected virtual void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        for (int i = 0; i < initialSize; i++)
        {
            var temp = InstantiateObject();
            temp.gameObject.SetActive(false);
        }
    }

    T InstantiateObject()
    {
        T instantiatedObject;
        if (shouldParentToThisObject)
        {
            instantiatedObject = Instantiate(objectPrefab, transform);
        }
        else
        {
            instantiatedObject = Instantiate(objectPrefab);
        }

        objects.Add(instantiatedObject);

        return instantiatedObject;
    }

    public T GetObject()
    {
        foreach (var obj in objects)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                return obj;
            }
        }

        var newObj = InstantiateObject();
        newObj.gameObject.SetActive(false);
        return newObj;
    }
}
