using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerClickHandler
{

    /*
     * [HideInInspector]
    public delegate void ButtonHandler();
    [HideInInspector]
    public event ButtonHandler OnClickSpin;
    public event ButtonHandler OnClickStop;
    */
    public Action OnClickSpin;
    public Action OnClickStop;

    // Spin buttom frames or skins
    public Sprite spinUp;
    public Sprite stopUp; 
     
    // Flags
    private bool spinStatus; 


    // Use this for initialization
    void Start () {
        // Render the spin button  
        gameObject.GetComponent<SpriteRenderer>().sprite = spinUp;  

        // Can the button be clicked to spin the reels?
        spinStatus = true; 
    }

    
    /*
     * Event system interface implemented to handle mouse clicks on button
     * Dispatch the appropriate event when clicked
     */
    public void OnPointerClick(PointerEventData eventData)
    {
        /*
         * Toggle spin button (ON and OFF frames)
         * Dispatch Spin and Stop events
         */
        if (spinStatus)
        { 
            // Dispatch spin event when button is clicked
            if (OnClickSpin != null)
            {
                OnClickSpin();
            } 

            spinStatus = false;
        }
        else
        {  
            // Dispatch stop event when button is clicked
            if (OnClickStop != null)
            {
                OnClickStop();
            } 

            spinStatus = true;
        } 
    }  


    /*
     * PUBLIC METHODS 
     */
    // Change the button frame to SPIN
    public void ChangeToSpinFrame()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = spinUp;
    }

    // Change the button frame to STOP
    public void ChangeToStopFrame()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = stopUp; 
    }

    // Enable the button
    public void Enable()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    // Disable the button
    public void Disable()
    { 
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
} 
