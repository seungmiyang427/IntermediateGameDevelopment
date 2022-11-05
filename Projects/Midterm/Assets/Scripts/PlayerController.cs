using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //movement
    public float moveSpeed;

    private bool isMoving;
    private Vector2 input;

    //animation
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //layers
    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;

    [SerializeField] private AudioSource walkSfx;

    public void HandleUpdate() //handleupdate() so that gamecontroller script can use this part of the code
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal"); //only has two values (for calculating direction)- 1 = right, -1 = left;
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) //prevent diagonal movement
                input.y = 0;

            if(input != Vector2.zero) //calculates target position to which player moves (current position + input)
            {
                //play animation
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                //movement
                walkSfx.Play(); //walk sfx
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (IsWalkable(targetPos)) //code below
                StartCoroutine(Move(targetPos)); //initiate code below
            }
        }

        //transition from idle to walk and vice versa
        animator.SetBool("isMoving", isMoving);

        if (Input.GetKeyDown(KeyCode.E))
            Interact();
    }

    void Interact() //interact in the direction player is facing
    {
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;

        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, interactableLayer); //check for objects
        if (collider != null) //if there is an object...
        {
            collider.GetComponent<Interactable>()?.Interact(); //calling interact function from NPCController script through Interactable interface
        }
    }


    IEnumerator Move(Vector3 targetPos) //coroutine- moving the player from starting position over a period of time. 
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon) //checks if the difference between target position and player's current position is greater than a very small value (mathf.epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime); //movement towards target position, as a result will stop the while statement
            yield return null; //stops coroutine after execution and readies it for next update function
        }

        transform.position = targetPos; //result
        isMoving = false;
    }


    private bool IsWalkable(Vector3 targetPos) //check for collisions
    {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer | interactableLayer) != null)
        {
            return false; //tile is not walkable; there is something there
        }
        return true;
    }
}
