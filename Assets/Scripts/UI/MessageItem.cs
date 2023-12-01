using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageItem : MonoBehaviour
{
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private Image bg;

    private Message message;


    public void Setup(Message message)
    {
        this.message = message;

        messageText.text = message.message;
        if (message.role == "user")
        {
            bg.color = Color.green;
        }
        else
        {
            bg.color = Color.red;
        }
    }
}
