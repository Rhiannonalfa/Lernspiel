using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] Button[] startButtons;
    [SerializeField] Button[] selectButtons;
    [SerializeField] Color selectedColor;
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] GameObject menuoverlay;
    [SerializeField] GameObject buttonPanel;
    [SerializeField] GameObject creditsoverlay;
    [SerializeField] Text credits;
    [SerializeField] Button quit;
    [SerializeField] Button credit;
    [SerializeField] GameObject auswahlmaxaufgaben;
    [SerializeField] InputField inputmaxaufgaben;
    Dictionary<Button, int> buttons;
    ColorBlock selectedColorBlock;
    ColorBlock normalColorBlock;
    int theme = -1;
    int bereich = 0;
    
    public void GenerateMenu()
    {
        creditsoverlay.gameObject.SetActive(false);
        auswahlmaxaufgaben.gameObject.SetActive(false);
        int i = 0;
        buttons = new Dictionary<Button, int>();
        foreach (LearnData lernData in XMLImporter.learnData)
        {
            Button newButton = Instantiate(buttonPrefab, buttonPanel.transform.position, Quaternion.identity, buttonPanel.transform).GetComponent<Button>();
            newButton.transform.localPosition += new Vector3(0,100-i * 100, 0);
            newButton.onClick.AddListener(() => { ChangeTheme(newButton); });
            newButton.GetComponentInChildren<Text>().text = lernData.theme;
            buttons.Add(newButton, i);
            normalColorBlock = new ColorBlock();
            normalColorBlock = newButton.colors;
            i++;
        }
        selectedColorBlock = normalColorBlock;
        selectedColorBlock.normalColor = selectedColor;
        selectedColorBlock.selectedColor = selectedColor;

        SetStartButtons();
    }

    public void ChangeTheme(Button newButton)
    {
        //buildingHelper.ChangeModule(AssignmentUtil.GetMeshTypes()[buttons[newButton]]);
        foreach(Button button in buttons.Keys)
        {
            button.colors = normalColorBlock;
        }
        newButton.colors = selectedColorBlock;
        theme = buttons[newButton];
        SetStartButtons();

    }

    public void StartLearning()
    {
        gameObject.GetComponent<LearnEnvironment>().StartLerning(theme);
    }
     public void StartExercise()
    {
        gameObject.GetComponent<ExerciseEnvironment>().StartExercise(theme, int.Parse(inputmaxaufgaben.text));
    }

     public void SetStartButtons()
    {
        foreach(Button button in startButtons)
        {
            button.gameObject.SetActive(false);
        }
        foreach (Button button in selectButtons)
        {
            button.colors = normalColorBlock;
        }
        selectButtons[bereich].colors = selectedColorBlock;
        if (theme == -1) return;
        if (bereich == 1 && !XMLImporter.learnData[theme].exerciseSet) return;
        startButtons[bereich].gameObject.SetActive(true);
        if (bereich == 1)
        {
            auswahlmaxaufgaben.gameObject.SetActive(true);
        }
    }

    public void SetBereich(int bereich)
    {
        this.bereich = bereich;
        SetStartButtons();
    }
     public void Credits()
    {
        menuoverlay.gameObject.SetActive(false);
        creditsoverlay.gameObject.SetActive(true);
        quit.gameObject.SetActive(true);
    }
    public void Quit()
    {
       Application.Quit ();
    }
}