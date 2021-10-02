using UnityEngine;
using TMPro;

public static class TMPHelpers
{
    public static void SetTextAlpha(TextMeshProUGUI text, float alpha)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
    }
}
