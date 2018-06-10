using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoPickupManager : MonoBehaviour {
    public static MonoPickupManager Instance;
    private void Awake()
    {
        Instance = this;
        endPoint.SetActive(false);

        return;
    }

    public GameObject[] pickupss;
    public GameObject endPoint;

    public void CheckAllPickups()
    {
        bool isAllPickupsDone = true;
        foreach(GameObject go in pickupss)
        {
            if (go != null && go.activeSelf)
            {
                isAllPickupsDone = false;
            }
        }
        endPoint.SetActive(isAllPickupsDone);

        return;
    }
}
