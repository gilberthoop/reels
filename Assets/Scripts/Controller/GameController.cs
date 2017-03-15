using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{

    // Game controller components
    public SpinButton spinButton;
    public Reels reels;
     
    // Spin the reels
    private void ReelSpinHandler()
    {
        reels.Spin();
    }
    
    // Stop the reels
    private void ReelStopHandler()
    {
        reels.Stop();
    }

    // Return the landing time
    private float FinishSpin()
    {
        return reels.DoneSpin();
    }

    // Subscribe handlers to the events
    void OnEnable()
    {
        spinButton.OnSpin += ReelSpinHandler;
        spinButton.OnStop += ReelStopHandler;
        spinButton.SpinStatus += FinishSpin;
    }


    // Unsubscribe handlers from the events
    void OnDisable()
    {
        spinButton.OnSpin -= ReelSpinHandler;
        spinButton.OnStop -= ReelStopHandler;
        spinButton.SpinStatus -= FinishSpin;
    }
}
