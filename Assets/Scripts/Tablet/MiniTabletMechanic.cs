using UnityEngine;
using TMPro;

public class MiniTabletMechanic : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _questNameText;
    
    private bool _questIsReady;
    
    private void Update()
    {
        if (_questIsReady && Input.GetKeyDown(KeyCode.F))
        {
            GameEvents.OnStartQuest?.Invoke();
            
            _questIsReady = false;
            _questNameText.gameObject.SetActive(false);
        }
    }

    public void ReadyQuest(string questName)
    {
        _questIsReady = true;
        _questNameText.gameObject.SetActive(true);
        _questNameText.text = questName + " is Ready! \n" + "Press \"F\"";
    }
}
