using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PinMinigame : Minigame
{
    public List<PinInputNode> inputNodes;
    public int charsPerNode = 5;

    private int nodesCorrect = 0;

    private void OnEnable()
    {
        for (int i = 0; i < inputNodes.Count; ++i)
        {
            inputNodes[i].Initialize(5);
        }
    }

    protected override void Update()
    {
        nodesCorrect = 0;
        for (int i = 0; i < inputNodes.Count; ++i)
        {
            if (inputNodes[i].AtTarget)
            {
                ++nodesCorrect;
            }
        }

        progress = Mathf.InverseLerp(0, inputNodes.Count, nodesCorrect);
        base.Update();
    }
}
