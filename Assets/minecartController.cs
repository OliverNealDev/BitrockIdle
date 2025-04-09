using System.Collections;
using UnityEngine;

public class minecartController : MonoBehaviour
{
    [SerializeField] private float originalScale = 2.5f;
    [SerializeField] private float hoverScale = 2.75f;
    [SerializeField] private int oreCapacity = 10;
    [SerializeField] private float speed = 1;

    [SerializeField] private Sprite emptyCart;
    [SerializeField] private Sprite semiFullCart;
    [SerializeField] private Sprite fullCart;
    
    private SpriteRenderer spriteRenderer;

    private int currentOreAmount = 0;
    private bool isParked = true;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Start()
    {
        spriteRenderer.sprite = emptyCart;
        
        transform.position = new Vector2(0, transform.position.y);
        transform.localScale = new Vector2(originalScale, originalScale);

        isParked = true;
    }

    void Arrive()
    {
        currentOreAmount = Random.Range(1, oreCapacity + 1);
        if (currentOreAmount == 0)
        {
            spriteRenderer.sprite = emptyCart;
        }
        else if (currentOreAmount < oreCapacity / 2)
        {
            spriteRenderer.sprite = semiFullCart;
        }
        else
        {
            spriteRenderer.sprite = fullCart;
        }
        
        transform.position = new Vector2(-11, transform.position.y);
        transform.localScale = new Vector2(originalScale, originalScale);
    }

    void Update()
    {
        if (!isParked && transform.position.x >= 0 && transform.position.x < 11)
        {
            transform.Translate(Time.deltaTime * speed, 0, 0);
            if (transform.position.x >= 11)
            {
                Invoke("Arrive", 2);
            }
        }
        else if (!isParked && transform.position.x >= -11 && transform.position.x < 0)
        {
            transform.Translate(Time.deltaTime * speed, 0, 0);
            if (transform.position.x >= 0)
            {
                transform.position = new Vector2(-0, transform.position.y);
                isParked = true;
            }
        }
    }

    void OnMouseOver()
    {
        transform.localScale = new Vector2(hoverScale, hoverScale);
        if (Input.GetMouseButtonDown(0) && isParked)
        {
            for (int i = 0; i < currentOreAmount; i++)
            {
                gameManager.Instance.PickAndAddOre();
            }
            currentOreAmount = 0;
            isParked = false;
            spriteRenderer.sprite = emptyCart;
        }
    }

    void OnMouseExit()
    {
        transform.localScale = new Vector2(originalScale, originalScale);
    }
}