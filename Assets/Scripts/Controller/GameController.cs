using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{ 

    // Game controller components
    public Button button;
    public Reels reels;


    // Add event listeners
    void Start()
    {
        /*
         * Subscribe handlers to events
         */
        // Button handlers
        button.ClickToSpin += ButtonSpinHandler;
        button.ClickToStop += ButtonStopHandler;

        // Reel handlers
        reels.Spin += ReelSpinHandler;
        reels.DisableSpin += ReelDisableSpinHandler;
        reels.FullStop += ReelStopHandler;
    } 


    // Button handler for spinning
    private void ButtonSpinHandler()
    {
        reels.Spinning(); 
    }


    // Button handler for stopping/landing
    private void ButtonStopHandler()
    {
        reels.Stopping(); 
    }


    // Reel handler when reels are spinning
    private void ReelSpinHandler()
    {
        button.StopFrame(); 
    }


    // reel handler when reels are landing/stopping
    private void ReelDisableSpinHandler()
    {
        button.Disable();
    }


    // Reel handler when reels have stopped
    private void ReelStopHandler()
    { 
        button.SpinFrame();
    }


    // Unsubscribe handlers to events
    void OnDisable()
    {
        button.ClickToSpin -= ButtonSpinHandler;
        button.ClickToStop -= ButtonStopHandler;
        reels.Spin -= ReelSpinHandler;
        reels.DisableSpin -= ReelDisableSpinHandler;
        reels.FullStop -= ReelStopHandler; 
    } 
     
}
