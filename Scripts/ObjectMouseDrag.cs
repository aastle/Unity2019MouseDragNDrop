using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMouseDrag : MonoBehaviour
{
    [SerializeField]
    private Transform targetPlace;

    [SerializeField]
    private Transform nameFrame;

    [SerializeField]
    public AudioClip missClip;

    [SerializeField]
    public AudioClip hitClip;


    private Vector2 initialPosition;
    private float deltax, deltay;

    [SerializeField]
    public bool locked;

    private Vector2 mousePosition;
    private AudioSource soundSource;

    private bool collided;

    BoxCollider2D boxCollider;

    Rigidbody2D rigidBody;


    private void Awake()
    {
       
    }



    // Start is called before the first frame update
    void Start()
    {
        // Add a 2D box collider with the isTrigger property set to true so we don't forget to do it
          boxCollider =  gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
          boxCollider.isTrigger = true;

        // Add a 2D Rigid Body with the Gravity Scale property set to zero so we don't forget to do it
        rigidBody = gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
        rigidBody.gravityScale = 0f;


        initialPosition = transform.position;

        soundSource = gameObject.AddComponent<AudioSource>();

        locked = false;

        collided = false;
      
    }

    private void OnMouseDown()
    {
        if (!locked)  // 
        {

            deltax = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            deltay = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;



        }
    }
    private void OnMouseDrag()
    {
        if (!locked)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x - deltax, mousePosition.y - deltay);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("This object collided " + col.gameObject.name + " : " + gameObject.name + " : " + Time.time.ToString());

        if (gameObject.CompareTag(col.gameObject.tag))
        { 
            collided = true; 
        }
    }



    private void OnMouseUp()
    {
        if (!locked) // && 
        {
           // if (Mathf.Abs(transform.position.x - targetPlace.position.x) <= 1f &&
           //        Mathf.Abs(transform.position.y - targetPlace.position.y) <= 1f  && targetPlace.tag == gameObject.tag)
            if(collided)
            {
                transform.position = new Vector2(nameFrame.position.x, nameFrame.position.y);
                locked = true;
                // Debug.Log("orange locked = " + locked.ToString());
                soundSource.PlayOneShot(hitClip, 1f);

                collided = false;

                
            }
            else
            {
                transform.position = new Vector2(initialPosition.x, initialPosition.y);
                soundSource.PlayOneShot(missClip, 1f);
                locked = false;
                collided = false;
            }
        } 
       
    }
    // Update is called once per frame
    void Update()
    {



    }
}
