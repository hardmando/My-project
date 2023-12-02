using UnityEngine;

[CreateAssetMenu(fileName = "NoteScript", menuName = "Note Script Content", order = 1)]
public class NoteScript : ScriptableObject
{
    public string noteName;
    public string noteContent;
}
