using System;
using UnityEngine;
using Random = System.Random;

/*
 * Everything that isn't stated to be written by Brian is
 * 
 * */

public class Controller : MonoBehaviour
{
    // Literally do not care about the naming scheme,
    // Too bad, python's naming has taken over, I'm not Capitalizing the second word, I'm using "_"

    private Random rng = new Random();
    
    // Clever variable use, where this starts at -1 in order to not have our multiplier set to 0 at the start by the 
    // boost reset which occurs at boost_time == 0.
    public float boost_time = -1f;
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
    
    public GameObject taiwan_button;
    private Transform taiwan_current_position;
    private float time_till_taiwan_spawns;
    private bool taiwan_pressed;
    
    //Punishments
    // vars involving strikes
    private bool is_strike;
    public int number_of_strikes;
    public int max_number_of_strikes = 3;
    public GameObject strike_image;
    private SpriteRenderer strike_sprite_renderer;
    private AudioSource strike_audio;
    private float strike_time_start;
    // vars involving clicking the taiwan image, if not we have a taiwan strike
    public bool taiwan_strike;
    public GameObject taiwan_strike_image;
    private SpriteRenderer taiwan_strike_sprite_renderer;
    private AudioSource taiwan_strike_audio;
    private float taiwan_strike_time_start;
    
    // If 3 strikes are given, the execution team heads over and the game ends.
    public GameObject execution_image;
    private SpriteRenderer execution_sprite_renderer;

    public void Is_taiwan_pressed()
    {
        taiwan_pressed = true;
    }
    
     private void Start()
     {
         strike_sprite_renderer = strike_image.GetComponent<SpriteRenderer>();
         strike_audio = strike_image.GetComponentInChildren<AudioSource>();
         strike_audio.enabled = false;

         taiwan_strike_sprite_renderer = taiwan_strike_image.GetComponent<SpriteRenderer>();
         taiwan_strike_audio = taiwan_strike_image.GetComponentInChildren<AudioSource>();
         taiwan_strike_audio.enabled = false;
         
         
         execution_sprite_renderer = execution_image.GetComponent<SpriteRenderer>();
         last_time_clicked = Time.time;
         time_till_random_comment = rng.Next(3, 10);

         time_till_taiwan_spawns = rng.Next(20, 60);
         taiwan_current_position = taiwan_button.GetComponent<Transform>();
         taiwan_button.SetActive(false);
     }
     
     private void STRIKE(bool is_strike)
     {
         if (is_strike)
         {
             number_of_strikes++;
             strike_sprite_renderer.enabled = true;
             strike_audio.enabled = true;
         }
         else
         {
             strike_sprite_renderer.enabled = false;
             strike_audio.enabled = false;
         }
     }

     private void TAIWAN_STRIKE(bool is_strike)
     {
         if (is_strike)
         {
             taiwan_strike_sprite_renderer.enabled = true;
             taiwan_strike_audio.enabled = true;
         }
         else
         {
             taiwan_strike_sprite_renderer.enabled = false;
             taiwan_strike_audio.enabled = false;
         }
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
                    chat.SendMessageToChat("Game: You gain a little fame");
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
                    chat.SendMessageToChat("CCP: KEEP WORK HARDER! NOT SMARTER!");
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
                    current_multiplier += 0.069f;
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

        // Written by Cody, guided by Brian
        if (labor_workers == 10)
        {
            chat.SendMessageToChat("You have a following, the CCP has given you 1 worker");
            labor_workers += 1;
        }
        else if (labor_workers == 50) 
        {
            chat.SendMessageToChat("You are now a factory");
            labor_workers += 1;
        }
        else if (labor_workers == 100)
        { 
            chat.SendMessageToChat("Your factory is now full and the workers are now angry");
            labor_workers += 1;

        }
        else if (labor_workers == 200)
        {
            chat.SendMessageToChat("You have too many workers, the people now think that you are a capitalist");
            chat.SendMessageToChat("The CCP takes away 50 workers");
            labor_workers -= 50;
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
            STRIKE(true);
            lose_credits = true;
             
            strike_time_start = Time.time;
            last_time_clicked = Time.time;
            chat.SendMessageToChat("CCP: You failed to click the button, the CPP be coming for you\n");
        }
        
        // Written by Daniel Peng, fixed bug and refactored by Brian
        // Make sure that this ony occurs once, thus == 103, rather than 0 <= boost_time <= 103
        if ((int)boost_time == 103)
        {
            current_multiplier += 1.0f;
        }
        else if ((int)boost_time == 0)
        {
            Debug.Log(boost_time);
            Debug.Log(true);
            current_multiplier -= 1.0f;
            
            // Make sure that this ony occurs once
            boost_time -= 1f;
        }

        if (boost_time > 0)
        {
            boost_time -= Time.deltaTime;
        }

        if (number_of_strikes >= max_number_of_strikes)
        {
            execution_sprite_renderer.enabled = true;
            chat.SendMessageToChat("CCP: You failed the game, the execution team is headed you way\n");
            Application.Quit();
        }
        if (0 <= time_till_taiwan_spawns && 0.1 >= time_till_taiwan_spawns)
        {
        
            Vector3 rand_location = new Vector3();
            rand_location.x = rng.Next(-5, 5);
            rand_location.y = rng.Next(-5, 5);
            rand_location.z = 0;
            taiwan_current_position.position = rand_location;
            taiwan_button.SetActive(true);
            time_till_taiwan_spawns -= 0.1f;
        }
        time_till_taiwan_spawns -= Time.deltaTime;
        if (taiwan_pressed)
        {
            taiwan_button.SetActive(false);
            taiwan_pressed = false;
            time_till_taiwan_spawns = rng.Next(20, 60);
            
        }
        else if (time_till_taiwan_spawns < -10)
        {
            STRIKE(true);
            lose_credits = true;
             
            strike_time_start = Time.time;
            chat.SendMessageToChat($"You failed to attack Taiwan, STRIKE {number_of_strikes}");
        }
        
        
        //Update the punishments
        if (Time.time - strike_time_start >= 3)
        {
            STRIKE(false);
        }
        if (lose_credits)
        {
            social_credits -= Math.Abs((float)0.05 * social_credits * Time.deltaTime);
        }

        if (taiwan_strike)
        {
            TAIWAN_STRIKE(true);
            taiwan_strike = false;
            taiwan_strike_time_start = Time.time;
        }

        if (Time.time - taiwan_strike_time_start >= 3)
        {
            TAIWAN_STRIKE(false);
        }
    }
}
