using UnityEngine;
using UnityEngine.UI;

public class PlayerCollisionChecker : MonoBehaviour
{
    PlayerMovementController playerMovementController;
    public Text StatusText;

    private string haveKeysToEnter = "Press E to go to next level.";
    private string needKeysToEnter = "You have";

    private void Start()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Hurt")
        {
            if (playerMovementController.IsLiving)
            {
                playerMovementController.RemoveLife(1);
                Destroy(other.gameObject);
            }
        }

        if (other.tag == "Life")
        {
            if (playerMovementController.PlayerLife <= 4 && !playerMovementController.IsLiving)
            {
                playerMovementController.AddLife(1);
                Destroy(other.gameObject);
            }
        }

        if(other.tag == "Rift")
        {
            if(playerMovementController.PlayerLife >= 5)
            {
                playerMovementController.SetToLiving();
            }
        }

        if(other.tag == "Key")
        {
            playerMovementController.AddKey();
            Destroy(other.gameObject);
        }

        if(other.tag == "Door")
        {
            int haveKeys = playerMovementController.KeyAmount;
            int neededKeys = other.gameObject.GetComponent<NextLevelDoor>().KeyAmountToUnlock;

            if(haveKeys == neededKeys)
            {
                StatusText.text = haveKeysToEnter;
                playerMovementController.levelClearedStatus = true;
                playerMovementController.nextSceneId = other.gameObject.GetComponent<NextLevelDoor>().NextLevelID;
            }
            else
            {
                StatusText.text = $"{needKeysToEnter} {haveKeys}/{neededKeys} keys";
                playerMovementController.levelClearedStatus = false;
            }

            StatusText.gameObject.SetActive(true);
        }

        if(other.tag == "Button")
        {
            other.gameObject.GetComponent<ButtonAction>().DestroyAndDisable();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Door")
        {
            StatusText.gameObject.SetActive(false);
            playerMovementController.levelClearedStatus = false;
        }
    }
}