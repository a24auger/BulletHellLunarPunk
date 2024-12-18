using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine;

public class DialogueReader : MonoBehaviour
{

    DialogueHolder currentText = null;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] Animator runicAnimator;
    private TMP_FontAsset defaultFont;


    private void Start()
    {
        defaultFont = textMesh.font; 
    }

    public void SetHolder(DialogueHolder holder)
    {
        currentText = holder;
        
        textMesh.fontSize = currentText.FontSize;
        if (currentText.DefaultFont != null)
            textMesh.font = currentText.DefaultFont;
        else
            textMesh.font = defaultFont;
    }

    public void OnDialogueEnter()
    {
        if(currentText == null)
        {
            return;
        }
        if (currentText.IsRunic)
        {
            StartRunicText();
        }
        else
        {
            StartNormalText();
        }
        OnNextDialogue();
        playerInput.SwitchCurrentControlScheme("UI");
    }

    public void OnNextDialogue()
    {
        string text = currentText.NextString();
        textMesh.text = text;
        if (text.Equals(""))
        {
            EndDialogue();
        }
    }

    private void StartRunicText()
    {
        runicAnimator.SetTrigger("Runic");
        runicAnimator.SetTrigger("Reading");
    }

    private void StartNormalText()
    {
        runicAnimator.SetTrigger("Runic");
        runicAnimator.SetTrigger("Reading");
    }

    private void EndRunicText()
    {
        runicAnimator.SetTrigger("Ending");
    }

    private void EndNormalText()
    {
        runicAnimator.SetTrigger("Ending");
    }

    private void EndDialogue()
    {
        if(currentText.IsRunic)
        {
            EndRunicText();
        }
        else
        {
            EndNormalText();
        }
        playerInput.SwitchCurrentControlScheme("Player");
        currentText = null;
    }
}
