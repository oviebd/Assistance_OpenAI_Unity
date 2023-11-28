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

    public void SendMessageToTheThread(SendMessageRequestModel requestModel ,string threadId ,Action<bool, SendMessageResponseModel> completation)
    {

        ApiCall.instance.PostRequest<SendMessageResponseModel>(ApiConfig.instance.GetSendMessageUrl(threadId),
            ApiConfig.instance.GetHeaOpenAIHeaders(), null, requestModel.ToBody(), (result =>
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
            ApiConfig.instance.GetHeaOpenAIHeaders(), null, requestModel.ToBody(), (result =>
            {
                Debug.Log("Run Success " + result.id);
                completation(true, result);

            }), (error =>
            {
                //Debug.Log("Create Post Error - " + error);
                completation(false, null);
            }));
    }


    public void GetRunStatus( string threadId, string runId, Action<bool, CreateRunResponseModel> completation)
    {
        ApiCall.instance.GetRequest<CreateRunResponseModel>(ApiConfig.instance.GetThreadRunStatusUrl(threadId, runId),
            ApiConfig.instance.GetHeaOpenAIHeaders(), (result =>
            {
                Debug.Log("Run Status Success " + result.status);
                completation(true, result);

            }), (error =>
            {
                //Debug.Log("Create Post Error - " + error);
                completation(false, null);
            }));
    }

    public void RunSubmitTool(SubmitToolRequestModel requestModel, string threadId, Action<bool, CreateRunResponseModel> completation)
    {

        ApiCall.instance.PostRequest<CreateRunResponseModel>(ApiConfig.instance.GetThreadRunUrl(threadId),
            ApiConfig.instance.GetHeaOpenAIHeaders(), null, requestModel.ToBody(), (result =>
            {
                Debug.Log("Run Submit Tool " + result.id);
                completation(true, result);

            }), (error =>
            {
                //Debug.Log("Create Post Error - " + error);
                completation(false, null);
            }));
    }


}
