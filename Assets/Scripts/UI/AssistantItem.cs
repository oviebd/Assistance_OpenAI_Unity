using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class AssistantItem : MonoBehaviour
{

    public TMP_Text title;
    public TMP_Text subTitle;
    public Image image;


    private AssistantData data;

    public void Setup(AssistantData data)
    {
        this.data = data;

        title.text = data.title;
        subTitle.text = data.subTitle;
        image.sprite = data.thumbImage;

    }

    public void OnItemClicked()
    {
        Debug.Log("Clicked at " + data.subTitle);
        AppData.instance.SetAssistantData(data);
        SceneManager.LoadScene("ChatScene", LoadSceneMode.Single);
    }

   
}
