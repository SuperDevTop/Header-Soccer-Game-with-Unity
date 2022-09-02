using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressButton : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveDownButton(GameObject btn)
    {

        btn.transform.localScale = new Vector3(btn.transform.localScale.x + 0.05f, btn.transform.localScale.y + 0.05f, 1);
        btn.GetComponent<Image>().CrossFadeAlpha(0.3f, 0.1f, true);

    }
    public void MoveUpButton(GameObject btn)
    {

        btn.transform.localScale = new Vector3(btn.transform.localScale.x - 0.05f, btn.transform.localScale.y - 0.05f, 1);
        btn.GetComponent<Image>().CrossFadeAlpha(1f, 0.1f, true);

    }
    public void MoveExitButton(GameObject btn)
    {

        btn.GetComponent<Image>().CrossFadeAlpha(1f, 0.1f, true);

    }
}
