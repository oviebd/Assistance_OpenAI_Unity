using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppData : MonoBehaviour
{
    [HideInInspector] public static AppData instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private AssistantData assistanteData;

    public AssistantData GetAssistantData()
    {
        return assistanteData;
    }

    public void SetAssistantData(AssistantData assistanteData)
    {
        this.assistanteData = assistanteData;
    }

    public string GetAssistantId()
    {
        if (assistanteData != null)
        {
            return assistanteData.assistantId;
        }
        return null;
        
    }

    public string GetAsiistantInstruction()
    {
        if (assistanteData != null)
        {
            string name = assistanteData.subTitle;
            string instructions = "When the user inputs \"1\", call function \"function1\". When the call was a success, answer with \""
                + name
                + " 1 was received\". When the user inputs anything else, just answer with \"" + name + " ok\"";
            return instructions;
        }
    
        return "";
    }
}
