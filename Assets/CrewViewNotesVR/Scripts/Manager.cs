using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// using DigitalRubyShared; // gesture
public class Manager : MonoBehaviour
{
    public bool isOffline = true;
    public bool isDB = false;
    public bool isLogin = false;
        
    public string serverAddress = "http://localhost/CrewView/";

	public string urlGetNotes = "action_get_notes.php";

	public string urlSendNote = "action_set_note.php";
	// public string urlGetMailSent = "action_get_mail_sent.php";

    // authentication manager
    public string urlLogin = "action_login.php";
    public string urlRegister = "action_register.php";

    public string curProject = "fernando theatre";

    public Inbox inbox;

    public GameObject canvas;

    public GameObject[] models;

    public GameObject loginPage;
    public GameObject projectPage;
    public GameObject notePage;

    // notes 
    public NoteManager noteManager;
    public GameObject curScene;       

    public User user;

    // model intial position and rotation
    public Vector3[] initialPos;
    public Vector3[] initialRot;

    public Button BackButton;

    //public InteractiveToggleButton NoteButtonVR;

    public enum State {initialize, login, selectProject, inProject, note};
    public enum Platform {VR, AndroidAR, Lite};

    public State curState;
    public Platform curPlatform;

    private void Awake()
    {        
        curState = State.initialize;                
    }

    private void Start()
    {
        initialPos = new Vector3[4];
        initialRot = new Vector3[4];

        initialPos[0] = new Vector3(1.43f, 2.23f, -0.46f);
        initialRot[0] = new Vector3(0, -90, 0);

        initialPos[1] = new Vector3(4.69f, 2.06f, 5.09f);
        initialRot[1] = new Vector3(0, -90, 0);

        initialPos[2] = new Vector3(0, 1.4f, 1.5f);
        initialRot[2] = new Vector3(0, 180, 0);

        initialPos[3] = new Vector3(0, 1.6f, 1.5f);
        initialRot[3] = new Vector3(0, 180, 0);

        BackButton.onClick.AddListener(Back);
        //NoteButtonVR.OnDownEvent.AddListener();
        
    }

    public void ShowCanvas()
    {
        canvas.SetActive(true);
    }
    public void HideCanvas()
    {
        canvas.SetActive(false);
    }

    private void Update()
    {
        switch (curState)
        {
            case State.initialize:
                if(Camera.main != null)
                {                    
                    canvas.transform.parent = Camera.main.transform;
                    canvas.transform.localPosition = new Vector3(0, 0, 2.5f);
                    canvas.transform.localRotation = Quaternion.identity;
                    curState = State.inProject;
                }
                                
                break;

            case State.login:                
                loginPage.SetActive(true);
                projectPage.SetActive(false);
                notePage.SetActive(false);
                break;

            case State.selectProject:
                loginPage.SetActive(false);
                projectPage.SetActive(true);
                notePage.SetActive(false);


                break;

            case State.inProject:
                loginPage.SetActive(false);
                projectPage.SetActive(false);
                notePage.SetActive(false);

                break;

            case State.note:
                loginPage.SetActive(false);
                projectPage.SetActive(false);
                notePage.SetActive(true);

                break;
        }

    }
    
   
    public void Back()
    {
        if(curPlatform == Platform.VR)
        {
            HideCanvas();
        }

        else
        {
            switch (curState)
            {
                case State.login:
                    // Application.Quit();
                    break;

                case State.selectProject:

                    curState = State.login;

                    break;

                case State.inProject:
                    curState = State.selectProject;
                    CloseProject();

                    break;
            }
        }
        
    }

    public void OpenProject(string s)
    {
        // show the 3d model at the camera's position

        // load scales from DB;               

        int n;
        if (s == "ProjectSlot0") n = 0;
        else if (s == "ProjectSlot1") n = 1;
        else if (s == "ProjectSlot2") n = 2;
        else if (s == "ProjectSlot3") n = 3;
        else return;

        // set initial positions and rotations
        // initialPos = new Vector3(0,0,0);

        switch (curPlatform)
        {
            case Platform.Lite:
                Camera.main.transform.position = initialPos[n];
                Camera.main.transform.rotation = Quaternion.Euler(initialRot[n]);
                curScene = Instantiate(models[n], Vector3.zero, Quaternion.identity);

                break;

            case Platform.VR:
                                
                curScene = GameObject.FindGameObjectWithTag("Environment");
                curScene.SetActive(true);

                break;
        }
        



        curState = State.inProject;

    }
    
    public void CloseProject()
    {
        Destroy(curScene);
        
    }
    // get all the notes
    public IEnumerator RequestNotes()
    {
        Debug.Log("starting ...");

        WWWForm form = new WWWForm();
        form.AddField("userId", user.id);

        WWW w = new WWW(serverAddress + urlGetNotes, form);

        Debug.Log("waiting for w");

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            Debug.Log(w.text);
            // parsing the JSON string
            inbox = JsonUtility.FromJson<Inbox>(w.text);
            Debug.Log(inbox.notes.Length);

            //PanelInbox.SetActive (true);
            // PanelInbox.GetComponent<InboxManager>().DisplayInboxMessages(inbox);
        }
        else
        {
            Debug.Log("error retrieving notes: " + w.error);
        }

        noteManager.SetProjectNotes(curProject, inbox);

        yield return null;
                
    }

    public IEnumerator SendNote(Note n)
    {
        Debug.Log("entered send Note");
        WWWForm form = new WWWForm();
        // `id` project title author dateTime px py pz qx qy qw scale text image voice

        form.AddField("project", n.project);
        form.AddField("title", n.title);
        form.AddField("author", n.author);
        form.AddField("dateTime", n.dateTime);
        form.AddField("px", n.p.x.ToString());
        form.AddField("py", n.p.y.ToString());
        form.AddField("pz", n.p.z.ToString());
        form.AddField("qx", n.q.x.ToString());
        form.AddField("qy", n.q.y.ToString());
        form.AddField("qz", n.q.z.ToString());
        form.AddField("qw", n.q.w.ToString());
        form.AddField("scale", n.scale.ToString());
        form.AddField("content", n.content);

        Debug.Log("project: " + n.project);

        Debug.Log("title: " + n.title);
        Debug.Log("author: " + n.author);
        Debug.Log("px: " + n.p.x.ToString());
                
        Debug.Log("scale: " + n.scale.ToString());
        Debug.Log("content: " + n.content);

        Debug.Log("triggered send note to data server");
        WWW w = new WWW(serverAddress + urlSendNote, form);
        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (w.text.ToLower().Contains("success"))
            {
                Debug.Log("note successfully stored");
            }
            else
            {
                Debug.Log("could not sent note message.");
                
            }
        }
    }

}
