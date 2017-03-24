using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reels : MonoBehaviour
{

    public delegate void ReelsHandler(); 
    //public event ReelsHandler OnReelsFullStop;
    public event EventHandler OnReelsFullStop;


    // Reels components
    public Reel[] reels;
    private Reel currentReel;
    private Reel reel1, reel2, reel3;
    private bool stopped;


    // Initialize Reels components
    void Awake()
    {
        reel1 = reels[0];
        reel2 = reels[1];
        reel3 = reels[2];

        // Add Stop handlers to the event of each reel
        reel1.OnFullStop += CompleteStopHandler;
        reel2.OnFullStop += CompleteStopHandler;
        reel3.OnFullStop += CompleteStopHandler;

        stopped = true;
    }


    // Set up the spin idle animation for the reels
    private void SpinReels()
    {  
        reel1.Spin();
        reel2.Spin();
        reel3.Spin();
    } 


    // Set up the landing and stop animation for the reels
    private void StopReels()
    {   
        reel1.Stop();
        reel2.Stop();
        reel3.Stop();
    }


    // Helper method to check the current moving status of reels
    private bool CheckStatus()
    { 
        for (int i = 0; i < reels.Length; i++)
        { 
            currentReel = reels[i];
            stopped = currentReel.HasStopped(); 
        }  

        return stopped;
    }


    // Handler when all reels have stopped 
    private void CompleteStopHandler(object sender, EventArgs eventArgs)
    {
        // All reels need to be completely stopped before dispatching the event 
        if (CheckStatus())
        {
            // Dispatch Full Stop event
            if (OnReelsFullStop != null)
            {
                OnReelsFullStop(sender, eventArgs);
                //Debug.Log("Sender object: " + sender); 
            }
            else
            {
                Debug.Log("FullStop event is null");
            }
        }
    }
    

    // Unubscribe handlers from events
    void OnDisable()
    {
        reel1.OnFullStop -= CompleteStopHandler;
        reel2.OnFullStop -= CompleteStopHandler;
        reel3.OnFullStop -= CompleteStopHandler; 
    } 


    // PUBLIC METHODS
    public void Spin()
    {
        SpinReels();
    }

    public void Stop()
    {
        StopReels();
    } 
}
