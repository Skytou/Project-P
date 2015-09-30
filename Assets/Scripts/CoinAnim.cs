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
        transform.position = newPos;
        float curTime = 0;
        for (int i = 0; i < coinParticlesList.Count; i++)
        {
            coinParticlesList[i].transform.position = newPos + new Vector3(Random.Range(1, 5), Random.Range(1, 2), 0);
        }
        while (curTime < 5f)
        {
            curTime += Time.deltaTime;
            for (int i = 0; i < coinParticlesList.Count; i++)
            {
                coinParticlesList[i].transform.position
                    += new Vector3(0, Time.deltaTime * 10, 0);
            }
            yield return null;
        }
        transform.position = new Vector3(10000,5000,0);
    }


}
