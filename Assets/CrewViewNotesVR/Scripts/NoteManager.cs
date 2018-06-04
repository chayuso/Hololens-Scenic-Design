using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

// NoteManager manages the Note UI.

// NoteMessage is the result of json parsing from DB
// Note is the note OBJECT in game, it has audioclip, image, etc...

[Serializable]
public class NoteMessage
{
    public int id;
    public string project;
    public string title;
    public string author;
    public string dateTime;

    public string px;
    public string py;
    public string pz;
    public string qx;
    public string qy;
    public string qz;
    public string qw;
    public string scale;

    public string content; // text,imageURL,audio content

}

[Serializable]
public class Inbox // contains all the notes from DB, by project names
{
    public NoteMessage[] notes;
}

public partial class NoteManager : MonoBehaviour
{

    public bool isDebugLog = true;

    // Notes to show in the noteList
    public List<Note> noteList;

    // Save Review
    Transform playerPosTrans;
    Transform playerRotTrans;
    public string playerPosTag;
    public string playerRotTag;
    
    public Vector3 pl;
    public Quaternion ql;
    public float sl;

    // string is the project name, List<string> is all the titles of the notes
    Dictionary<string, List<string>> projectNoteTitles;
    
    // string is the note title, list<Note> is the note objects (replies) under the title
    Dictionary<string, List<Note>> messageTable; 

    public Note notePrefab;
    public UINoteSelectionSlot noteSelectionSlotPrefab;
    public UIMessageSlot messageSlotPrefab;
    public Note curNote;
    public Note curMessage;
    
    public User user;
    public string noteTitle;
    public string noteContent;

    // Message Title
    public string messageTitle;

    public int totalUnreadNum;
    
    // UIs
   
    public Button noteButton;

    public Button addButton;
    public Button closeButton;
    public Button backButton;

    public Button submitButton;
    public Button cancelButton;
    //public Button voiceButton;
    //public Button drawButton;
    //public Button recordButton;

    // Panels
    public GameObject inputPanel;
    public GameObject notePanel;
    public GameObject noteListPanel;
    //public GameObject recordingPanel;
    public GameObject messagePanel;
    public GameObject noteEditPanel;

    // InputFields
    public InputField contentInput;
    public InputField titleInput;
    public InputField textInput;

    // Content to layout
    public Transform noteListContent;
    public Transform messageContent;

    // MessagePanel
    public Text messagePanelTitle;

    // Audio
    //float startTime;
    //AudioSource aud;
    //AudioClip curAudioClip;
    //string mic;

    Manager manager;

    // Use this for initialization
    void Start () {

        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();

        messageTable = new Dictionary<string, List<Note>>();
        projectNoteTitles = new Dictionary<string, List<string>>();

        noteTitle = "";
        noteContent = "";

        notePanel.SetActive(true);
        noteListPanel.SetActive(true);

        inputPanel.SetActive(false);
        messagePanel.SetActive(false);
        noteEditPanel.SetActive(false);
        notePanel.SetActive(false);

        if(manager.curPlatform != Manager.Platform.VR)
            noteButton.onClick.AddListener(ViewNotePanel);

        addButton.onClick.AddListener(CreateNote);        
        submitButton.onClick.AddListener(SubmitNote);
        // cancelButton.onClick.AddListener(CancelNote);
        backButton.onClick.AddListener(BackButton);
        closeButton.onClick.AddListener(ViewNotePanel);

        //Application.RequestUserAuthorization(UserAuthorization.Microphone);
        //if (Microphone.devices.Length == 0)
        //{
        //    Debug.LogWarning("No microphone found to record audio clip sample with.");
        //    return;
        //}
        //mic = Microphone.devices[0];

    }

    private void BackButton()
    {
        if (messagePanel.activeSelf)
        {
            messagePanel.SetActive(false);
            noteListPanel.SetActive(true);
        }

        else if (noteEditPanel.activeSelf)
        {
            noteEditPanel.SetActive(false);
            noteListPanel.SetActive(true);
        }

        else if (noteListPanel.activeSelf)
        {
            ViewNotePanel();
        }

    }       
           
    public void ViewNotePanel()
    {        
        notePanel.SetActive(!notePanel.activeSelf);
    }

    public void CancelNote()
    {
        inputPanel.SetActive(false);        
        notePanel.SetActive(true);
    }
    public void CreateNote()
    {
        //instantiate a note prefab
        Instantiate(notePrefab);
        curNote = notePrefab.GetComponent<Note>();
        
        contentInput.text = "";
        noteEditPanel.SetActive(true);
        noteListPanel.SetActive(false);

    }
        
    //void StartRecord()
    //{
    //    recordingPanel.SetActive(true);
        
    //    startTime = Time.time;
    //    curAudioClip = Microphone.Start(mic, false, 44100);
    //}
    //void EndRecord()
    //{
    //    if (!isRecordStarted) return;
    //    recordingPanelMask.SetActive(false);
    //    Microphone.End(mic);
    //    isRecordStarted = false;
    //    aud.Play();
    //    afterRecordingShowUps.SetActive(true);

    //}

    //public void SubmitRecord()
    //{
    //    // save and submit record
    //    SavWav.Save(saveFileName, aud.clip);
    //    GameManager.Instance.selectedClips = aud.clip;

    //}

    //public void PlayRecord()
    //{
    //    // play the record again
    //    aud.Play();
    //}
    
    public void SubmitNote()
    {
        // submit a new note to a note list
        if (noteEditPanel.activeSelf)
        {            
            curNote = new Note();

            curNote.project = manager.curProject;
            curNote.title = titleInput.text;
            curNote.author = user.userName;
            
            curNote.dateTime = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

            Debug.Log(ql);

            // save view
            SaveView();

            Debug.Log(ql);

            curNote.p = pl;            
            curNote.q = ql;
            curNote.scale = sl;

            curNote.content = contentInput.text;

            titleInput.text = "";
            contentInput.text = "";

            //submit
            noteList.Add(curNote);

            // create message list and set the first message to be the new note
            // s.t. noteList[i].title = messageList[i][1].title
            List<Note> messageList = new List<Note>();
            messageList.Add(curNote);
            messageTable.Add(curNote.title, messageList);

            // clear input field
            textInput.text = "";

            // back to note selection
            notePanel.SetActive(true);
            noteListPanel.SetActive(true);
            noteEditPanel.SetActive(false);                      

            if (manager.isDB)            
                StartCoroutine(manager.SendNote(curNote));

            Debug.Log(ql);
            Debug.Log(curNote.q);
                      
        }

        else if (messagePanel.activeSelf)
        {
            if (curMessage == null)
            {
                curMessage = new Note();
            }
            curMessage.content = textInput.text;  // textInput on the bottom
            curMessage.title = messageTitle;
            curMessage.dateTime = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            curMessage.author = user.userName;

            //submit to the messageTitle
            var ml = messageTable[messageTitle];
            ml.Add(curMessage);
            messageTable[messageTitle] = ml;

            if (manager.isDB)
            {                
                StartCoroutine(manager.SendNote(curMessage));                
            }                

        }

    }
        
    // set NoteList and messageTable
    public void SetProjectNotes(string project, Inbox inbox)
    {
        projectNoteTitles.Clear();
        noteList.Clear();
        messageTable.Clear();

        // first create projectTitles, which has all the titles of the Notes in the project      
        List<string> titles = new List<string>();        
        foreach (NoteMessage nm in inbox.notes)
        {
            Debug.Log("this nm is : " + nm.project + " " + nm.title + " " + nm.content);

            Debug.Log("curProject: " + project);
            if (nm.project == project)
            {
                Debug.Log("set: " + nm.project + " " + nm.title + " " + nm.content);

                Note n = new Note();
                n.project = nm.project;
                n.title = nm.title;
                
                n.author = nm.author;
                n.dateTime = nm.dateTime;

                n.p = new Vector3(float.Parse(nm.px), float.Parse(nm.py), float.Parse(nm.pz));
                n.q = new Quaternion(float.Parse(nm.qx), float.Parse(nm.qy), float.Parse(nm.qz), float.Parse(nm.qw));
                
                n.scale = float.Parse(nm.scale);
                             
                n.content = nm.content;

                // later image/voice url

                // if the nm.title is a new title, nm will be the first note
                if (titles.Contains(nm.title) == false)
                {
                    titles.Add(nm.title);                                    
                    noteList.Add(n);                    
                    messageTable.Add(n.title, new List<Note>());
                }
                
                messageTable[nm.title].Add(n);
                  
            }            
        }

        Debug.Log("noteList: " + noteList.Count);
        Debug.Log("messageTable: " + messageTable.Count);

    }

    public void SaveView()
    {
        Transform tr = manager.curScene.transform;
        playerPosTrans = GameObject.FindGameObjectWithTag(playerPosTag).GetComponent<Transform>();
        playerRotTrans = GameObject.FindGameObjectWithTag(playerRotTag).GetComponent<Transform>();

        // model's position in the playerCamera's reference frame
        pl = playerPosTrans.InverseTransformPoint(tr.position);

        // model's rotation in the playerCamera's reference frame
        Debug.Log("tr.rotation in save view " + tr.rotation);

        ql = Quaternion.Inverse(playerRotTrans.rotation) * tr.rotation;

        Debug.Log("ql in save view " + ql);

        // save model's scale
        sl = tr.localScale.x;
    }

    public void ReProduceView()
    {        
        // get ql, rl, sl in the note by title from noteList
        Note nl = messageTable[messageTitle][0];

        Debug.Log("reproduce view");
        Debug.Log(nl.p);
        Debug.Log(nl.q);

        Transform tr = manager.curScene.transform;
        playerPosTrans = GameObject.FindGameObjectWithTag(playerPosTag).GetComponent<Transform>();
        playerRotTrans = GameObject.FindGameObjectWithTag(playerRotTag).GetComponent<Transform>();
        tr.position = playerPosTrans.TransformPoint(nl.p);

        // cube.transform.rotation = r_c * playerRotTrans.rotation;
        Quaternion q = nl.q * playerRotTrans.rotation;
        Vector3 rot = (Quaternion.Inverse(tr.rotation) * q).eulerAngles;

        tr.Rotate(Vector3.up * rot.y); // only show y rotation
        tr.localScale = Vector3.one * nl.scale; 

        
    }


    // Update is called once per frame
    void Update()
    {        
        // only update if notePanel is visible
        if (!notePanel.activeSelf) return;               
       
        if (noteEditPanel.activeSelf)
        {
            // users will click Title and content to start typing
            inputPanel.SetActive(true);
            textInput.gameObject.SetActive(false);
        }

        else if (noteListPanel.activeSelf)
        {
            inputPanel.SetActive(false);

            // instantiate/destroy enough slots
            Utilities.BalancePrefabs(noteSelectionSlotPrefab.gameObject, noteList.Count, noteListContent);

            // refresh the content in the slots        
            for (int i = 0; i < noteList.Count; ++i)
            {
                int cc = noteListContent.childCount;
                var slot = noteListContent.GetChild(cc - i - 1).GetComponent<UINoteSelectionSlot>();
                var ni = noteList[i];
                
                slot.title.text = ni.title;
                slot.author.text = user.userName;
                slot.content.text = ni.content;
                slot.time.text = ni.dateTime;

                // ...
            }
        }
        else if (messagePanel.activeSelf)
        {
            if(!inputPanel.activeSelf)
                inputPanel.SetActive(true);
            if(!textInput.gameObject.activeSelf)
                textInput.gameObject.SetActive(true);

            // refresh message page
            // messagetitle is set in UINoteSelction when it is clicked                    

            messagePanelTitle.text = messageTitle;

            // instantiate/destroy enough slots
            var ml = messageTable[messageTitle];
            Utilities.BalancePrefabs(messageSlotPrefab.gameObject, ml.Count, messageContent);

            // load contents on messageSlots

            for (int i = 0; i < ml.Count; ++i)
            {
                var slot = messageContent.GetChild(i).GetComponent<UIMessageSlot>();
                var ni = ml[i]; // List<Note> messageList
                               

                slot.author.text = ni.author;
                slot.content.text = ni.content;
                slot.time.text = ni.dateTime;         
                
                // ...
            }

        }



    }
}
