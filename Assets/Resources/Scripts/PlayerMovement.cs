using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody rigidBody;
    Character characterInput;
    Vector2 movementInput;
    bool jumpInput;
    [SerializeField]
    Camera cam;
    [SerializeField]
    float clamp= 5.0f;
    [SerializeField]
    float clampMaxSprint = 10.0f;
    private void Awake()
    {
        characterInput = new Character();
        characterInput.Player.Movement.performed += ctx => Movement_performed(ctx.ReadValue<Vector2>());
        characterInput.Player.Jump.started += ctx => Jump_Performed(ctx.ReadValue<bool>());
        //characterInput.Player.Jump.performed += ctx => Movement_performed(ctx.ReadValue<bool>());
        rigidBody = GetComponent<Rigidbody>();
    }
    private void Jump_Performed(bool j)
    {
        jumpInput = j;
    }

    private void Movement_performed(Vector2 inpt)
    {
        //Debug.Log(inpt);
        movementInput = inpt;
    }

    private void OnEnable()
    {
        characterInput.Enable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movementInput.x > 0.0f || movementInput.x < 0.0f ||
            movementInput.y > 0.0f || movementInput.y < 0.0f
            )
        {
            Vector3 d = transform.position - cam.transform.position;
            d.y = 0.0f;
            Vector3 r = Vector3.Cross(Vector3.up,d).normalized;
            Vector3 f = Vector3.Cross(Vector3.Cross(Vector3.up, d).normalized,Vector3.up);
            Debug.Log(f);
            Vector3 x = Vector3.Scale(r, new Vector3(movementInput.x, 0, movementInput.x));
            Vector3 y = Vector3.Scale(f, new Vector3(movementInput.y, movementInput.y, movementInput.y));
            Vector3 vel = rigidBody.velocity;
            vel += x;
            vel += y;
            vel.x = Mathf.Max(Mathf.Min(vel.x, clamp), -clamp);
            vel.z = Mathf.Max(Mathf.Min(vel.z, clamp), -clamp);
            rigidBody.velocity = vel;

            //Debug.Log(x);
            //Debug.Log(y);
        }
        if (jumpInput)
        {

        }
    }
}
