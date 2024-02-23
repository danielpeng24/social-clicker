using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Random = System.Random;
using UnityEngine.UI;
//GRRRR

public class Controller : MonoBehaviour
{
    // Literally do not care about the naming scheme,
    // Too bad, python's naming has taken over, I'm not Capitalizing the second word, I'm using _
    
    //Change some of the vars to private later after testing

    private Random rng = new Random();
    public float boost_time;
    public Chatbox chat;
    private float time_till_random_comment;
   
    public float social_credits;
    private float max_social_credits;

    public bool lose_credits;

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
    
    private bool is_strike;
    public GameObject strike_image;
    private SpriteRenderer strike_sprite_renderer;
    private AudioSource strike_audio;
    private float strike_time_start;
    
    
    public GameObject execution_image;
    private SpriteRenderer execution_sprite_renderer;

     private void Start()
     {
         
         strike_sprite_renderer = strike_image.GetComponent<SpriteRenderer>();
         strike_audio = strike_image.GetComponentInChildren<AudioSource>();
         strike_audio.enabled = false;
         
         
         execution_sprite_renderer = execution_image.GetComponent<SpriteRenderer>();
         last_time_clicked = Time.time;
         time_till_random_comment = rng.Next(3, 10);

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
         // Check for certain ranges to say random comments
         time_till_random_comment -= Time.deltaTime;
         if (time_till_random_comment <= 0)
         {
             
             time_till_random_comment = rng.Next(15, 30);
             if (max_social_credits < 0)
             {
                 if (rng.Next(-1, 2) == 0)
                 {
                     chat.SendMessageToChat("CCP: Be warned!");
                 }
                 else
                 {
                     chat.SendMessageToChat("CCP: You are on the list of the outcasts!");
                 }
             }
             else if (max_social_credits == 0)
             {
                 if (rng.Next(-1, 2) == 0)
                 {
                     chat.SendMessageToChat("CCP: We expect much from you!");
                 }
                 else
                 {
                     chat.SendMessageToChat("Game: Nobody knows you, and nobody cares!");
                 }
             }
             // Don't ask me know this works, it just does, rider did this not me, I wrote 0 <= max_social_credits && max_social_credits <= 100
             else if (max_social_credits is >= 0 and <= 100)
             {
                 if (rng.Next(-1, 2) == 0)
                 {
                     chat.SendMessageToChat("Game: Some people start to see your patriotism!");
                 }
                 else
                 {
                     chat.SendMessageToChat("Game: You game a little fame");
                 }
             }
             else if (max_social_credits is >= 101 and <= 500)
             {
                 if (rng.Next(-1, 2) == 0)
                 {
                     chat.SendMessageToChat("Game: Many people start to notice your patriotism!");
                 }
                 else
                 {
                     chat.SendMessageToChat("CCP: WORK HARD! NOT SMART!");
                 }
             }
             else if (max_social_credits is >= 501 and <= 1500)
             {
                 if (rng.Next(-1, 2) == 0)
                 {
                     chat.SendMessageToChat("Game: You have gained some fame for your patriotism!");
                 }
                 else
                 {
                     chat.SendMessageToChat("CCP: WORK HARDER! NOT SMARTER!");
                 }
             }
             else if (max_social_credits is >= 1501 and <= 2000)
             {
                 if (rng.Next(-1, 2) == 0)
                 {
                     chat.SendMessageToChat("Game: You are now famous throughout China!");
                 }
                 else
                 {
                     chat.SendMessageToChat("CCP: You have been recognized!");
                 }
             }
             else
             {
                 if (rng.Next(-1, 2) == 0)
                 {
                     chat.SendMessageToChat("Game: You are now famous throughout the world!");
                 }
                 else
                 {
                     chat.SendMessageToChat("Game: People throughout the world praise you for you amount of credits");
                 }
             }
             chat.SendMessageToChat("");
         }

         // check for rewards
         switch (current_stage_of_achievements)
         {
             case 0:
                 if (max_social_credits > 500)
                 {
                     current_stage_of_achievements++;
                     current_multiplier += 0.025f;
                     chat.SendMessageToChat("Game: congratulations on getting better schooling +2.5% permanent SC boost");
                 }
                 break;
             case 1:
                 if (max_social_credits > 1000)
                 {
                     current_stage_of_achievements++;
                     current_multiplier += 0.05f;
                     chat.SendMessageToChat("Game: we accepted your uni application +2.5% permanent SC boost");
                 }

                 break;
             case 2:
                 if (max_social_credits > 2000)
                 {
                     current_stage_of_achievements++;
                     current_multiplier += 0.1f;
                     chat.SendMessageToChat("you got a job +10% permanent sc boost");
                 }

                 break;
             case 3:
                 if (max_social_credits > 100000)
                 {
                     current_stage_of_achievements++;
                     current_multiplier += 1f;
                     chat.SendMessageToChat("Game: you become a member of the ccp +100% permanent SC boost");
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
             is_strike = true;
             number_of_strikes++;
             
             strike_sprite_renderer.enabled = true;
             strike_audio.enabled = true;
             lose_credits = true;
             
             strike_time_start = Time.time;
             last_time_clicked = Time.time;
             chat.SendMessageToChat("CCP: You failed to click the button, the CPP be coming for you\n");
         }

         if (lose_credits)
         {
             social_credits *= ((float)0.95 * Time.deltaTime);
         }

         if (is_strike && Time.time - strike_time_start >= 2.5)
         {
             is_strike = false;
             strike_sprite_renderer.enabled = false;
             strike_audio.enabled = false;
             last_time_clicked = Time.time;
         }

         if (number_of_strikes >= max_number_of_strikes)
         {
             execution_sprite_renderer.enabled = true;
             chat.SendMessageToChat("CCP: You failed the game, the execution team is headed you way\n");
             // Application.Quit();
         }

 

}
}
