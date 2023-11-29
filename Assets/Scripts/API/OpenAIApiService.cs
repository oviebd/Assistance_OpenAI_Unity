using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using REST_API_HANDLER;
using System;

public class OpenAIApiService 
{
    private ApiConfig apiConfig = ApiConfig.instance;
    private ApiCall apiCall = ApiCall.instance;

    public void CreateThread(Action<bool,string> completation)
    {

        ApiCall.instance.PostRequest<CreateThreadResponseModel>(ApiConfig.instance.API_CREATE_THREAD, ApiConfig.instance.GetOpenAIHeaders(), null, null, (result =>
        {

           // Debug.Log("Create Post Success  " + result.id);
            completation(true, result.id);

        }), (error =>
        {
            //Debug.Log("Create Post Error - " + error);
            completation(false, "");
        }));
    }

    public void SendMessageToTheThread(SendMessageRequestModel requestModel ,string threadId ,Action<bool, SendMessageResponseModel> completation)
    {

        ApiCall.instance.PostRequest<SendMessageResponseModel>(ApiConfig.instance.GetSendMessageUrl(threadId),
            ApiConfig.instance.GetOpenAIHeaders(), null, requestModel.ToBody(), (result =>
        {
            Debug.Log("Send Message Success " + result.id);
            completation(true, result);

        }), (error =>
        {
            //Debug.Log("Create Post Error - " + error);
            completation(false, null);
        }));
    }

    public void RunThread(CreateRunRequestModel requestModel, string threadId, Action<bool, CreateRunResponseModel> completation)
    {

        ApiCall.instance.PostRequest<CreateRunResponseModel>(ApiConfig.instance.GetThreadRunUrl(threadId),
            ApiConfig.instance.GetOpenAIHeaders(), null, requestModel.ToBody(), (result =>
            {
                Debug.Log("Run Success " + result.id);
                completation(true, result);

            }), (error =>
            {
                //Debug.Log("Create Post Error - " + error);
                completation(false, null);
            }));
    }


    public void GetRunStatus( string threadId, string runId, Action<bool, GetStatusResponseModel> completation)
    {
        ApiCall.instance.GetRequest<GetStatusResponseModel>(ApiConfig.instance.GetThreadRunStatusUrl(threadId, runId),
            ApiConfig.instance.GetOpenAIHeaders(), (result =>
            {
                Debug.Log("Run Status Success " + result.status);
                completation(true, result);

            }), (error =>
            {
                //Debug.Log("Create Post Error - " + error);
                completation(false, null);
            }));
    }

    public void RunSubmitTool(SubmitToolRequestModel requestModel, string threadId,string runId, Action<bool, GetStatusResponseModel> completation)
    {

        ApiCall.instance.PostRequest<GetStatusResponseModel>(ApiConfig.instance.GetSubmitToollUrl(threadId, runId),
            ApiConfig.instance.GetOpenAIHeaders(), null, requestModel.ToBody(), (result =>
            {
                Debug.Log("Run Submit Tool " + result.id);
                completation(true, result);

            }), (error =>
            {
                //Debug.Log("Create Post Error - " + error);
                completation(false, null);
            }));
    }

    public void GetMessages(string threadId, Action<bool, List<Message>> completation)
    {
        ApiCall.instance.GetRequest<ThreadMessagesResponseModel>(ApiConfig.instance.GetThreadMessages(threadId),
            ApiConfig.instance.GetOpenAIHeaders(), (result =>
            {
                Debug.Log("Run get Message Success " + result.first_id);

                List<Message> messages = new List<Message>();

                for (int i = 0; i < result.data.Count; i++)
                {
                  
                    ThreadMessagesModel messageModel = result.data[i];
                    List<MessageContent> messageContents = messageModel.content;
                    for (int j = 0; j < messageContents.Count; j++)
                    {
                        Message message = new Message(0, messageModel.role, messageContents[j].text.value);
                        messages.Add(message);
                    }
                }
                Debug.Log(" Total messages " + messages.Count);
                completation(true, messages);

            }), (error =>
            {
                //Debug.Log("Create Post Error - " + error);
                completation(false, null);
            }));
    }


}
