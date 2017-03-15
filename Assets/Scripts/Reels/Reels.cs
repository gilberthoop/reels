using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reels : MonoBehaviour
{

    // Reels components
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

    // Return the time it takes to finish the landing animation of each reel
    private float Done()
    {
        float result = 0f;

        for (int i = 0; i < reels.Length; i++)
        {
            currentReel = reels[i];
            result = currentReel.FinishSpin();
        }
        return result;
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

    public float DoneSpin()
    {
        return Done();
    }

}
