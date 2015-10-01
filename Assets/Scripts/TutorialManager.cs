using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour {
    public GameObject [] TutImg;
    public bool canClick;
    public GameObject ForwardBtn;
    public GameObject Backbutton;
    int count;
	
	void Start ()
    {
	}
	
	
	void Update ()
    {
	}


    public void PointerDownForward()
    {
        TutImg[count].SetActive(false);
        count += 1;
        if (count >= 4)
        {
            if (SavedData.Inst.GetGamePlayCount() <= 1)
            {
                Application.LoadLevel(GameGlobalVariablesManager.LevelSelection);
            }
            else
                MainMenu();
        }
        if (count > 3)
        {
            count = 3;
        }
        else
        {
            AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
        }
        TutImg[count].SetActive(true);
    }


    public void PointerDownBack()
    {
        TutImg[count].SetActive(false);
        count -= 1;
        if (count < 0)
        {
            count = 0;
        }
        else
        {
            AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
        }
        TutImg[count].SetActive(true);
    }

    public void MainMenu()
    {
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
        Application.LoadLevel(GameGlobalVariablesManager.MainMenu);
    }
}
