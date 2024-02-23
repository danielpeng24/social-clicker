using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Chatbox : MonoBehaviour
{
    public int maxmessages = 10;
    public bool selected_chatbox = false;
    public GameObject chatPanel, textObject;
    public InputField chatboxInput;

    public GameObject Controller;
    private Controller _controller;
    

    List<string> key_words_list = new List<string>();


    [SerializeField] List<Message> messagelist = new List<Message>();

    void Start()
    {
        // key_words_list.Add("mao")
        _controller = Controller.GetComponent<Controller>();

    }

    // Update is called once per frame
    void Update()
    {
        if ((EventSystem.current.currentSelectedGameObject == chatboxInput.gameObject ||
             Input.GetKeyDown(KeyCode.Space)) && Input.GetMouseButtonDown(0))
        {
            selected_chatbox = !selected_chatbox;
            if (selected_chatbox)
            {
                chatboxInput.ActivateInputField();
            }
            else
            {
                chatboxInput.DeactivateInputField();
            }
        }

        if (chatboxInput.text != "")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SendMessageToChat(chatboxInput.text, true);
                chatboxInput.text = "";
                chatboxInput.ActivateInputField();
            }

        }
    }

    public void SendMessageToChat(string text, bool is_player = false)
    {

        // Comment to Daniel Peng use is_player to check whether or not you should check for key words
        // Checker code

        if (messagelist.Count >= maxmessages)
        {
            Destroy(messagelist[0].textObject.gameObject);
            messagelist.RemoveAt(0);
        }

        if (is_player)
        {
            if (text == "glory to the ccp")
            {
                _controller.boost_time = 103;
                text = "GLORY TO THE CCP 103 seconds boost";
            }
            // DANIEL PENG THIS IS NOT HOW YOU CODE, THIS DOES NOT WORK
            // USE _controller.social_credits
            
            // else if (text == "taiwan good")
            // {
            //     sc -= 9999
            // }
        }
        
        
        


        Message newMessage = new Message();

        newMessage.text = text;

        GameObject newText = Instantiate(textObject, chatPanel.transform);

        newMessage.textObject = newText.GetComponent<Text>();
        newMessage.textObject.text = text;


        messagelist.Add(newMessage);
    }
}

[System.Serializable]
public class Message
{
    public string text;
    public Text textObject;
}