using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reel : MonoBehaviour
{

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

    // Landing animation  
    private int topMostIndex;
    private float yVelocity;
    private float endPoint;
    private float landingPos;  
    private float currentYpos; 


    // Initialize reel components
    void Start()
    {  
        speed = 50f; 
        smoothTime = 0.3f;
        iconHeight = 3f;
        topBound = 6f;
        bottomBound = topBound - iconHeight * 4;
        yVelocity = 0.0f;

        SetIcon(symbols);
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
            if (currentIcon.transform.position.y <= bottomBound )
            {
                /*
                 * Update current icon position 
                 * Subtract the speed multiplied by the fixed delta time to avoid gaps 
                 */
                yPos = topBound - (speed * Time.fixedDeltaTime);      
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
    private void ExecuteStop()
    {
        // Get reference to the object
        currentIcon = icons[topMostIndex];

        // Define start position for the animation
        currentYpos = currentIcon.transform.position.y;

        // Define the target landing position for the animation
        endPoint = topBound - iconHeight;

        // Apply easing to the icon's landing animation
        landingPos = Mathf.SmoothDamp(currentYpos, endPoint, ref yVelocity, smoothTime);

        // Gradually decrease idling speed until 0  
        RenderIcons(currentYpos - landingPos);
    }


    // Start idling spin animation
    private void StartSpin()
    {
        // Move icons by speed * fixed delta time
        RenderIcons(speed * Time.fixedDeltaTime);
    }


    // Stop the reels
    private void StopSpin()
    {
        ExecuteStop();
    }



    // PUBLIC METHODS
    public void Spin()
    {
        StartSpin();
    }

    public void Stop()
    {
        StopSpin(); 
    }
}
