using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Conversation : MonoBehaviour {
	[SerializeField]
	GameObject panel;
    [SerializeField]
    Image portrait;
	[SerializeField]
	AudioClip[] clips;
	AudioSource audioSource;
	bool printText = false;
	// string text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi in enim nibh. Quisque sed augue ac dolor molestie mattis ut vitae lectus. Suspendisse potenti. Nulla fringilla";
    int lineCount = 0;
	[SerializeField]
	bool PrintWordByWord = false;
    [SerializeField] Text dialogueText;
    [SerializeField] Text nameText;

	string text;
	// Use this for initialization
	void Start () {
		panel.SetActive(false);
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	public void Play (List<Lines> lines) {
		if(!printText) {
			if(panel.activeSelf && lineCount == lines.Count) {
				panel.SetActive(false);
				lineCount = 0;
			}
			else {
				Character c = lines[lineCount].character;
				printText = true;
				text = lines[lineCount].line;
				nameText.text = c.characterName;
				portrait.sprite = c.sprite;
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
