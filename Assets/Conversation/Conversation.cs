using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Conversation : MonoBehaviour {
	[SerializeField]
	GameObject panel;
    [SerializeField]
    Image portrait;
	[SerializeField]
	AudioClip[] clips;
	AudioSource audioSource;
	bool printText = false;
	string text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi in enim nibh. Quisque sed augue ac dolor molestie mattis ut vitae lectus. Suspendisse potenti. Nulla fringilla";
    [SerializeField]
    Line[] lines;
    int lineCount = 0;
	[SerializeField]
	bool PrintWordByWord = false;
	
	// Use this for initialization
	void Start () {
		panel.SetActive(false);
        //portrait.SetActive(false);
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F)) {
			if(!printText) {
				if(panel.activeSelf) {
					panel.SetActive(false);
				} else {
					printText = true;
                    text = lines[lineCount].line;
                    portrait.sprite = lines[lineCount].sprite;
					if(PrintWordByWord)
						StartCoroutine("PrintTextByWord");										
					else
						StartCoroutine("PrintTextByCharacter");
				}
				
			} else {
				printText = false;
			}
			
		} 

		
	}


	IEnumerator PrintTextByWord() {
		string[] words = text.Split(' ');
		panel.SetActive(true);
		Text container = panel.GetComponentInChildren<Text>();
		container.text = "";
		foreach (string word in words) {
			container.text = container.text + word + " ";
			AudioClip clip = clips[Random.Range(0,clips.Length)];
			audioSource.clip = clip;
			audioSource.Play();
			if(printText)
				yield return new WaitForSeconds(0.1f);
		}
		printText = false;
	}

	IEnumerator PrintTextByCharacter() {
		char[] chars = text.ToCharArray();
		panel.SetActive(true);
		Text container = panel.GetComponentInChildren<Text>();
		container.text = "";
		foreach (char c in chars) {
			container.text = container.text + c;
			AudioClip clip = clips[Random.Range(0,clips.Length/2)];
			audioSource.clip = clip;
			audioSource.Play();
			if(printText)
				yield return new WaitForSeconds(0.005f);
		}

		printText = false;
	}

}
