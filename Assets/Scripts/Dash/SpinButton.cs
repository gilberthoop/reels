using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SpinButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // Spin buttom frames or skins
    public Sprite spinUp;
    public Sprite spinOver;
    public Sprite spinDown;
    public Sprite stopSpin;

    // Delegates and events
    [HideInInspector]
    public delegate void ClickToSpin();  
    [HideInInspector]
    public event ClickToSpin OnSpin;

    [HideInInspector]
    public delegate void ClickToStop();
    [HideInInspector]
    public event ClickToStop OnStop; 

    // Flags
    private bool spinStatus;
    private bool stopped; 


    // Use this for initialization
    void Start()
    {
        // Render the spin button 
        gameObject.GetComponent<SpriteRenderer>().sprite = spinUp; 

        // Set initial spin status to false and stopped status to true
        spinStatus = false;
        stopped = true; 
    } 

    // Change the button frame when mouse cursor enters the UI object  
    public void OnPointerEnter(PointerEventData eventData)
    { 
        gameObject.GetComponent<SpriteRenderer>().sprite = spinOver;
    }


    // Change the button frame when mouse cursor exits the UI object
    public void OnPointerExit(PointerEventData eventData)
    { 
        gameObject.GetComponent<SpriteRenderer>().sprite = spinUp;
    }


    // Event system interface implemented when mouse cursor clicks the UI object 
    public void OnPointerClick(PointerEventData eventData)
    {
        /*
         * When spin button is toggled on, the reel will spin and change spin buttom frame
         * Spin event will be dispatched
         */
        if (!spinStatus)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = stopSpin;

            InvokeSpin();

            spinStatus = true;
        }

        /*
         * When spin button is toggled off, the reels will start to land and change spin button frame
         * Stop event will be dispatched
         */
        else
        {
            // TODO Disable click  
            //gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = spinUp;

            InvokeStop();

            spinStatus = false; 
        }
        // Indicate that the reels have started to spin
        stopped = false; 
    } 

    
    // Invoke the spin and stop events
    public void InvokeSpin()
    {
        if (OnSpin != null)
        {
            OnSpin();
        }
    }
    
    public void InvokeStop()
    {
        if (OnStop != null)
        {
            OnStop();
        }
    }
     
}
