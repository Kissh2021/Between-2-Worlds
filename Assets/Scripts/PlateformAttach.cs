using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Platform/Plateform Attacher")]
[RequireComponent(typeof(Rigidbody2D))]
public class PlateformAttach : MonoBehaviour
{
    private List<GameObject> objectsOn = new List<GameObject>();

    private Vector2 previousPostion;


    private void Start()
    {
        previousPostion = transform.position;
        if (!GetComponent<Rigidbody2D>().isKinematic)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }
    }


    // Update is called once per frame
    void Update()
    {
        foreach (var go in objectsOn)
        {
            if (go.TryGetComponent<Rigidbody2D>(out var rb))
            {
                if (rb.velocity.magnitude > 0.1f)
                {
                    continue;
                }
            }
            go.transform.position = (Vector2)go.transform.position + ((Vector2)transform.position - previousPostion);
        }
        
        previousPostion = transform.position;
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!objectsOn.Contains(col.gameObject))
        {
            objectsOn.Add(col.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (objectsOn.Contains(other.gameObject))
        {
            objectsOn.Remove(other.gameObject);
        }
    }
}
