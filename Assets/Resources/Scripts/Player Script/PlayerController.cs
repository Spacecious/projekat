using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public InputAction moveAction, dashAction, gambleAction;
    [HideInInspector] public InputAction fireUp, fireDown, fireLeft, fireRight;

    public static bool firstBossDefeated = false;

    [Header("UI Reference (Dodaj u Editoru)")]
    public GameObject dashIcon;
    public GameObject gambleIcon;
    public SlotMachineUI slotUI;

    void Awake()
    {
        // Inicijalizacija svih inputa
        moveAction = InputSystem.actions.FindAction("Move");
        dashAction = InputSystem.actions.FindAction("Jump");
        gambleAction = InputSystem.actions.FindAction("Gamble");
        fireUp = InputSystem.actions.FindAction("ShootUp");
        fireDown = InputSystem.actions.FindAction("ShootDown");
        fireLeft = InputSystem.actions.FindAction("ShootLeft");
        fireRight = InputSystem.actions.FindAction("ShootRight");
    }
}
