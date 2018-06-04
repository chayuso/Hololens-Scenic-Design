using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

[Serializable]
public class UserJson
{
    public bool success;
    public string error;
    public string email;
    public int id;
}


public class AuthenticationManager : MonoBehaviour {


	public GameObject mainMenu;
	public GameObject buttonLogin;
	public GameObject buttonSwapRegistration;
	public GameObject buttonRegister;

	public GameObject fieldEmailAddress;
	public GameObject fieldPassword;
	public GameObject fieldReenterPassword;

	public InputField inputEmail;
	public InputField inputPassword;
	public InputField inputReenterPassword;

	public Text textSwapButton;
	public Text textFeedback;

	WWWForm form;

	public bool showRegistration = false;

    public Manager manager;

    // Use this for initialization
    void Start () {
		textFeedback.text = "";
        
        displayLoginPanel ();

        buttonRegister.GetComponent<Button>().onClick.AddListener(RegisterButtonTapped);
        buttonSwapRegistration.GetComponent<Button>().onClick.AddListener(SwapSignupSignin);
        buttonLogin.GetComponent<Button>().onClick.AddListener(LoginButtonTapped);

        if (manager.isLogin == false)
        {
            inputEmail.text = "kjing1@uci.edu";
            inputPassword.text = "password";
        }



    }
	
	// Update is called once per frame
	void Update () {
	
	}

	public void displayLoginPanel() {
		buttonRegister.SetActive (false);
		fieldReenterPassword.SetActive (false);
	}


	public void SwapSignupSignin() {
		if (showRegistration) {
			showRegistration = false;
            buttonLogin.SetActive (true);
			buttonRegister.SetActive (false);
			fieldReenterPassword.SetActive (false);
			textSwapButton.text = "SignUp";


		} else {
			showRegistration = true;
            buttonLogin.SetActive (false);
			buttonRegister.SetActive (true);
			fieldReenterPassword.SetActive (true);
			textSwapButton.text = "SignIn";

		}
	}

	public void RegisterButtonTapped() {
		// todo
		textFeedback.text = "Processing registration...";
		StartCoroutine ("RequestUserRegistration");
	}

	public void LoginButtonTapped() {
		textFeedback.text = "Logging in...";
		StartCoroutine ("RequestLogin");


	}

	public IEnumerator RequestLogin() {
		
		string email = inputEmail.text;
		string password = inputPassword.text;

		form = new WWWForm ();
		form.AddField ("email", email);
		form.AddField ("password", password);

		WWW w = new WWW (manager.serverAddress + manager.urlLogin, form);
		yield return w;

		if (string.IsNullOrEmpty (w.error)) {
            UserJson userJson = JsonUtility.FromJson<UserJson> (w.text);
			if (userJson.success == true) 
			{
				if (userJson.error != "") 
				{
					textFeedback.text = userJson.error;
				} else 
				{
					textFeedback.text = "login successful.";
                                       

                    ProcessPlay(userJson);

                    manager.curState = Manager.State.selectProject;                                 


                }
			} else {
				textFeedback.text = "An error occured";
			}

			// todo: launch the game (player)
		} else {
			// error
			textFeedback.text = "An error occured.";
		}

        yield return null;
    }

	public IEnumerator RequestUserRegistration(){
		string email = inputEmail.text;
		string password = inputPassword.text;
		string reenterPassword = inputReenterPassword.text;

		if (password.Length < 8) {
			textFeedback.text = "password needs to be at least 8 characters long.";
			yield break;
		}
		if(password != reenterPassword) {
			textFeedback.text = "password do not match";
			yield break;
		}

		form = new WWWForm();
		form.AddField("email", email);
		form.AddField("password", password);

		WWW w = new WWW(manager.serverAddress + manager.urlRegister, form);
		yield return w;

		if (string.IsNullOrEmpty (w.error)) {
			UserJson userJson = JsonUtility.FromJson<UserJson> (w.text);
			if (userJson.success == true) {
				if (userJson.error != "") {
					textFeedback.text = userJson.error;
				} else {
					textFeedback.text = "registration successful.";
					//ProcessPlay (user);
				}
			} else {
				textFeedback.text = "An error occured";
			}
		}
		
	}


    public void ProcessPlay(UserJson uj)
    {
        manager.user.id = uj.id;
        manager.user.email = uj.email;
        manager.user.userName = "Ke";

        if(manager.isDB)
            StartCoroutine(manager.RequestNotes());
        //GetComponent<NetworkManagerMainMenu>().JoinGame();
    }


    
}
