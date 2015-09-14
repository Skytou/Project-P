using UnityEngine;
using System.Collections;

public class SpawnTrigger : MonoBehaviour
{
    #region Var

    public static int prevPlat;
    public static int beforePrevPlat;
    public static int twoBeforePrevPlat;
    public Vector3[] distBtnPlatforms; //  starts from index 1

    int randNo;
    GameObject instance;

    #endregion var

    void Start()
    {
        prevPlat = 1;
    }

    void Update()
    {

    }

    #region SpawnHandler

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Trigger");
            randNo = Random.Range(1, 3);

            instance = Pooler.InstRef.GetPooledObject(randNo);
            instance.transform.position = transform.parent.transform.parent.transform.position + distBtnPlatforms[1];
            instance.SetActive(true);

            // Update spawn history
            twoBeforePrevPlat = beforePrevPlat;
            beforePrevPlat = prevPlat;
            prevPlat = randNo;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" /*&& DogRunner.instRef.runStart*/)//check - is game running if needed 
        {
            Pooler.InstRef.Sleep((gameObject.transform.parent.transform.parent.gameObject));
        }
    }

    #endregion SpawnHandler
}
