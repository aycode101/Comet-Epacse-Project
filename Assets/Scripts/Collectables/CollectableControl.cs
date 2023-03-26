using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableControl : MonoBehaviour
{
    public static int coinCount;
    public GameObject coinCountDisplay;

    void Update()
    {
        coinCountDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = "" + coinCount;
        /* TMPro.TextMeshProUGUI
         * TextMeshProUGUI
         * */
    }
}
