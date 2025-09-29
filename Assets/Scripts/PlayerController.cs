using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float rotationSpeed = 10f;
    public Transform stackAnchor;
    public KeyCode interactKey = KeyCode.E;


    CharacterController controller;
    Vector3 moveDir;

    private Vector2 startTouchPos;
    private Vector2 currentTouchPos;
    private bool isDragging = false;

    private Camera cam;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            controller = gameObject.AddComponent<CharacterController>();
            controller.height = 1.8f;
            controller.radius = 0.3f;
        }
        cam = Camera.main;
    }


    void Update()
    {
        HandleTouchMovement();
        ClampInsideCameraView();
     //   Move();
        /*     if (Input.GetKeyDown(interactKey))
             {
                 InteractNearby();
             }*/
    }


    void HandleTouchMovement()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPos = touch.position;
                isDragging = true;
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                currentTouchPos = touch.position;
                Vector2 delta = (currentTouchPos - startTouchPos).normalized;

                Vector3 moveDir = new Vector3(delta.x, 0, delta.y);

                if (moveDir.magnitude > 0.1f)
                {
                    // Smooth rotation
                    transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
                    controller.Move(moveDir * moveSpeed * Time.deltaTime);
                }
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isDragging = false;
            }


            // Lock Y axis
            Vector3 pos = transform.position;
            pos.y = 1f;
            transform.position = pos;
        }
    }


    void ClampInsideCameraView()
    {
        // Convert player position to viewport (0..1 in x and y)
        Vector3 viewPos = cam.WorldToViewportPoint(transform.position);

        // Clamp values
        viewPos.x = Mathf.Clamp01(viewPos.x);
        viewPos.y = Mathf.Clamp01(viewPos.y);

        // Convert back to world
        Vector3 clampedWorld = cam.ViewportToWorldPoint(viewPos);

        // Keep original Y (height), adjust only X and Z
        transform.position = new Vector3(clampedWorld.x, transform.position.y, clampedWorld.z);
    }

    void InteractNearby()
    {
        // Simple sphere-check to find interactables
        Collider[] hits = Physics.OverlapSphere(transform.position, 1f);
        foreach (var c in hits)
        {
            var beanSource = c.GetComponent<BeanSource>();
            if (beanSource != null)
            {
                beanSource.PickBean(this);
                return;
            }
            var machine = c.GetComponent<CoffeeMachine>();
            if (machine != null)
            {
                machine.InsertBag(this);
                return;
            }
            var cup = c.GetComponent<CoffeeCup>();
            if (cup != null)
            {
                cup.PickUp(this);
                return;
            }
            var customer = c.GetComponent<CustomerAI>();
            if (customer != null)
            {
                customer.ReceiveCoffee(this);
                return;
            }
        }
    }
}
