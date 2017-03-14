using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reels : MonoBehaviour
{

    public Reel[] reels;

    private Reel currentReel;


    // Set up the spin idle animation for the reels
    private void StartSpin()
    {
        for (int i = 0; i < reels.Length; i++)
        {
            currentReel = reels[i];
            currentReel.Spin();
        }
    }

    // Set up the landing and stop animation for the reels
    private void StopSpin()
    {
        for (int i = 0; i < reels.Length; i++)
        {
            currentReel = reels[i];
            currentReel.Stop();
        }
    }



    // PUBLIC METHODS
    public void Spin()
    {
        StartSpin();
    }

    public void Stop()
    {
        StopSpin();
    }
}
