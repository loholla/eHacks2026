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
        if (Instance != null && Instance != this)
        {
            Destroy(transform.root.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(transform.root.gameObject);

        controls = new PlayerInput();

        controls.Player.LeftClick.performed += ctx => OnActionPressed?.Invoke("LeftClick");
        controls.Player.RightClick.performed += ctx => OnActionPressed?.Invoke("RightClick");
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

}