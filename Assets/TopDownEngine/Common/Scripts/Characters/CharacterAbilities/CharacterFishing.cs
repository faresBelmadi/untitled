using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;
using System;

[AddComponentMenu("TopDown Engine/Character/Abilities/CharacterFishing")]
public class CharacterFishing : CharacterAbility
{
    /// This method is only used to display a helpbox text
    /// at the beginning of the ability's inspector
    public override string HelpBoxText() { return "allow your character to fish"; }

    [Header("TODO_HEADER")]
    /// declare your parameters here
    public bool nearWater;
    public List<string> InputList;
    public List<string> WaitingInputList;
    public float delayWaitingList;
    public float MaxTimeQTE;
    public Transform QTEPromptTransform;
    public ButtonPrompt prompt;
    public ButtonPrompt SpawnedPrompt;
    public List<GameObject> SpawnedInputList;
    public List<GameObject> SpawnedWaitingInputListList;


    protected const string _yourAbilityAnimationParameterName = "YourAnimationParameterName";
    protected int _yourAbilityAnimationParameter;

    /// <summary>
    /// Here you should initialize our parameters
    /// </summary>
    protected override void Initialization()
    {
        base.Initialization();
        nearWater = false;
    }

    /// <summary>
    /// Every frame, we check if we're crouched and if we still should be
    /// </summary>
    public override void ProcessAbility()
    {
        if(nearWater)
        {
            ShowPrompt();
        }
        else
        {
            HidePrompt();
        }

        base.ProcessAbility();
    }

    private void SpawnQtePrompt()
    {
    }
    public virtual void ShowPrompt()
    {
        // we add a blinking A prompt to the top of the zone
        if (SpawnedPrompt == null)
        {
            SpawnedPrompt = (ButtonPrompt)Instantiate(prompt);
            SpawnedPrompt.Initialization();
            //_buttonPromptAnimator = SpawnedPrompt.gameObject.MMGetComponentNoAlloc<Animator>();
        }
        
        SpawnedPrompt.transform.position = QTEPromptTransform.position;

        SpawnedPrompt.transform.parent = transform;
        //SpawnedPrompt.transform.localEulerAngles = PromptRotation;
        SpawnedPrompt.SetText("a");
        SpawnedPrompt.SetBackgroundColor(Color.red);
        SpawnedPrompt.SetTextColor(Color.white);
        SpawnedPrompt.Show();
    }
    public virtual void HidePrompt()
    {
        if (SpawnedPrompt != null)
        {
            SpawnedPrompt.Hide();
        }
    }
    public void CheckWater(bool value)
    {
        nearWater = value;
    }

    /// <summary>
    /// Called at the start of the ability's cycle, this is where you'll check for input
    /// </summary>
    protected override void HandleInput()
    {
        // here as an example we check if we're pressing down
        // on our main stick/direction pad/keyboard
        if (_inputManager.InteractButton.IsPressed && nearWater)
        {
            StartFishing();
        }
    }

    /// <summary>
    /// If we're pressing down, we check for a few conditions to see if we can perform our action
    /// </summary>
    protected virtual void StartFishing()
    {
        // if the ability is not permitted
        if (!AbilityPermitted
            // or if we're not in our normal stance
            || (_condition.CurrentState != CharacterStates.CharacterConditions.Normal)
            // or if we're grounded
            || (!_controller.Grounded))
        {
            // we do nothing and exit
            return;
        }

        // if we're still here, we display a text log in the console
        MMDebug.DebugLogTime("We're fishing something yay!");
    }

    /// <summary>
    /// Adds required animator parameters to the animator parameters list if they exist
    /// </summary>
    protected override void InitializeAnimatorParameters()
    {
        RegisterAnimatorParameter(_yourAbilityAnimationParameterName, AnimatorControllerParameterType.Bool, out _yourAbilityAnimationParameter);
    }

    /// <summary>
    /// At the end of the ability's cycle,
    /// we send our current crouching and crawling states to the animator
    /// </summary>
    public override void UpdateAnimator()
    {

        bool myCondition = true;
        MMAnimatorExtensions.UpdateAnimatorBool(_animator, _yourAbilityAnimationParameter, myCondition, _character._animatorParameters);
    }
}
