using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using System;
using Newtonsoft.Json;
using Ink.Runtime;
public class VNManager : MonoBehaviour
{
    #region Variables
    public GameObject gamePanel;
    public GameObject dialougeBox;
    public TextMeshProUGUI speakerName;
    public TextMeshProUGUI speakerContents;

    private Story story;
    private string filePath = Constants.STORY_INK_PATH;


    [Header("Json.file放在這")]
    [SerializeField]
    private TextAsset inkJsonAsset = null;

    public static VNManager Instance { get; private set; } //靜態物件
    #endregion
    #region LifeCycle
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        if (inkJsonAsset == null)
        {
            Debug.LogError("Ink JSON asset is not assigned!");
            return;
        }
    }
    void Start()
    {
        LoadStoryFromJson();
        DisplayNextLine();

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            DisplayNextLine();
        }

    }
    #endregion
    #region Intialization
    void LoadStoryFromJson()
    {
        
        TextAsset storyData = Resources.Load<TextAsset>(filePath);
        if (storyData == null)
        {
            Debug.LogError("cant find story json");
            return;
        }
        story = new Story(storyData.text);
    }
    #endregion
    #region Display
    void DisplayNextLine()
    {
        {
            // 如果 story 還沒載入或已經讀完，就什麼都不做
            if (story == null || !story.canContinue)
            {
                dialougeBox.SetActive(false);
                Debug.Log("劇情播放完畢");
                return;
            }

            // 取下一行文字
            string rawLine = story.Continue().Trim();

            // 簡單解析「名稱: 內容」
            string speaker = "";
            string content = rawLine;
            if (rawLine.Contains(Constants.COLON))
            {
                var parts = rawLine.Split(new[] { Constants.COLON }, 2, StringSplitOptions.None);
                speaker = parts[0].Trim();
                content = parts[1].Trim();
            }
            else if (story.currentTags.Count > 0)
            {
                // 如果你在 Ink 劇本裡有下 tag，也可以把第一個 tag 當作講話者
                speaker = story.currentTags[0].Trim();
                content = rawLine;
            }

            // 顯示對話框並填入文字
            dialougeBox.SetActive(true);
            speakerName.text = speaker;
            speakerContents.text = content;
        }
    }
    #endregion
    #region Audios
    #endregion
    #region Images
    #endregion
    #region Buttons
    #region Buttom
    #endregion
    #region Auto
    #endregion
    #region Skip
    #endregion
    #region Save
    #endregion
    #region Load
    #endregion
    #region History
    #endregion
    #region Settings
    #endregion
    #endregion
}
