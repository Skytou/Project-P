using UnityEngine;
using System.Collections;

public class SaveDataMgr : MonoBehaviour {
    
    private static SaveDataMgr instance = null;
    public static SaveDataMgr Inst
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<SaveDataMgr>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != instance)
                Destroy(this.gameObject);
        }
    }


    void OnApplicationPause(bool pauseStatus)
    {
        //SavedData.Inst.SaveAllData();
    }

    void OnApplicationQuit()
    {
        SavedData.Inst.SaveAllData();
    }

	void Start () {
	
	}
	

	void Update () {
	
	}
}
