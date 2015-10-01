using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CoinAnim : MonoBehaviour {

    public List<GameObject> coinParticlesList;

	void Start ()
    {
    }
	

	void Update ()
    {
	}


    public void PlayCoinAnim(Vector3 newPos)
    {
        StartCoroutine(PlayCoinAnimAt(newPos));
    }


    IEnumerator PlayCoinAnimAt(Vector3 newPos)
    {
        float curTime = 0;
        int coinCount = Random.Range(3, coinParticlesList.Count);
        for (int i = 0; i < coinCount; i++)
        {
            coinParticlesList[i].SetActive(true);
            coinParticlesList[i].transform.position = newPos + new Vector3(Random.Range(0.5f, 3)-2, Random.Range(0, 3)-2, 0);
            //Debug.Log(coinParticlesList[i].transform.position.ToString());
        }
        while (curTime < 0.3f)
        {
            curTime += Time.deltaTime;
            transform.position
                    += new Vector3(0, Time.deltaTime * 10, 0);
            yield return null;
        }
        for (int i = 0; i < coinParticlesList.Count; i++)
        {
            coinParticlesList[i].SetActive(false);
            //Debug.Log(coinParticlesList[i].transform.position.ToString());
        }
        //transform.position = new Vector3(2000,2000,0);
    }


}
