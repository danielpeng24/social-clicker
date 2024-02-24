using System;
using UnityEngine;
using UnityEngine.UI;

public class Labor_workers : MonoBehaviour
{
    public GameObject Contoller;
    private Controller _controller;

    public Text Cost_text;
    
    public Text Labor_workers_text;
    
    void Start()
    {
        _controller = Contoller.GetComponent<Controller>();
    }
    
    public void Add_labor_workers()
    {
        if (_controller.social_credits >= _controller.labor_workers_cost)
        {
            _controller.social_credits -= _controller.labor_workers_cost;
            _controller.labor_workers += 1;
            _controller.labor_workers_cost = (int)Math.Pow(1.35, _controller.labor_workers);
            
            Labor_workers_text.text = $"{_controller.labor_workers}";
            Cost_text.text = $"{(int)_controller.labor_workers_cost}";
        }
    }
}
