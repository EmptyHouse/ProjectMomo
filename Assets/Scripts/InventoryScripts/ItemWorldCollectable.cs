using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorldCollectable : MonoBehaviour {
    public SpriteRenderer spriteRenderer;
    public float hoverDistance = .3f;
    public float hoverSpeed = 5f;
    public float hoverAcceleration = 15f;
    public InventoryItem associatedItem;


    private void Start()
    {
        StartCoroutine(HoverItemInPlace());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerCharacterStats playerStats = collision.GetComponent<PlayerCharacterStats>();
        if (playerStats)
        {
            playerStats.inventoryManager.AddItemToInventory(associatedItem, 1);
            Destroy(this.gameObject);
        }
    }
    
    private IEnumerator HoverItemInPlace()
    {
        float direction = 1;
        Vector3 goalPosition = direction * Vector3.up * hoverDistance;
        Vector3 currentPosition = Vector3.zero;
        float speed = 0;
        while (this.gameObject)
        {
            if (direction * spriteRenderer.transform.localPosition.y > direction * goalPosition.y)
            {
                direction *= -1;
                goalPosition = direction * Vector3.up * hoverDistance;

            }
            speed = Mathf.MoveTowards(speed, direction * hoverSpeed, CustomTime.DeltaTime * hoverAcceleration);
            spriteRenderer.transform.localPosition += speed * CustomTime.DeltaTime * Vector3.up;
            yield return null;
        }
    }
}
