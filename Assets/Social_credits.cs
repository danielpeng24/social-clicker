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
    

    private void Start()
    {
        _controller = Contoller.GetComponent<Controller>();
    }

    public void Add_credits()
    {
        _controller.social_credits += _controller.base_clicks * _controller.current_multiplier;
        _controller.last_time_clicked = Time.time;
        _controller.lose_credits = false;
    }
    

}
