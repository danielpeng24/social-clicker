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
    private Text _button_text;
    
    void Start()
    {
        _controller = Contoller.GetComponent<Controller>();
        _button_text = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    public void Add_labor_workers()
    {
        if (_controller.social_credits >= _controller.labor_workers_cost)
        {
            _controller.social_credits -= _controller.labor_workers_cost;
            _controller.labor_workers += 1;
            _button_text.text = $"Labor Workers: {_controller.labor_workers}";
        }
    }
}
