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


    [Header("Json.file��b�o")]
    [SerializeField]
    private TextAsset inkJsonAsset = null;

    public static VNManager Instance { get; private set; } //�R�A����
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
            // �p�G story �٨S���J�Τw�gŪ���A�N���򳣤���
            if (story == null || !story.canContinue)
            {
                dialougeBox.SetActive(false);
                Debug.Log("�@�����񧹲�");
                return;
            }

            // ���U�@���r
            string rawLine = story.Continue().Trim();

            // ²��ѪR�u�W��: ���e�v
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
                // �p�G�A�b Ink �@���̦��U tag�A�]�i�H��Ĥ@�� tag ��@���ܪ�
                speaker = story.currentTags[0].Trim();
                content = rawLine;
            }

            // ��ܹ�ܮبö�J��r
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
