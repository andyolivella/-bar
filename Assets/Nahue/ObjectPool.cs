using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    List<GameObject> pool;
    [SerializeField] GameObject prefab;
    [SerializeField] int maxCuantityAtSameTime;
    // Start is called before the first frame update
    void Start()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < maxCuantityAtSameTime; i++)
        {
            pool.Add(Instantiate(prefab));
        }
        for (int i = 0; i < pool.Count; i++)
        {
            pool[i].name += " " + i.ToString();
            pool[i].SetActive(false);
        }
    }

    public void Release(GameObject objectToRelease)
    {
        if (pool.Contains(objectToRelease))
            objectToRelease.SetActive(false);
    }

    public GameObject Get()
    {
        foreach (GameObject gobj in pool)
        {
            if (!gobj.activeInHierarchy)
            {
                gobj.SetActive(true);
                return gobj;
            }
        }
        GameObject newObj = Instantiate(prefab);
        pool.Add(newObj);
        return newObj;
    }


}
