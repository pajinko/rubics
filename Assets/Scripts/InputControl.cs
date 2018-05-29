using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InputControl : MonoBehaviour {

    public InputField mainInputField;
    public Transform cube;
    private RotateScript r;
    private Image image;

    public void Start()
    {
        image = transform.GetComponent<Image>();
        r = cube.GetComponent<RotateScript>();
        //Adds a listener to the main input field and invokes a method when the value changes.
#pragma warning disable CS0618 // Type or member is obsolete
        mainInputField.onValueChange.AddListener(delegate { ValueChangeCheck(); });
#pragma warning restore CS0618 // Type or member is obsolete
    }

    private void OnMouseDown()
    {
        r.SetActive(false);
    }

    // Invoked when the value of the text field changes.
    public void ValueChangeCheck()
    {
        ArrayList allowed = new ArrayList{ "R", "R'", "L", "U" };
        string test = mainInputField.text;
        test.Trim(' ');
        string[] chars = test.Split(',');

        bool ok = true;
        foreach (string ch in chars)
        {
            if (allowed.Contains(ch))
            {
                ok = true;
            }
            else
            { ok = false; }
        }

        if (!ok)
        {
            Debug.Log("error");
            image.color = Color.red;
        }
        else {
            image.color = Color.green;
        }

        //Debug.Log("Value Changed");
    }
}
