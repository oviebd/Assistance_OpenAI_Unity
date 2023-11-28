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

        ApiCall.instance.PostRequest<CreateThreadResponseModel>(ApiConfig.instance.API_CREATE_THREAD, ApiConfig.instance.GetHeaOpenAIHeaders(), null, null, (result =>
        {

           // Debug.Log("Create Post Success  " + result.id);
            completation(true, result.id);

        }), (error =>
        {
            //Debug.Log("Create Post Error - " + error);
            completation(false, "");
        }));
    }

    public void SendMessageToAThread(SendMessageRequestModel requestModel ,string threadId ,Action<bool, string> completation)
    {

        ApiCall.instance.PostRequest<SendMessageResponseModel>(ApiConfig.instance.GetSendMessageUrl(threadId),
            ApiConfig.instance.GetHeaOpenAIHeaders(), null, requestModel.ToBody(), (result =>
        {
            Debug.Log("Send Message Success " + result.id);
            completation(true, result.id);

        }), (error =>
        {
            //Debug.Log("Create Post Error - " + error);
            completation(false, "");
        }));
    }
}
