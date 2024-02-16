using System;
using UnityEngine;
using System.Collections;
using Microsoft.ML.OnnxRuntime.Tensors;
using UnityEngine.EventSystems;
using UnityEngine.UI;
 
public class MyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Literally do not care about the naming scheme,
    // Too bad, python's naming has taken over, I'm not Capitalizing the second word, I'm using _
    public bool button_pressed;
    
    public float social_credits;
    private float max_social_credits;
    public Text score_text;

    public float current_multiplier = 1f;
    public float max_time_not_clicked = 20f;
    
    private int current_stage_of_achievements;
    private int job_promotions;

    public int number_of_strikes;
    public int max_number_of_strikes = 3;

    private float last_time_clicked = Time.time;


    public void OnPointerDown(PointerEventData eventData)
    {
        button_pressed = true;
    }
    
    public void OnPointerUp(PointerEventData eventData){
        button_pressed = false;
    }

    private void Update()
    {
        // Check if current SC is the largest score there is
        if (social_credits > max_social_credits)
        {
            max_social_credits = social_credits;
        }
        
        if (Input.GetMouseButtonDown(0) && button_pressed)
        {
            last_time_clicked = Time.time;
            social_credits += 1 * current_multiplier;
            score_text.text = "Score: " + social_credits;
        }
        
        // check for rewards
        switch (current_stage_of_achievements)
        {
            case 0:
                if (social_credits > 500)
                {
                    // Say in chat "Gained Better Schooling" 
                    current_stage_of_achievements++;
                    current_multiplier += 0.025f;
                }
                break;
            case 1:
                if (social_credits > 1000)
                {
                    //Say in chat "Admitted to a Great University" 
                    current_stage_of_achievements++;
                    current_multiplier += 0.05f;
                }

                break;
            case 2:
                if (social_credits > 2000)
                {
                    current_stage_of_achievements++;
                    current_multiplier += 0.1f;
                }

                break;
            case 3:
                if (social_credits > 100000)
                {
                    current_stage_of_achievements++;
                    current_multiplier += 1f;
                }
                break;
                
        }
        // check if we have more than 2000 SC and increment the job promotion of +2.5%
        if ((max_social_credits - 2500) / 1000 > job_promotions)
        {
            job_promotions++;
            current_multiplier += 0.05f;
        }
        
        
        
        
        
        
        // check for punishments
        if (Time.time - last_time_clicked > max_time_not_clicked)
        {
            last_time_clicked = Time.time;
            number_of_strikes++;
            Debug.Log("You failed to click the button, the CPP be coming for you");
        }

        if (number_of_strikes > max_number_of_strikes)
        {
            Debug.Log("You failed the game, the execution team is headed you way");
        }
        
        
        
    }
}