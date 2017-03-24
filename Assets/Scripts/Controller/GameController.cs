using System;
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
    void Awake()
    {
        /*
         * Subscribe handlers to events
         */
        // Reel handlers 
        reels.OnReelsFullStop += ReelStopHandler;
         
        // Button handlers
        button.OnClickSpin += ButtonClickSpinHandler;
        button.OnClickStop += ButtonStopHandler;
    } 


    // Button handler for spinning
    private void ButtonClickSpinHandler()
    {
        // Start spinning & Change button to stop frame
        reels.Spin(); 
        button.ChangeToStopFrame();
    }


    // Button handler for stopping/landing
    private void ButtonStopHandler()
    {
        // Disable the button & Start landing animation 
        button.Disable();
        reels.Stop();  
    }
     

    // Reel handler when reels have stopped
    private void ReelStopHandler(object sender, EventArgs eventArgs)
    {
        // Enable the button & Change the button to spin frame
        button.Enable();
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
