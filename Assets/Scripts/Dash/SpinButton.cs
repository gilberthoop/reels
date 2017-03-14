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
    private float delay;


    // Use this for initialization
    void Start()
    {
        // Render the spin button 
        gameObject.GetComponent<SpriteRenderer>().sprite = spinUp; 

        // Set initial spin status to false and stopped status to truel
        spinStatus = false;
        stopped = true;
        delay = 0f;
    }


    // Update is called once per frame
    void Update()
    {
        /*
         * Set click delay to 2 seconds between clicks - avoid overlapping call stack
         * If the spin status is true, start spinning
         * Else execute landing animation when reels are spinning
         */
        delay += Time.deltaTime;

         if (spinStatus)
         { 
              Spin();     
         }
         else
         { 
             if (!stopped)
             { 
                 Stop();   
             }
         }  
    }


    // Event system interface implemented when mouse cursor enters the UI object  
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (spinStatus)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = stopSpin;
            spinStatus = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = spinOver;
            spinStatus = false;
        }
    }


    // Event system interface implemented when mouse cursor exits the UI object
    public void OnPointerExit(PointerEventData eventData)
    {
        if (spinStatus)
        {
            // Change button frame 
            gameObject.GetComponent<SpriteRenderer>().sprite = stopSpin;
            spinStatus = true; 
        }
        else
        {
            // Change button frame
            gameObject.GetComponent<SpriteRenderer>().sprite = spinUp;
            spinStatus = false; 
        }
    }


    // Event system interface implemented when mouse cursor clicks the UI object
    // Dispatch click events
    public void OnPointerClick(PointerEventData eventData)
    {
        /**
         * When spin button is toggled on, the reel will spin and change spin buttom frame
         * Spin event will be dispatched
         * */
        if (!spinStatus)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = stopSpin;
            spinStatus = true;  
        }
        /**
         * When spin button is toggled off after 2 seconds, the reels will start to land and change spin button frame
         * Stop event will be dispatched
         * Reset delay
         * */
        else
        {
            if (delay > 2.0f)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = spinUp;
                spinStatus = false;
                delay = 0f;
            } 
        }
        // Indicate that the reels have started to spin
        stopped = false;
    }


    // Invoke the spin and stop events
    public void Spin()
    {
        if (OnSpin != null)
        {
            OnSpin();
        }
    }
    
    public void Stop()
    {
        if (OnStop != null)
        {
            OnStop();
        }
    }
}
