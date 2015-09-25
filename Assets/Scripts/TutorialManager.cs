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
            Application.LoadLevel(GameGlobalVariablesManager.MainMenu);
        }
        if (count > 3)
        {
            count = 3;
        }
        TutImg[count].SetActive(true);
    }


    public void PointerDownBack()
    {
        TutImg[count].SetActive(false);
        count -= 1;
        if(count<0)
        {
            count = 0;
        }
        TutImg[count].SetActive(true);
    }
}
