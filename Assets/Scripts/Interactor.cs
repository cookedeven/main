using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public float detectRange = 3f;
    public string holdableTag = "Holdable";

    private Holdable currentTarget;
    private PlayerMovement controller;
    void Start()
    {
        controller = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (controller == null || controller.heldObject != null)
        {
            return;
        }

        FindClosestHoldable();

        if (currentTarget != null)
        {
            currentTarget.SetHightlight(true);
        }

        if (Input.GetMouseButtonDown(0) && currentTarget != null)
        {
            controller.PickUpObject(currentTarget.gameObject);
            currentTarget.IsHeld = true;
            currentTarget.SetHightlight(false);
        }
    }

    void FindClosestHoldable()
    {
        GameObject[] holdables = GameObject.FindGameObjectsWithTag(holdableTag);
        float closest = detectRange;
        currentTarget = null;

        foreach (var obj in holdables)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            Holdable holdable = obj.GetComponent<Holdable>();

            if (distance < closest && holdable != null && !holdable.IsHeld)
            {
                if (currentTarget != null)
                {
                    currentTarget.SetHightlight(false);
                }

                currentTarget = holdable;
                closest = distance;
            }
            else if(holdable != null && holdable != currentTarget)
            {
                holdable.SetHightlight(false);
            }
        }
    }
}
