using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class Conversation : MonoBehaviour {
	[SerializeField]
	GameObject panel;
    [SerializeField]
    Image portrait;
	AudioClip[] clips;
    [SerializeField]
    AudioClip[] defaultClips;
	AudioSource audioSource;
	bool printingText = false;
    int lineCount = 0;
	[SerializeField]
	bool PrintWordByWord = false;
    [SerializeField] Text dialogueText;
    [SerializeField] Text nameText;

	string text;
	void Start () {
		panel.SetActive(false);
		audioSource = GetComponent<AudioSource>();
	}
	
	public void PlayDialogue (Dialogue dialogue) {
        List<Lines> lines = dialogue.lines;
		if(!printingText) {
            // This happens if we have shown all the lines in the dialogue
			if(panel.activeSelf && lineCount == lines.Count) {
				panel.SetActive(false);
				lineCount = 0;
			}
			else {
                // This happens if the settings are set to printing over time.
				Character c = lines[lineCount].character;
				printingText = true;
				text = lines[lineCount].line;
				nameText.text = c.characterName;
                ShowSprite(c.sprite);
                AssignVoice(c.voice);
				if(PrintWordByWord)
					StartCoroutine("PrintTextByWord");										
				else
					StartCoroutine("PrintTextByCharacter");
				lineCount++;
			}
			
		} else {
            // This happens when the method is called while printing the text. 
            // is causes the printing to skip to the end immediatly.
			printingText = false;
		}
	}

    private void AssignVoice(Voice voice)
    {
        if(voice == null)
        {
            clips = defaultClips;
        } else
        {
            clips = voice.clips.ToArray();
        }
    }

    private void ShowSprite(Sprite sprite)
    {
        if (sprite == null)
        {
            portrait.gameObject.SetActive(false);
        }
        else
        {
            portrait.gameObject.SetActive(true);
            portrait.sprite = sprite;
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
			if(printingText)
				yield return new WaitForSeconds(0.1f);
		}
		printingText = false;
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
			if(printingText)
				yield return new WaitForSeconds(0.005f);
		}

		printingText = false;
	}

}
