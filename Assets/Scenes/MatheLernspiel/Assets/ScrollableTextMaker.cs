using UnityEngine;
using UnityEngine.UI;

public class ScrollableTextMaker : MonoBehaviour
{
    [SerializeField] int padding = 1;
    float scrollDistance;
    float thirdLength;
    Text textObject;
    Scrollbar scrollbar;

    public string Text
    {
        get
        {
            return textObject.text;
        }
        set
        {
            textObject.text = value;
            ResetScroll();
        }
    }

    void Awake()
    {
        textObject = gameObject.GetComponentInChildren<Text>();
        scrollbar = gameObject.GetComponentInChildren<Scrollbar>();
        ResetScroll();
    }

    public void OnScroll() // aktualisiere die textposition für die aktuelle Scrollposition
    {
        if(textObject.preferredHeight > GetComponent<RectTransform>().sizeDelta.y + padding)
        {
            textObject.rectTransform.anchoredPosition = new Vector2(textObject.rectTransform.anchoredPosition.x, (scrollDistance * scrollbar.value) - thirdLength);
        }
        else
        {
            textObject.rectTransform.anchoredPosition = new Vector2(textObject.rectTransform.anchoredPosition.x, 0);
        }
    }

    void ResetScroll() //Passe Werte für geänderte Textgröße an
    {
        scrollbar.gameObject.SetActive(textObject.preferredHeight > GetComponent<RectTransform>().sizeDelta.y + padding); // Zeige die Scrollbar an wenn der Text Länger ist als das Feld
        textObject.rectTransform.sizeDelta = new Vector2(textObject.rectTransform.sizeDelta.x, textObject.preferredHeight); // setze die höhe des Textfeldes auf die für die Textlänge erforderliche Größe
        thirdLength = textObject.preferredHeight * 0.333f; //berechne ein drittel der erforderlichen Höhe für die passende Ausrichtung
        scrollDistance = textObject.preferredHeight - GetComponent<RectTransform>().sizeDelta.y + padding; //Berechne die Höhe die Auserhalb des Sichtbereiches ist
        scrollbar.value = 0;// Scrolle nach ganz oben
        OnScroll();
    }
}