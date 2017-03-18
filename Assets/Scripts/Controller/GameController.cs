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
        // Subscribe handlers to events
        button.ClickToSpin += new Button.ButtonHandler(ButtonSpinHandler);
        button.ClickToStop += new Button.ButtonHandler(ButtonStopHandler);

        reels.Spinning += new Reels.ReelsHandler(ReelStartHandler);
        reels.Stopping += new Reels.ReelsHandler(ReelStopHandler);
    }


    // Reel handler when reels are spinning
    private void ReelStartHandler()
    {
       // button.Disable();
    }

    // Reel handler when reels are landing/stopping
    private void ReelStopHandler()
    {
        /*
         * While the reels are spinning, disable SPIN button
         * Until the reels have fully stopped 
        if (!reels.HasStopped())
        {
            button.Disable();
        }
        else
        {
            button.Enable();
        }
        */
    }

    // Button handler for spinning
    private void ButtonSpinHandler()
    {
        reels.Spin();
    }

    // Button handler for stopping/landing
    private void ButtonStopHandler()
    {
        reels.Stop();
    }

    // Unsubscribe handlers to events
    void OnDisable()
    {
        button.ClickToSpin -= ButtonSpinHandler;
        button.ClickToStop -= ButtonStopHandler;
    }

}
