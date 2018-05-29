using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrambleScript : MonoBehaviour {


    private InputField input;
    public Transform cube;
    private RotateScript r;

    void Start () {
        input = GetComponent<InputField>();
        r = cube.GetComponent<RotateScript>();
    }


    public void ScrambleCube() {
        string movesastext = input.text;
        string[] moves = movesastext.Split(',');

        foreach (string move in moves)
        {
            Debug.Log(move);

        }

        r.SetActive(true);

    }

    
}
