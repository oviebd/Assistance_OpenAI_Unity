using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using REST_API_HANDLER;
using System;

public class ChatPanel : MonoBehaviour
{
   
    private OpenAIApiService apiService = new OpenAIApiService();
    private string threadId = "";
    private string message = "";

    public string assistantId = "asst_sgjlg3pltKFAzIu5t98oQMF6";
    private string instructions = "When the user inputs \"1\", call function \"function1\". When the call was a success, answer with \"1 was received\". When the user inputs anything else, just answer with \"ok\"";

    private string runId = "";



    public void OnChatMessageOptionButtonPressed(int option)
    {
        message = option.ToString();
        CreateThread();

    }

    private void CreateThread()
    {

        if (threadId != null &&  threadId != "")
        {
            SendMessage();
            return;
        }

        apiService.CreateThread((isSuccess, id) =>
        {
            threadId = id;
            print("U>> 01 IsSuccess " + isSuccess + "Id " + id);
            SendMessage();
        });
    }

    private void SendMessage()
    {
        SendMessageRequestModel requestModel = new SendMessageRequestModel("user", message);
        apiService.SendMessageToTheThread(requestModel,threadId,(isSuccess, model) =>
        {
            print("U>> 02  IsSuccess " + isSuccess + "Send message  id " + model.id);
            if (isSuccess)
            {
                RunThread();
            }
        });
    }

    private void RunThread()
    {
        CreateRunRequestModel requestModel = new CreateRunRequestModel(assistantId, instructions);
        apiService.RunThread(requestModel, threadId, (isSuccess, model) =>
        {
            runId = model.id;
            print("U>> 03 IsSuccess in run " + isSuccess + "Run Id " + runId);
            GetStatus(runId);
        });
    }


    private void GetStatus(string runId)
    {
        apiService.GetRunStatus(threadId, runId,(isSuccess, model) =>
        {
            print("U>> 04 IsSuccess status get " + isSuccess + " status " + model.status);
        });
    }
}


