using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit
{

    //Current State the player is in
    private PlayerStateBase currentState;
    public string curentStateNae;

    //Public access to the current state
    public PlayerStateBase CurrentState 
    {
        get { return currentState; }
    }

    //Public State
    public PlayerMovingState playerMovingState = new PlayerMovingState();
    public PlayerAirboneState playerAirBoneState = new PlayerAirboneState();

    //Variable to store the inputs used to move
    public Controls controls;
    public Vector2 moveInput;

    [SerializeField]
    private List<BaseSkill> skillIst;

    public void TransitionToState(PlayerStateBase nextState)
    {
        currentState = nextState;
        currentState.EnterState(this);
    }

    public bool FindSkill(BaseSkill skill)
    {
        bool returnSkill = false;

        if(skillIst.Count > 0)
        {
            BaseSkill findSkill = skillIst.Find(x => x.IDname == skill.IDname);
            returnSkill = (findSkill != null);
        }
        return returnSkill;
    }

    public void AddSkill(BaseSkill skillTodADd)
    {
        skillIst.Add(skillTodADd);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Sets the first player state (This can change)
        TransitionToState(playerMovingState);
    }

    private void Awake()
    {
        //Here goes the Controls settings
        controls = new Controls();
        controls.Gameplay.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Movement.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    #region Sectoion for Styates

    // Update is called once per frame
    void Update()
    {
        currentState.Update(this);    
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdate(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(this, other);
    }

    private void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(this, other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        currentState.OnCollisionExit(this, collision);
    }

    #endregion
}
