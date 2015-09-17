using UnityEngine;
using System.Collections.Generic;
using Prime31;


public class IABUIManager : MonoBehaviourGUI
{
#if UNITY_ANDROID
	void OnGUI()
	{
		beginColumn();

		if( GUILayout.Button( "Initialize IAB" ) )
		{
			var key = "your public key from the Android developer portal here";
			key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAviAOI4AdVKsLQbvDj+17rhM3Khhoq3DexB+6PR/PVJbTnuvMfr++Yr7unzbWHTnDdOMp3E+Wf0zbitCoJBtb7rFgoSz05Z23lYAxk94YT9/JuHJHgFFKoAAU0AK9/oBCaZVsQ57WrAxrOOv+RYMH4WZpHKaRrtN4YXX1hE8enmIqS9ZMZgCqCely+kVGi2Szbh7NoGljoIeL0hrhCknu0dM1QC+t/28zwPJrNuWX7ziQA0sijo3L9h41BmyZPnDLUW7mK2Gp493e9Kx9FKkvnCmz4C8VoCvJbPsZcq/sEG6gwf5NgUU1AxrZtVun6Um/H1m2qZta9+mdLuuGcnLfzQIDAQAB";
			GoogleIAB.init( key );
		}


		if( GUILayout.Button( "Query Inventory" ) )
		{
			// enter all the available skus from the Play Developer Console in this array so that item information can be fetched for them
            var skus = new string[] { "consume_100_crystals", "consume_100_coins", "consume_200_coins", "weeklyupdate", "android.test.purchased" };
			GoogleIAB.queryInventory( skus );
		}

        if (GUILayout.Button("Are subscriptions supported?"))
        {
            Debug.Log("subscriptions supported: " + GoogleIAB.areSubscriptionsSupported());
        }


        if (GUILayout.Button("Purchase Test Product"))
        {
            GoogleIAB.purchaseProduct("consume_100_coins");
        }


        if (GUILayout.Button("Consume Test Purchase"))
        {
            GoogleIAB.consumeProduct("consume_100_coins");
        }


        if (GUILayout.Button("Test Unavailable Item"))
        {
            GoogleIAB.purchaseProduct("android.test.item_unavailable");
        }


        endColumn(true);


        if (GUILayout.Button("Purchase Real Product"))
        {
            GoogleIAB.purchaseProduct("android.test.purchased", "payload that gets stored and returned");
        }


        if (GUILayout.Button("Purchase Real Subscription"))
        {
            GoogleIAB.purchaseProduct("weeklyupdate", "subscription weeklyupdate");
        }


        if (GUILayout.Button("Consume Real Purchase"))
        {
            GoogleIAB.consumeProduct("consume_200_coins");
        }


        if (GUILayout.Button("Enable High Details Logs"))
        {
            GoogleIAB.enableLogging(true);
        }


        if (GUILayout.Button("Consume Multiple Purchases"))
        {
            var skus = new string[] { "com.prime31.testproduct", "android.test.purchased" };
            GoogleIAB.consumeProducts(skus);
        }

        endColumn();
	}
#endif
}
