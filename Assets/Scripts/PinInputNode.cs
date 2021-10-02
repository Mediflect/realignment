using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PinInputNode : MonoBehaviour
{
    private static List<int> cachedIndexChoices = new List<int>();

    public string allowedCharacters = "abcdefghijklmnopqrstuvwxyz";
    public TextMeshProUGUI textDisplay;
    public bool AtTarget => atTarget;

    private List<string> characterCycle = new List<string>();
    private int target = 0;
    private int current = 1;
    private bool atTarget = false;

    public void Initialize(int numCharacters)
    {
        if (numCharacters > allowedCharacters.Length)
        {
            Debug.LogError("not enough unique characters");
            numCharacters = allowedCharacters.Length;
        }

        cachedIndexChoices.Clear();
        characterCycle.Clear();
        for (int i = 0; i < allowedCharacters.Length; ++i)
        {
            cachedIndexChoices.Add(i);
        }
        for (int i = 0; i < numCharacters; ++i)
        {
            int random = Random.Range(0, cachedIndexChoices.Count);
            int characterIndex = cachedIndexChoices[random];
            characterCycle.Add(allowedCharacters.Substring(characterIndex, 1));
            cachedIndexChoices.RemoveAt(random);
        }
        target = 0;     // just make the first item in the list always be the target
        current = Random.Range(1, numCharacters);   // and start on a different item
        RefreshText();
    }

    public void OnUpClicked()
    {
        ++current;
        current = current % characterCycle.Count;
        RefreshText();
    }

    public void OnDownClicked()
    {
        --current;
        if (current < 0)
        {
            current = characterCycle.Count - 1;
        }
        RefreshText();
    }

    private void RefreshText()
    {
        textDisplay.SetText(characterCycle[current]);
        atTarget = current == target;
    }

    [ContextMenu("Init 4")]
    private void TestInitialize4()
    {
        Initialize(4);
    }

    [ContextMenu("Init 8")]
    private void TestInitialize8()
    {
        Initialize(8);
    }
}
