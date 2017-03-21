using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class Reel : MonoBehaviour
{
    public delegate bool ReelStateHandler(); 
    public event ReelStateHandler FullyStopped;
    
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
    private bool isSpinning;
    private bool fullyStopped;  



    // Initialize reel components
    void Awake()
    {   
        speed = 25f; 
        smoothTime = 0.3f;
        iconHeight = 3f;
        topBound = 6f;
        bottomBound = topBound - iconHeight * 4;
        yVelocity = 0.0f;

        // Is the reel spinning in the beginning?
        isSpinning = false;
        // Is the reel fully stopped in the beginning?
        fullyStopped = true; 

        SetIcon(symbols);
    }


    // Execute spin idling and landing animation controlled by flags
    void Update()
    {
        // If the spin signal is true, start spinning
        if (isSpinning)
        {
            Spin();
        }
        else
        {
            // Check if reels are not stopped, then stop
            if (!fullyStopped)
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
        isSpinning = true; 
        // Is the reel fully stopped?
        fullyStopped = false;

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
        fullyStopped = false; 

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
        isSpinning = false;

        /*
         * This is the point where the reels have completely landed/stopped
         */
        if (currentYpos == landingPos)
        {  
            fullyStopped = true;   

            // Dispatch reel stopped state event
            if (FullyStopped != null)
            {
                FullyStopped();
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
        return fullyStopped;
    } 

}
