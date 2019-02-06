using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    public List<Lines> lines;

    void Update() {
        if(Input.GetButtonDown("Fire1")) {
            GameObject.Find("Conversation").GetComponent<Conversation>().Play(lines);
        }
    }
}

[System.SerializableAttribute]
public class Lines {
        public Character character;
        public string line;
}