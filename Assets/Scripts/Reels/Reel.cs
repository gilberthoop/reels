using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using Random = UnityEngine.Random;

public class Reel : MonoBehaviour
{  
    //public event ReelsHandler OnFullStop;

    public Action OnFullStop;
    
    // Reel components (icons/sprites)
    public SpriteRenderer[] icons; 
    public Sprite[] symbols;
     
    // Visual 
    private SpriteRenderer currentIcon;
    private int symbolIndex;
    private float xPos;
    private float yPos;

    // Animation/Measurements
    private float speed;
    private float smoothTime;
    private float iconHeight;
    private float topBound;
    private float bottomBound;

    // Landing animation & calculations/timing  
    private int topMostIndex;
    private float yVelocity;
    private float endPoint;
    private float landingPos;  
    private float currentYpos; 
    
    // Reel status
    private bool canSpin;
    private bool isSpinning;
    private bool stopped;


    // Initialize reel components
    void Awake()
    {   
        speed = 25f; 
        smoothTime = 0.15f;
        iconHeight = 3f;
        topBound = 6f; 
        bottomBound = (topBound - iconHeight * 4);   
        yVelocity = 0.0f;

        // Current state of the reel
        stopped = true;
        // Can the reel spin at start up?
        canSpin = false;
        // Is the reel spinning at start up?
        isSpinning = false;

        // Create initial set of icons
        SetIcon(symbols); 
    }


    // Execute spin idling and landing animation controlled by flags
    // Do not call Stop function inside because it dispatches the full stop event
    void Update()
    {
        // If reel can spin, start spinning
        if (canSpin)
        {
            Spin();
        }
        else
        {
            if (isSpinning)
            {
                Stop(); 
            }

        }  
    } 

    // Create initial set of icons
    private void SetIcon(Sprite[] symbol)
    {
        for (int i = 0; i < icons.Length; i++)
        {
            // Get reference to the object
            currentIcon = icons[i];

            // Assign a sprite to an icon 
            currentIcon.sprite = symbol[0];

            // Assign the x & y positions of the icons
            xPos = currentIcon.transform.position.x; 
            yPos = topBound - (i * iconHeight); 

            // Set its position 
            currentIcon.transform.position = new Vector2(xPos, yPos);  
        }
    }


    // Render the set of icons
    private void RenderIcons(float speed)
    {
        // Indicate that the reels have started to spin
        canSpin = true;
        stopped = false;

        // The reels are now moving
        isSpinning = true; 

        for (int i = 0; i < icons.Length; i++)
        {
            // Get reference to the object
            currentIcon = icons[i];

            // Apply speed
            xPos = currentIcon.transform.position.x; 
            yPos = currentIcon.transform.position.y;
            yPos -= speed; 

            // Update the icon's new position
            currentIcon.transform.position = new Vector2(xPos, yPos);

            // Check bounds
            if (currentIcon.transform.position.y < bottomBound)
            {
                /*
                 * Update current icon position  
                 */
                yPos = topBound - speed;      
                currentIcon.transform.position = new Vector2(xPos, yPos);

                // Store its index
                topMostIndex = i; 

                // Give the icon a new random symbol
                symbolIndex = Random.Range(0, symbols.Length);
                icons[i].GetComponent<SpriteRenderer>().sprite = symbols[symbolIndex];
            }
        } 
    }


    // Start landing animation
    private void StartLanding()
    {  
        // Get reference to the object
        currentIcon = icons[topMostIndex];

        // Define start position for the animation
        currentYpos = currentIcon.transform.position.y;

        // Define the target landing position for the animation
        endPoint = topBound - iconHeight;

        // Apply easing to the icon's landing animation
        landingPos = Mathf.SmoothDamp(currentYpos, endPoint, ref yVelocity, smoothTime);

        // Gradually decrease idling speed until it reaches the "endpoint"   
        RenderIcons(currentYpos - landingPos);

        // Change the reel status to stop
        canSpin = false;  

        /*
         * This is the point where the reels have completely landed/stopped
         */
        if (currentYpos == landingPos)
        {
            // The reel is no longer spinning
            isSpinning = false;
            stopped = true;

            // Dispatch reel stopped state event
            if (OnFullStop != null)
            { 
                OnFullStop(); 
            }
            else
            {
                Debug.Log("OnFullStop event is null");
            } 
        } 
    }  


    // PUBLIC METHODS
    public void Spin()
    {
        // Move icons by speed * fixed delta time
        RenderIcons(speed * Time.fixedDeltaTime);
    }

    public void Stop()
    {
        StartLanding();

    }

    public bool HasStopped()
    {
        return stopped;
    }


}
