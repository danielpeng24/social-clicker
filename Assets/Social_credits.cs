using System;
using UnityEngine;
using UnityEngine.UI;

public class Inc_social_credits: MonoBehaviour
{
    public GameObject Contoller;
    private Controller _controller;
    private Text social_credits_text;
    
    private void Start()
    {
        social_credits_text = GetComponent<Text>();
        _controller = Contoller.GetComponent<Controller>();
    }

    private void Update()
    {
        social_credits_text.text = $"Social Credits: {(int)_controller.social_credits} SC";
    }

    //Called when by unity's button system, thus this is used
    public void Add_credits()
    {
        _controller.social_credits += _controller.base_clicks * _controller.current_multiplier;
        _controller.last_time_clicked = Time.time;
        _controller.lose_credits = false;
    }
    

}
