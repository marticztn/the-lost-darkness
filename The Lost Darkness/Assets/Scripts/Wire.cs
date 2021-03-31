using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public SpriteRenderer wireEnd;
    public GameObject lightOn;
    public GameObject eventChecker;

    private Vector3 startPoint;
    private Vector3 startPosition;

    void Start()
    {
        startPoint = transform.parent.position;
        startPosition = transform.position;
    }

    private void OnMouseDrag()
    {
        // Mouse position to world point
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;

        // Check for nearby connection points
        Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, .1f);
        foreach (Collider2D collider in colliders)
        {
            // Snap the wire
            UpdateWire(collider.transform.position);

            // Make sure this is not my own collider
            if (collider.gameObject != gameObject)
            {
                // Check if the wires are the same color
                if (transform.parent.name.Equals(collider.transform.parent.name))
                {
                    collider.GetComponent<Wire>()?.Done();

                    // Tell the event system to increase the count
                    eventChecker.GetComponent<EventCheck>().check();

                    Done();
                }

                return;
            }
        }

        // Update wire
        UpdateWire(newPosition);
    }

    void Done()
    {
        // Turn on the light
        lightOn.SetActive(true);

        // Destroy the script for this specific wire
        Destroy(this);
    }

    private void OnMouseUp()
    {
        // Reset wire position
        UpdateWire(startPosition);
    }

    private void UpdateWire(Vector3 newPosition)
    {
        // Update wire & position
        transform.position = newPosition;

        // Update wire angle
        Vector3 angle = newPosition - startPoint;
        transform.right = angle * transform.lossyScale.x;

        // Update wire scale
        float dist = Vector2.Distance(startPoint, newPosition);
        wireEnd.size = new Vector2(dist, wireEnd.size.y);
    }

    void Update()
    {
        
    }
}
