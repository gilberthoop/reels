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
        button.OnClickSpin += ButtonClickSpinHandler;
        button.OnClickStop += ButtonStopHandler;

        // Reel handlers 
        reels.OnReelsFullStop += ReelStopHandler;
    } 


    // Button handler for spinning
    private void ButtonClickSpinHandler()
    {
        reels.Spin();
        button.ChangeToStopFrame();
    }


    // Button handler for stopping/landing
    private void ButtonStopHandler()
    {
        reels.Stop();
        button.Disable();
    }
     

    // Reel handler when reels have stopped
    private void ReelStopHandler()
    { 
        button.ChangeToSpinFrame();
    }


    // Unsubscribe handlers to events
    void OnDisable()
    {
        button.OnClickSpin -= ButtonClickSpinHandler;
        button.OnClickStop -= ButtonStopHandler; 
        reels.OnReelsFullStop -= ReelStopHandler; 
    } 
     
}
