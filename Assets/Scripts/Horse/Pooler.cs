/**
Script Author : Vaikash 
Description   : Generic Object Pooler
**/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pooler : MonoBehaviour
{

    #region Var

    public static Pooler InstRef;

    //public int poolerObjLimit;
    public PoolerData[] PoolHolder;

    GameObject instance;
    GameObject polledInst;
    PoolerData pooler;
    GameObject pooledObj;

    [System.Serializable]
    public struct PoolerData
    {
        public string name;
        public int ID; // starts from 0
        public int cloneLimit;
        public bool canGrow;
        //public bool isBusy; // to be implemented if need arises
        public GameObject prefab;
        public List<GameObject> pool;
    }

    #endregion Var

    public void Awake()
    {
        InstRef = this;
    }

    void Start()
    {

    }

    public void OnEnable()
    {
        Init();
    }

    public void OnDisable()
    {
        ShutDown();
    }

    void Update()
    {

    }

    // Initialize Pooler
    void Init()
    {
        foreach (var obj in PoolHolder)
        {
            PoolInit(obj);
        }
    }

    // Clean Pooler
    void ShutDown()
    {
        foreach (var obj in PoolHolder)
        {
            PoolKill(obj);
        }
    }

    // Create, Store and Hide
    void PoolInit(PoolerData data)
    {
        for (int i = 0; i < data.cloneLimit; i++)
        {
            instance=Instantiate(data.prefab, Vector3.zero, Quaternion.identity) as GameObject;
            instance.transform.parent = transform;
            data.pool.Add(instance);
            instance.SetActive(false);
        }
    }

    // Destroy and Clean
    void PoolKill(PoolerData data)
    {
        for (int i = 0; i < data.pool.Count; i++)
        {
            Destroy(data.pool[i]);
        }
        data.pool.Clear();
    }

    #region Interface

    // Get a specific type of pooled object
    public GameObject GetPooledObject(int ID)
    {
        polledInst = null;
        pooler=PoolHolder[ID];
        for (int i = 0; i < pooler.pool.Count; i++)
        {
            if(!pooler.pool[i].activeInHierarchy)
            {
                polledInst = pooler.pool[i];
                //polledInst.SetActive(true);
                return polledInst;
            }
        }
        if (pooler.canGrow)
        {
            instance=Instantiate(pooler.prefab, Vector3.zero, Quaternion.identity) as GameObject;
            instance.transform.parent = this.gameObject.transform;
            pooler.pool.Add(instance);
            //instance.SetActive(true);
            return instance;
        }
        return null;
    }

    // Pseudo Destroy Code
    public void Sleep(GameObject Obj)
    {
        Obj.transform.position = Vector3.zero;
        Obj.transform.rotation = Quaternion.identity;
        Obj.SetActive(false);
    }

    // Disable all pooled Objects
    public void HideAll()
    {
        foreach (var obj in PoolHolder)
        {
            for (int i = 0; i < obj.pool.Count; i++)
            {
                pooledObj=obj.pool[i];
                pooledObj.SetActive(false);
            }
        }
    }

    #endregion Interface
}
