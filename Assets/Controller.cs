using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    // Literally do not care about the naming scheme,
    // Too bad, python's naming has taken over, I'm not Capitalizing the second word, I'm using _
    
    //Change some of the vars to private later after testing

    public float social_credits;
    private float max_social_credits;
    public GameObject social_credits_text;
    private Text _social_credits_text;

    public int labor_workers;
    public float labor_workers_cost = 10f;

    public float base_clicks = 1;
    public float current_multiplier = 1f;
    
    public float last_time_clicked;
    public float max_time_not_clicked = 5f; //For testing change to 20f later

    private int current_stage_of_achievements;
    private int job_promotions;

    private int number_of_strikes;
    public int max_number_of_strikes = 3;
    

    public GameObject execution_image;
    private SpriteRenderer execution_sprite_renderer;

     private void Start()
     {
         _social_credits_text = social_credits_text.GetComponent<Text>();
         execution_sprite_renderer = execution_image.GetComponent<SpriteRenderer>();
         last_time_clicked = Time.time;

     }

//
     private void Update()
     {
         // Add the labor workers social credits
         social_credits += labor_workers * 0.1f * current_multiplier * Time.deltaTime;
         // Check if current SC is the largest score there is
         if (social_credits > max_social_credits)
         {
             max_social_credits = social_credits;
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
             current_multiplier += 0.025f;
         }
         
         // check for punishments
         if (Time.time - last_time_clicked > max_time_not_clicked)
         {
             last_time_clicked = Time.time;
             number_of_strikes++;
             execution_sprite_renderer.enabled = true;
             Debug.Log("You failed to click the button, the CPP be coming for you");
         }

         if (number_of_strikes > max_number_of_strikes)
         {
             Debug.Log("You failed the game, the execution team is headed you way");
         }
         
         
         // Update the social credits text at the end
         _social_credits_text.text = $"Social Credits: {(int)social_credits} SC"; 
//         
     }
}
