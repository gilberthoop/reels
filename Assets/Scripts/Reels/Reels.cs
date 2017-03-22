using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reels : MonoBehaviour
{

    public delegate void ReelsHandler();
    public event ReelsHandler Spin;
    public event ReelsHandler DisableSpin;
    public event ReelsHandler FullStop;

    // Reels components
    public Reel[] reels;
    private Reel currentReel;
    private Reel reel1, reel2, reel3;


    // Initialize Reels components
    void Awake()
    {
        reel1 = reels[0];
        reel2 = reels[1];
        reel3 = reels[2];

        // Add Stop handlers to the event of each reel
        reel1.FullyStopped += CompleteStopHandler;
        reel2.FullyStopped += CompleteStopHandler;
        reel3.FullyStopped += CompleteStopHandler;
    }


    // Set up the spin idle animation for the reels
    private void SpinReels()
    { 
        for (int i = 0; i < reels.Length; i++)
        {
            currentReel = reels[i];
            currentReel.Spin();
        }

        // Dispatch Spin event
        if (Spin != null)
        {
            Spin(); 
            Debug.Log("Reels are spinnning");
        }
        else
        {
            Debug.Log("Spin event is null");
        }
    } 


    // Set up the landing and stop animation for the reels
    private void StopReels()
    {
        bool fullStop = false;
        for (int i = 0; i < reels.Length; i++)
        { 
            currentReel = reels[i];
            currentReel.Stop(); 
        }

        // Dispatch Disable Spin event
        if (DisableSpin != null)
        {
            DisableSpin();
        }
        else
        {
            Debug.Log("DisableSpin event is null");
        }
    }


    // Handler when all reels have stopped 
    private void CompleteStopHandler()
    {
        // Dispatch Full Stop event
        if (FullStop != null)
        {
            FullStop();
            Debug.Log("All reels have completely stopped");
        }
        else
        {
            Debug.Log("FullStop event is null");
        }
    }
    

    // Unubscribe handlers from events
    void OnDisable()
    {
        reel1.FullyStopped -= CompleteStopHandler;
        reel2.FullyStopped -= CompleteStopHandler;
        reel3.FullyStopped -= CompleteStopHandler;
    }


    // PUBLIC METHODS
    public void Spinning()
    {
        SpinReels();
    }

    public void Stopping()
    {
        StopReels(); 
    } 
}
