using TMPro;
using UnityEngine;

public class Dialogue
{
    TextMeshPro dialogue;
    private string text;
    private string[] choice;

    private int i;
    public Dialogue(string text, string[] choice)
    {
        this.text = text;
        this.choice = choice;
        dialogue = GameObject.FindGameObjectsWithTag("dialogue")[0].transform.GetChild(0).GetComponent<TextMeshPro>();
    }
    public void showCharacter()
    {
        dialogue.text += text[i];
        i++;
    }
}
