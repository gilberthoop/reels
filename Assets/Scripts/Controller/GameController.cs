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

    private bool isClicked;


    // Add event listeners
    void Start()
    {
        isClicked = false;

        // Subscribe handlers to events
        button.ClickToSpin += new Button.ButtonHandler(ButtonSpinHandler);
        button.ClickToStop += new Button.ButtonHandler(ButtonStopHandler); 
        reels.Stop += new Reels.ReelsHandler(ReelStopHandler);
    }

    // Check reel landing status on every frame
    void Update()
    {
        if (isClicked)
        {
            ReelStopHandler();
        }
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


    // Reel handler when reels are landing/stopping
    private void ReelStopHandler()
    {
        isClicked = true;
        /*
        * While the reels are spinning, disable SPIN button
        * Until the reels have fully stopped
        */   
        if (!reels.Stopped())
        {
            button.Disable(); 
            Debug.Log("disable");
        }
        else
        {
            button.Enable();
            Debug.Log("enable");
            isClicked = false;
        }
    } 
    

    // Unsubscribe handlers to events
    void OnDisable()
    {
        button.ClickToSpin -= ButtonSpinHandler;
        button.ClickToStop -= ButtonStopHandler;
         
        reels.Stop -= ReelStopHandler;
    } 
     
}
