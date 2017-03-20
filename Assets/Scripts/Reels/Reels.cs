using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reels : MonoBehaviour
{

    public delegate void ReelsHandler();  
    public event ReelsHandler Stop;

    // Reels components
    public Reel[] reels;

    private Reel currentReel;


    void OnEnable()
    { 
        currentReel.ReelStopped += ReelStopHandler;
    }


    // Set up the spin idle animation for the reels
    private void ReelStartHandler()
    {
        for (int i = 0; i < reels.Length; i++)
        {
            currentReel = reels[i];
            currentReel.Spin(); 
        }
    } 


    // Set up the landing and stop animation for the reels
    private void ReelStopHandler()
    { 
        for (int i = 0; i < reels.Length; i++)
        {
            currentReel = reels[i];
            currentReel.Stop();

            //  Dispatch stop event
            if (Stop != null)
            {
                Stop();
            }
        }  
    } 

    void OnDisable()
    {   
        currentReel.ReelStopped -= ReelStopHandler; 
    }

     
    // PUBLIC METHODS
    public void Spinning()
    {
        ReelStartHandler();
    }

    public void Stopping()
    {
        ReelStopHandler();
    }

    public float GetLandingTime()
    {
        float landingTime = 0f;

        for (int i = 0; i < reels.Length; i++)
        {
            currentReel = reels[i];
            landingTime = currentReel.GetLandingTime();
        }

        return landingTime;
    }


    public bool Stopped()
    {
        bool IsStopped = false;

        for (int i = 0; i < reels.Length; i++)
        {
            currentReel = reels[i];
            IsStopped = currentReel.HasStopped();
        }

        return IsStopped;
    }

}
