using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public event Action<string> OnActionPressed;
    private PlayerInput controls;

    [Header("Player State")]
    public Vector3 playerPosition;
    private void Awake()
    {
        bool playerManagerHasBeenMade = Instance != null;
        bool thisPlayerManagerIsNotItself = Instance != this;

        if (playerManagerHasBeenMade && thisPlayerManagerIsNotItself)
        {
            
            Destroy(transform.root.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(transform.root.gameObject);

        controls = new PlayerInput();

        controls.Player.LeftClick.performed += ctx => OnActionPressed?.Invoke("LeftClick");
        controls.Player.RightClick.performed += ctx => OnActionPressed?.Invoke("RightClick");
        controls.Player.WalkUp.performed += ctx => OnActionPressed?.Invoke("WalkUpD");
        controls.Player.WalkLeft.performed += ctx => OnActionPressed?.Invoke("WalkLeftD");
        controls.Player.WalkDown.performed += ctx => OnActionPressed?.Invoke("WalkDownD");
        controls.Player.WalkRight.performed += ctx => OnActionPressed?.Invoke("WalkRightD");
        controls.Player.WalkUp.canceled += ctx => OnActionPressed?.Invoke("WalkUpC");
        controls.Player.WalkLeft.canceled += ctx => OnActionPressed?.Invoke("WalkLeftC");
        controls.Player.WalkDown.canceled += ctx => OnActionPressed?.Invoke("WalkDownC");
        controls.Player.WalkRight.canceled += ctx => OnActionPressed?.Invoke("WalkRightC");
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

}