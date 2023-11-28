using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using REST_API_HANDLER;
using System;

public class ChatPanel : MonoBehaviour
{
   
    private OpenAIApiService apiService = new OpenAIApiService();

    private string threadId = "";

    public void OnChatMessageOptionButtonPressed(int option)
    {
        CreateThread();
    }

    private void CreateThread()
    {
        apiService.CreateThread((isSuccess, id) =>
        {
            threadId = id;
            print("IsSuccess " + isSuccess + "Id " + id);
        });
    }

    private void SendMessage()
    {
        apiService.CreateThread((isSuccess, id) =>
        {
            threadId = id;
            print("IsSuccess " + isSuccess + "Id " + id);
        });
    }
}


