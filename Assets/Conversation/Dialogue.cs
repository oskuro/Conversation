using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Dialogue : ScriptableObject
{
    [SerializeField]
    public List<Lines> lines;
}

[System.SerializableAttribute]
public class Lines {
        public Character character;
        public string line;
}