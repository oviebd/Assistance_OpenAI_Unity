using System.Collections.Generic;
using UnityEngine;

namespace REST_API_HANDLER
{

	public class ApiConfig : MonoBehaviour
	{
		public static ApiConfig instance;


		private string openAiToken = "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";
		//Dummy Url site - https://dummy.restapiexample.com/
		private string API_BASE_URL = "http://dummy.restapiexample.com/api/v1/";

		private string OPEN_API_BASE_URL = "https://api.openai.com/v1/";
		

		[HideInInspector]
		public string API_GET_EMPLOYEES;
		[HideInInspector]
		public string API_CREATE_POST;
		[HideInInspector]
		public string API_DELETE_POST;
		[HideInInspector]
		public string API_UPLOAD_VIDEO;
		[HideInInspector]
		public string API_UPDATE_VIDEO;


		[HideInInspector]
		public string API_CREATE_THREAD;

		void Awake()
		{
			if (instance == null)
				instance = this;

			API_GET_EMPLOYEES = "https://jsonplaceholder.typicode.com/posts";//API_BASE_URL + "employees";
			API_CREATE_POST = "https://jsonplaceholder.typicode.com/posts";
			API_DELETE_POST = "https://jsonplaceholder.typicode.com/posts";

			API_CREATE_THREAD = OPEN_API_BASE_URL + "threads";
		}

		public string GetSendMessageUrl(string threadId)
        {
			return OPEN_API_BASE_URL + "threads/" + threadId + "/messages";

		}

		public string GetThreadRunUrl(string threadId)
		{
			return OPEN_API_BASE_URL + "threads/" + threadId + "/runs";
		}

		public string GetThreadRunStatusUrl(string threadId, string runId)
		{
			return OPEN_API_BASE_URL+ "threads/" + threadId + "/runs/" + runId;
		}

		public string GetSubmitToollUrl(string threadId, string runId)
		{
			return OPEN_API_BASE_URL + "threads/" + threadId + "/runs/" + runId + "/submit_tool_outputs";
		}

		public string GetThreadMessages(string threadId)
		{
			return OPEN_API_BASE_URL + "threads/" + threadId + "/messages";
		}


		public static Dictionary<string, string> GetHeaders()
		{
			Dictionary<string, string> headers = new Dictionary<string, string>();
			//headers.Add("Content-type", "application/json; charset=UTF-8");
			headers.Add("Content-type", "application/json");
			headers.Add("OpenAI-Beta", "assistants=v1");
			return headers;
		}

		public Dictionary<string, string> GetHeaOpenAIHeaders()
		{
			Dictionary<string, string> headers = new Dictionary<string, string>();
			//headers.Add("Content-type", "application/json; charset=UTF-8");
			headers.Add("Content-type", "application/json");
			headers.Add("OpenAI-Beta", "assistants=v1");
			headers.Add("Authorization", "Bearer " + openAiToken);
			return headers;
		}

	}

}


