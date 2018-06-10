using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoEndPoint : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<MonoPlayer>())
        {
            MonoAIManager.Instance.OnLevelEnd(true);
        }

        return;
    }
}
