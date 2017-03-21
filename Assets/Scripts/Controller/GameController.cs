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
        reels.Spin += new Reels.ReelsHandler(ReelSpinHandler);
        reels.Stop += new Reels.ReelsHandler(ReelStopHandler);  
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


    // Reel handler when reels are landing/stopping
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
        reels.Stop -= ReelStopHandler; 
    } 
     
}
