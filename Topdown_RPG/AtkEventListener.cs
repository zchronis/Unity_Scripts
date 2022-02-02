using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkEventListener : MonoBehaviour
{
    private AtkManager atkManager; 
    // Start is called before the first frame update
    void Start()
    {
        atkManager = GetComponent<AtkManager>();
    }

    public void AnimEvent()
    {
        //Debug.Log("Animation End Check");
        atkManager.checkAtkPhase();
    }
}
