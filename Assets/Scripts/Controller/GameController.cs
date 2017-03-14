using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public SpinButton spinButton;
    public Reels reels;


    private void ReelSpinHandler()
    {
        reels.Spin();
    }

    private void ReelStopHandler()
    {
        reels.Stop();
    }

    void OnEnable()
    {
        spinButton.OnSpin += ReelSpinHandler;
        spinButton.OnStop += ReelStopHandler;
    }

    void OnDisable()
    {
        spinButton.OnSpin -= ReelSpinHandler;
        spinButton.OnStop -= ReelStopHandler;
    }
}
