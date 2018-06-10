using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoPickup : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<MonoPlayer>() != null)
        {
            gameObject.SetActive(false);
            MonoPickupManager.Instance.CheckAllPickups();
        }

        return;
    }
}
