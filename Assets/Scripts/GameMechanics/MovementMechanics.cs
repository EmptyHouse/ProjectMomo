using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CustomPhysics2D))]
public class MovementMechanics : MonoBehaviour {
    private const float INPUT_THRESHOLD_RUNNING = .6f;
    private const float INPUT_THRESHOLD_WALKING = .15f;

    #region main variables
    [Header("Mono References")]
    [Tooltip("A reference to the sprite renderer object")]
    public SpriteRenderer spriteRenderer;
    [Header("Ground Movement")]
    [Tooltip("The maximum walking speed")]
    public float walkingSpeed = 2f;
    [Tooltip("The maximum running speed")]
    public float runningSpeed = 5f;
    [Tooltip("The units per second that our speed will increase")]
    public float groundAcceleration = 25f;

    [Header("Sprite Renderer Referneces")]
    [Tooltip("This indicates what direction our sprite will be facing. This will change based on input")]
    public bool isFacingRight;

    [Header("Jumping Variables")]
    [Tooltip("The time it will take for our character to reach the top of their jump")]
    public float timeToReachJumpApex = 1;
    [Tooltip("The height in units that our character will reach when they perform a jump")]
    public float heightOfJump = 1;
    [Tooltip("The scale we apply to our character when they are fast falling, typically meaning the player isn't holding down the jump button")]
    public float fastFallScale = 1.7f;
    /// <summary>
    /// Jump velocity is calculated based on the jump height and time to reach apex
    /// </summary>
    private float jumpVelocity;

    /// <summary>
    /// The custom physics component reference
    /// </summary>
    private CustomPhysics2D rigid;

    /// <summary>
    /// The last horizontal input that was passed in
    /// </summary>
    private float horizontalInput;
    #endregion main variables

    #region monobehaivour methods
    private void Awake()
    {
        rigid = GetComponent<CustomPhysics2D>();
    }

    private void Update()
    {
        UpdateCurrentSpeedOnGround();
    }

    private void OnValidate()
    {
        if (!rigid)
        {
            rigid = GetComponent<CustomPhysics2D>();
        }
        SetSpriteFlipped(isFacingRight);

        float gravity = (2 * heightOfJump) / Mathf.Pow(timeToReachJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToReachJumpApex;
        rigid.gravityScale = gravity / CustomPhysics2D.GRAVITY_CONSTANT;
    }
    #endregion monobehaviour methods
    
    /// <summary>
    /// Sets the horizontal input that will determine the speed that our character will move
    /// </summary>
    /// <param name="horizontalInput"></param>
    public void SetHorizontalInput(float horizontalInput)
    {
        this.horizontalInput = horizontalInput;
        FlipSpriteBasedOnInput(this.horizontalInput);
    }

    /// <summary>
    /// Updates the speed of our character while they are grounded
    /// </summary>
    private void UpdateCurrentSpeedOnGround()
    {
        float goalSpeed = 0;
        if (Mathf.Abs(horizontalInput) > INPUT_THRESHOLD_RUNNING)
        {
            goalSpeed = runningSpeed * Mathf.Sign(horizontalInput);
        }
        else if (Mathf.Abs(horizontalInput) > INPUT_THRESHOLD_WALKING)
        {
            goalSpeed = walkingSpeed * Mathf.Sign(horizontalInput);
        }
        Vector2 newVelocityVector = new Vector2(rigid.velocity.x, rigid.velocity.y);
        newVelocityVector.x = Mathf.MoveTowards(rigid.velocity.x, goalSpeed, Time.deltaTime * groundAcceleration);
        rigid.velocity = newVelocityVector;
    }


    /// <summary>
    /// Flips the character's sprite appropriately based on the input passed through
    /// </summary>
    /// <param name="horizontalInput"></param>
    private void FlipSpriteBasedOnInput(float horizontalInput)
    {
        if (Mathf.Abs(horizontalInput) < INPUT_THRESHOLD_WALKING)
        {
            return;
        }

        if (horizontalInput < 0 && isFacingRight)
        {
            SetSpriteFlipped(false);
        }
        else if (horizontalInput > 0 && !isFacingRight)
        {
            SetSpriteFlipped(true);
        }
    }

    /// <summary>
    /// Sets whether our sprite is facing left or right
    /// </summary>
    private void SetSpriteFlipped(bool spriteFacingright)
    {
        this.isFacingRight = spriteFacingright;
        if (spriteFacingright)
        {
            Vector3 currentScale = spriteRenderer.transform.localScale;
            currentScale.x = Mathf.Abs(currentScale.x);
            spriteRenderer.transform.localScale = currentScale;
        }
        else
        {
            Vector3 currentScale = spriteRenderer.transform.localScale;
            currentScale.x = -Mathf.Abs(currentScale.x); ;
            spriteRenderer.transform.localScale = currentScale;
        }
    }


    #region jumping methods
    /// <summary>
    /// Method that performs a jump by applying a velocity in the y direction
    /// </summary>
    /// <returns></returns>
    public bool Jump()
    {
        if (rigid.isInAir)
        {
            //if (doubleJumpAvailable)
            //{
            //    rigid.velocity = new Vector2(rigid.velocity.x, jumpVelocity);
            //    doubleJumpAvailable = false;
            //    return true;
            //}
            return false;
        }
        rigid.velocity = new Vector2(rigid.velocity.x, jumpVelocity);
        return true;
    }
    #endregion jumping methods

}
