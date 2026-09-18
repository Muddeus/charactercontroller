using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private InputActionAsset playerControls;

    [SerializeField] private string actionMapName = "Player";

    [SerializeField] private string move = "Move";
    [SerializeField] private string look = "Look";
    [SerializeField] private string sprint = "Sprint";
    [SerializeField] private string jump = "Jump";
    

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction sprintAction;
    private InputAction jumpAction;
    //private InputAction newAction;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool SprintTriggered { get; private set; }
    public bool JumpTriggeredThisFrame => jumpAction.triggered;
    //public bool/Vector2 NewActionPressedThisFrame/NewActionTriggered/NewActionInput => shootAction.triggered/{ get; private set; };

    private void Awake()
    {
        InputActionMap mapReference = playerControls.FindActionMap(actionMapName);

        moveAction = mapReference.FindAction(move);
        lookAction = mapReference.FindAction(look);
        sprintAction = mapReference.FindAction(sprint);
        jumpAction = mapReference.FindAction(jump);
        //newActionAction = mapReference.FindAction(interact);

        SubscribeActionValuesToInputEvents();
    }

    private void SubscribeActionValuesToInputEvents()
    {
        moveAction.performed += inputInfo => MoveInput = inputInfo.ReadValue<Vector2>();
        moveAction.canceled += inputInfo => MoveInput = Vector2.zero;

        lookAction.performed += inputInfo => LookInput = inputInfo.ReadValue<Vector2>();
        lookAction.canceled += inputInfo => LookInput = Vector2.zero;

        sprintAction.performed += inputInfo => SprintTriggered = true;
        sprintAction.canceled += inputInfo => SprintTriggered = false;

    }

    private void OnEnable()
    {
        playerControls.FindActionMap(actionMapName).Enable();
    }
    private void OnDisable()
    {
        playerControls.FindActionMap(actionMapName).Disable();
    }
}
