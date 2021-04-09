using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;

    public AudioClip jumpVoice;
    public AudioClip jumpSFX;
    public AudioClip footStep1;
    public AudioClip footStep2;
    public AudioClip footStep3;
    public AudioClip footStep4;

    public float movementSpeed = 6f;
    public float jumpForce = 10f;

    private float horizontalMovement;
    private Rigidbody2D body;
    private BoxCollider2D collider;
    AudioSource audioSource;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Save player data before entering a new scene
        SavePlayer();

        // Change is needed here, since the other collider can be NPCs
        SceneManager.LoadScene(other.name);
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontalMovement * movementSpeed, body.velocity.y);

        if(IsGrounded() && body.velocity.x != 0f && !audioSource.isPlaying)
        {
            playFootSteps();
        }
    }

    private void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.PlayOneShot(jumpVoice, 1.0f);
            audioSource.PlayOneShot(jumpSFX, 1.0f);
            body.velocity = Vector2.up * jumpForce;
        }
    }

    private void playFootSteps()
    {
        int clipChoice = Random.Range(0, 4);
        switch (clipChoice)
        {
            case 0:
                audioSource.PlayOneShot(footStep1, 1.0f);
                break;

            case 1:
                audioSource.PlayOneShot(footStep2, 1.0f);
                break;

            case 2:
                audioSource.PlayOneShot(footStep3, 1.0f);
                break;

            case 3:
                audioSource.PlayOneShot(footStep4, 1.0f);
                break;

            default:
                audioSource.PlayOneShot(footStep1, 1.0f);
                break;
        }
    }

    private bool IsGrounded()
    {
        Color[] rayColor = new Color[3];
        float heightOffset = 0.05f;

        RaycastHit2D hitMid = Physics2D.Raycast(collider.bounds.center, Vector2.down, collider.bounds.extents.y + heightOffset, platformLayerMask);
        RaycastHit2D hitLeft = Physics2D.Raycast(new Vector2(collider.bounds.center.x - collider.bounds.extents.x, collider.bounds.center.y), Vector2.down, collider.bounds.extents.y + heightOffset, platformLayerMask);
        RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(collider.bounds.center.x + collider.bounds.extents.x, collider.bounds.center.y), Vector2.down, collider.bounds.extents.y + heightOffset, platformLayerMask);

        if(hitMid.collider != null) 
            rayColor[0] = Color.green;
        else 
            rayColor[0] = Color.red;

        if (hitLeft.collider != null)
            rayColor[1] = Color.green;
        else
            rayColor[1] = Color.red;

        if (hitRight.collider != null)
            rayColor[2] = Color.green;
        else
            rayColor[2] = Color.red;

        Debug.DrawRay(collider.bounds.center, Vector2.down * (collider.bounds.extents.y + heightOffset), rayColor[0]);
        Debug.DrawRay(new Vector3(collider.bounds.center.x - collider.bounds.extents.x, collider.bounds.center.y), Vector2.down * (collider.bounds.extents.y + heightOffset), rayColor[1]);
        Debug.DrawRay(new Vector3(collider.bounds.center.x + collider.bounds.extents.x, collider.bounds.center.y), Vector2.down * (collider.bounds.extents.y + heightOffset), rayColor[2]);

        if(hitMid.collider != null || hitLeft.collider != null || hitRight.collider != null)
            return true;

        return false;
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }
}
