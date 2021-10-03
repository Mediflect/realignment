using UnityEngine;
using TMPro;
using UnityEngine.UI;

public static class Helpers
{
    public static void SetTextAlpha(TextMeshProUGUI text, float alpha)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
    }

    public static void SetImageAlpha(Image image, float alpha)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
    }
}
