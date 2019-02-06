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
    [SerializeField] Text dialogueText;
    [SerializeField] Text nameText;
	// Use this for initialization
	void Start () {
		panel.SetActive(false);
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F)) {
			if(!printText) {
				if(panel.activeSelf && lineCount == lines.Length) {
					panel.SetActive(false);
                    lineCount = 0;
                }
                else {
					printText = true;
                    text = lines[lineCount].line;
                    nameText.text = lines[lineCount].characterName;
                    portrait.sprite = lines[lineCount].sprite;
					if(PrintWordByWord)
						StartCoroutine("PrintTextByWord");										
					else
						StartCoroutine("PrintTextByCharacter");
                    lineCount++;
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
        dialogueText.text = "";
		foreach (string word in words) {
            dialogueText.text = dialogueText.text + word + " ";
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
		
		dialogueText.text = "";
		foreach (char c in chars) {
			dialogueText.text = dialogueText.text + c;
			AudioClip clip = clips[Random.Range(0,clips.Length/2)];
			audioSource.clip = clip;
			audioSource.Play();
			if(printText)
				yield return new WaitForSeconds(0.005f);
		}

		printText = false;
	}

}
