using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inc_social_credits: MonoBehaviour
{
    
    // Literally do not care about the naming scheme,
    // Too bad, python's naming has taken over, I'm not Capitalizing the second word, I'm using _

    public GameObject Contoller;
    private Controller _controller;
    private Text social_credits_text;
    

    private void Start()
    {
        social_credits_text = GetComponent<Text>();
        _controller = Contoller.GetComponent<Controller>();
    }

    public void Add_credits()
    {
        _controller.social_credits += _controller.base_clicks * _controller.current_multiplier;
        social_credits_text.text = $"Social Credits: {(int)_controller.social_credits} SC";
        
        _controller.last_time_clicked = Time.time;
        _controller.lose_credits = false;
    }
    

}
