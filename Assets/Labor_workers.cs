using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Labor_workers : MonoBehaviour
{
    public GameObject Contoller;
    private Controller _controller;

    // public GameObject Button_text;
    private Text _labor_workers_text;
    private Text _cost_text;
    
    void Start()
    {
        _controller = Contoller.GetComponent<Controller>();
        _cost_text = transform.Find("Cost text").GetComponent<Text>();
        _labor_workers_text = transform.Find("Workers text").GetComponent<Text>();
    }

    // Update is called once per frame
    public void Add_labor_workers()
    {
        if (_controller.social_credits >= _controller.labor_workers_cost)
        {
            _controller.social_credits -= _controller.labor_workers_cost;
            _controller.labor_workers += 1;
            _controller.labor_workers_cost = (float)Math.Pow(1.2, _controller.labor_workers);
            _labor_workers_text.text = $"Labor Workers: {_controller.labor_workers}";
            _cost_text.text = $"Cost: {(int)_controller.labor_workers_cost} SC";
        }
    }
}
