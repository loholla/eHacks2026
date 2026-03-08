using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMove_Pitfall : MonoBehaviour
{
    private bool walkUp = false;
    private bool walkLeft = false;
    private bool walkDown = false;
    private bool walkRight = false;
    private float movement = 5f;
    private void OnEnable()
    {
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.OnActionPressed += HandleAction;
        }
    }

    private void OnDisable()
    {
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.OnActionPressed -= HandleAction;
        }
    }

    private void HandleAction(string actionName)
    {
        if (actionName == "WalkUpD") {
            walkUp = true;
        } else if (actionName == "WalkLeftD") {
            walkLeft = true;
        } else if (actionName == "WalkDownD") {
            walkDown = true;
        } else if (actionName == "WalkRightD") {
            walkRight = true;
        } else if (actionName == "WalkUpC") {
            walkUp = false;
        } else if (actionName == "WalkLeftC") {
            walkLeft = false;
        } else if (actionName == "WalkDownC") {
            walkDown = false;
        } else if (actionName == "WalkRightC") {
            walkRight = false;
        }
    }

    void Update()
    {
        Vector3 direction = Vector3.zero;
        if (walkUp)
        {
            direction.z = 1;
        }
        else if (walkLeft)
        {
            direction.x = -1;
        }
        else if (walkDown)
        {
            direction.z = -1;
        }
        else if (walkRight)
        {
            direction.x = 1;
        }
        transform.position += direction * movement * Time.deltaTime;
    }
}
