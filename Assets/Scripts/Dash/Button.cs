using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerClickHandler
{

    [HideInInspector]
    public delegate void ButtonHandler();
    [HideInInspector]
    public event ButtonHandler ClickToSpin;
    public event ButtonHandler ClickToStop;

    // Spin buttom frames or skins
    public Sprite spinUp;
    public Sprite stopUp; 
     
    // Flags
    private bool spinStatus; 


    // Use this for initialization
    void Start () {
        // Render the spin button  
        gameObject.GetComponent<SpriteRenderer>().sprite = spinUp;  

        // Set initial spin status to false and stopped status to true
        spinStatus = false; 
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
        if (!spinStatus)
        {
            // Toggle button frame 
            gameObject.GetComponent<SpriteRenderer>().sprite = stopUp;
             
            // Dispatch spin event when button is clicked
            if (ClickToSpin != null)
            {
                ClickToSpin(); 
                Debug.Log("SPIN");
            }
            else
            {
                Debug.Log("Event cannot be dispatched. Event is null");
            }

            spinStatus = true;
        }
        else
        {
            // Toggle button frame 
            gameObject.GetComponent<SpriteRenderer>().sprite = spinUp;
             
            // Dispatch stop event when button is clicked
            if (ClickToStop != null)
            {
                ClickToStop(); 
                Debug.Log("STOP");
            }
            else
            {
                Debug.Log("Event cannot be dispatched. Event is null");
            }

            spinStatus = false;
        } 
    }  


    // PUBLIC METHODS 
    public void Enable()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void Disable()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
} 
