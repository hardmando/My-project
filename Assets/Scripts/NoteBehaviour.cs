using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteBehaviour : MonoBehaviour
{
    public NoteScript noteData;
    private GameObject overlayText;
    private GameObject overlayBg;
    private bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        overlayText = GameObject.FindGameObjectWithTag("NoteTextOverlay");
        overlayBg = GameObject.FindGameObjectWithTag("NoteBgOverlay");
    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Interact") && isActive || Input.GetKey(KeyCode.Escape))
        {
            overlayText.GetComponent<Text>().enabled = false;
            overlayBg.GetComponent<Image>().enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetButton("Interact"))
        {
            Debug.Log($"Open note {noteData.noteName}, read {noteData.noteContent}");
            overlayText.GetComponent<Text>().text = (noteData.noteName + "\n" + noteData.noteContent + "\n\n [Esc] to exit");
            overlayText.GetComponent<Text>().enabled = true;
            overlayBg.GetComponent<Image>().enabled = true;
            isActive = true;
        }
    }
}
