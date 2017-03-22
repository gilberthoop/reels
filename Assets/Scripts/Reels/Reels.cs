using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reels : MonoBehaviour
{

    public delegate void ReelsHandler();
    public event ReelsHandler Spin;
    public event ReelsHandler Stop; 

    // Reels components
    public Reel[] reels;
    private Reel currentReel;
    private Reel reel1, reel2, reel3;

    private bool isMoving;
    private bool isStopping;
     

    // Initialize each reel
    void Start()
    { 
        reel1 = reels[0];
        reel2 = reels[1];
        reel3 = reels[2];
         
        // Are the reels moving?
        isMoving = false;

        // Has the reels run
        isStopping = false;
    }

    // Update the status of reels
    void Update()
    {   
        if (isStopping)
        {
            ReelStatusHandler();
            //Debug.Log(ReelStatusHandler()); 
        }
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
        }
        // Reels moving
        isMoving = true;
        isStopping = false;

        Debug.Log("running, is moving:  " + isMoving);
    } 


    // Set up the landing and stop animation for the reels
    private void StopReels()
    {
        for (int i = 0; i < reels.Length; i++)
        { 
            currentReel = reels[i];
            currentReel.Stop();
        }

        // Reels will stop but still moving
        isMoving = true;
        isStopping = true;

        Debug.Log("STOPPING, is moving:    " + isMoving);
    }


    // Handler to check if reels have COMPLETELY stopped
    private bool ReelStatusHandler()
    {
        // Reels have completely stopped 
        //isMoving = true; 

        if (AllStopped())
        {
            // Dispatch full stop event 
            if (Stop != null)
            {
                Stop(); 
            }
        }

        Debug.Log("All stopped, is moving:   " + isMoving);

        return AllStopped();
    }
     

    // Verify that the reels have fully stopped
    private bool AllStopped()
    {
        bool fullyStopped = false;

        for (int i = 0; i < reels.Length; i++)
        {
            currentReel = reels[i];
            fullyStopped = currentReel.HasStopped(); 
        }

        if (fullyStopped)
        { 
            // Reels have completely stopped 
            isMoving = false;
            //isStopping = true;
        }

        return fullyStopped;
    }


    // Subscribe handlers to events
    void OnEnable()
    {

        if (reel1 != null)
        {
            reel1.FullyStopped += ReelStatusHandler;
        }

        if (reel2 != null)
        {
            reel2.FullyStopped += ReelStatusHandler;
        }

        if (reel3 != null)
        {
            reel3.FullyStopped += ReelStatusHandler;
        }
    }

    // Unubscribe handlers to events
    void OnDisable()
    {
        reel1.FullyStopped -= ReelStatusHandler;
        reel2.FullyStopped -= ReelStatusHandler;
        reel3.FullyStopped -= ReelStatusHandler;
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

    public bool IsMoving()
    {
        return isMoving; 
    }

}
