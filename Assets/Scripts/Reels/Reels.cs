using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reels : MonoBehaviour
{

    public delegate void ReelsHandler(); 
    public event ReelsHandler Spinning;
    public event ReelsHandler Stopping;

    // Reels components
    public Reel[] reels;

    private Reel currentReel;


    void Start()
    {
        for (int i = 0; i < reels.Length; i++)
        {
            currentReel = reels[i];  
        }
    }


    // Set up the spin idle animation for the reels
    private void ReelStartHandler()
    {
        for (int i = 0; i < reels.Length; i++)
        {
            currentReel = reels[i];
            currentReel.Spin();
        }

        // Dispatch start event
        if (Spinning != null)
        {
            Spinning();
        }
    } 


    // Set up the landing and stop animation for the reels
    private void ReelStopHandler()
    {
        for (int i = 0; i < reels.Length; i++)
        {
            currentReel = reels[i];
            currentReel.Stop();
        }

        // Dispatch stop event
        if (Stopping != null)
        {
            Stopping();
        }
    }

    // Check if reels have stopped
    private bool IsStopping()
    {
        bool stopStatus = false;
        for (int i = 0; i < reels.Length; i++)
        {
            currentReel = reels[i];
            stopStatus = currentReel.hasStopped();
        }
        return stopStatus;
    }
     

    void OnEnable()
    {
        currentReel.ReelStarted += ReelStartHandler;
        currentReel.ReelStopped += ReelStopHandler; 
    } 

    void OnDisable()
    {  
        currentReel.ReelStarted -= ReelStartHandler;
        currentReel.ReelStopped -= ReelStopHandler; 
    }

     
    // PUBLIC METHODS
    public void Spin()
    {
        ReelStartHandler();
    }

    public void Stop()
    {
        ReelStopHandler();
    }

    public bool HasStopped()
    {
        return IsStopping();
    }

}
