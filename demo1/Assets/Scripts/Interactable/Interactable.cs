using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private string promptMessage = "Interact (E)";

    public void Interact() => Interaction();
    protected virtual void Interaction() {}
}
