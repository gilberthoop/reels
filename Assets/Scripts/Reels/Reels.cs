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
    private Reel reel1;
    private Reel reel2;
    private Reel reel3;


    void Start()
    {
        reel1 = reels[0];
        reel2 = reels[1];
        reel3 = reels[2]; 
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

    // Add handlers to the events
    void OnEnable()
    {  
        if (reel1 != null)
        {
            reel1.ReelStopped += ReelStopHandler;
        } 

        if (reel2 != null)
        {
            reel2.ReelStopped += ReelStopHandler;
        } 

        if (reel3 != null)
        {
            reel3.ReelStopped += ReelStopHandler;
        } 
    }

    // Unsubscribe handler to the events
    void OnDisable()
    {
        if (reel1 != null)
        {
            reel1.ReelStopped -= ReelStopHandler;
        }

        if (reel2 != null)
        {
            reel2.ReelStopped -= ReelStopHandler;
        }

        if (reel3 != null)
        {
            reel3.ReelStopped -= ReelStopHandler;
        }
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
