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
}

public class AudioMgr : MonoBehaviour {

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

    AudioSource audioSrc;

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
                audioSrc.PlayOneShot(EnemyDeath);
                break;

            case SfxVals.Sword:
                audioSrc.PlayOneShot(Sword);
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
        }
    }
}
