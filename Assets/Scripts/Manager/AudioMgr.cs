using UnityEngine;
using System.Collections;

public enum SfxVals
{
    CoinCollect = 0,
    CrateCrash,
    PotCrash,
    EnemyDeath,
    Sword,
    Knife,
    Bomb,
    Cyclone,
    CameraLock,
    FrogTalk,
    ButtonClick,
    BuyItem
}

public class AudioMgr : MonoBehaviour {

    public AudioSource audioSrc;
    public AudioSource audioSrcBg;

    public AudioClip CoinCollect;
    public AudioClip CrateCrash;
    public AudioClip PotCrash;
    public AudioClip EnemyDeath;
    public AudioClip Sword;
    public AudioClip Knife;
    public AudioClip Bomb;
    public AudioClip Cyclone;
    public AudioClip CameraLock;
    public AudioClip FrogTalk;
    public AudioClip ButtonClick;
    public AudioClip BuyItem;
    
    private static AudioMgr instance = null;
    public static AudioMgr Inst
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<AudioMgr>();
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


	void Start () {
	}
	
	void Update () {
	}

    public void PlaySfx(SfxVals curSfxVal)
    {
        if(GameGlobalVariablesManager.isSoundMuted)
            return;
        switch(curSfxVal)
        {
            case SfxVals.CoinCollect:
                audioSrc.PlayOneShot(CoinCollect);
                break;

            case SfxVals.CrateCrash:
                audioSrc.PlayOneShot(CrateCrash);
                break;

            case SfxVals.PotCrash:
                audioSrc.PlayOneShot(PotCrash);
                break;

            case SfxVals.EnemyDeath:
                audioSrc.PlayOneShot(EnemyDeath, 0.2f);
                break;

            case SfxVals.Sword:
                audioSrc.PlayOneShot(Sword, 0.5f);
                break;

            case SfxVals.Knife:
                audioSrc.PlayOneShot(Knife);
                break;

            case SfxVals.Bomb:
                audioSrc.PlayOneShot(Bomb);
                break;

            case SfxVals.Cyclone:
                audioSrc.PlayOneShot(Cyclone);
                break;

            case SfxVals.CameraLock:
                audioSrc.PlayOneShot(CameraLock);
                break;

            case SfxVals.FrogTalk:
                audioSrc.PlayOneShot(FrogTalk);
                break;

            case SfxVals.ButtonClick:
                audioSrc.PlayOneShot(ButtonClick, 0.8f);
                break;

            case SfxVals.BuyItem:
                audioSrc.PlayOneShot(BuyItem, 0.8f);
                break;
        }
    }

    public void MusicToggle()
    {
        audioSrcBg.mute = GameGlobalVariablesManager.isSoundMuted;
    }
}
