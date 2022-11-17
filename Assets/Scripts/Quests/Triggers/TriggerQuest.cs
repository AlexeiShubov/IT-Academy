using UnityEngine;

public class TriggerQuest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetType() == typeof(CharacterMove))
        {
            Debug.Log("Player");
        }
    }
}
