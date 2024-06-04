using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateWorldState : MonoBehaviour
{
    TextMeshProUGUI debugText;

    private void Start()
    {
        debugText = GetComponent<TextMeshProUGUI>();
    }

    void LateUpdate()
    {
        Dictionary<string, int> worldStates = GWorld.Instance.GetWorld().GetStates();
        debugText.text = "";

        foreach (KeyValuePair<string, int> ws in worldStates)
        {
            debugText.text += ws.Key + ", " + ws.Value + "\n";
        }
    }
}
