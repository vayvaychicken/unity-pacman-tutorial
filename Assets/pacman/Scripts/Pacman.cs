using UnityEngine;
using System.IO.Ports;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Pacman_Movement))]
public class Pacman : MonoBehaviour
{
    SerialPort sp = new SerialPort("/dev/cu.usbmodem14201", 9600);

    public AnimatedSprite deathSequence;
    public SpriteRenderer spriteRenderer { get; private set; }
    public new Collider2D collider { get; private set; }
    public Pacman_Movement movement { get; private set; }
    [SerializeField] public int timeout = 100;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        movement = GetComponent<Pacman_Movement>();
    }
    void Start() {

        sp.Open();
        sp.ReadTimeout = timeout;
    }

    private void Update()
    {
        // Set the new direction based on the current input
        /*if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            movement.SetDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            movement.SetDirection(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            movement.SetDirection(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            movement.SetDirection(Vector2.right);
        }*/

        if (sp.IsOpen){
            try{
                int input = sp.ReadByte();
                if (input == 1){
                    movement.SetDirection(Vector2.right);
                }
                else if (input == 2){
                    movement.SetDirection(Vector2.left);
                }
                else if (input == 3){
                    movement.SetDirection(Vector2.up);
                }
                else if (input == 4){
                    movement.SetDirection(Vector2.down);
                }
            }
            catch (System.Exception){
               Debug.Log("Error recieving Arduino input.");
            }
        } else { Debug.Log("closed"); }

        // Rotate pacman to face the movement direction
        float angle = Mathf.Atan2(movement.direction.y, movement.direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void ResetState()
    {
        enabled = true;
        spriteRenderer.enabled = true;
        collider.enabled = true;
        deathSequence.enabled = false;
        deathSequence.spriteRenderer.enabled = false;
        movement.ResetState();
        gameObject.SetActive(true);
    }

    public void DeathSequence()
    {
        enabled = false;
        spriteRenderer.enabled = false;
        collider.enabled = false;
        movement.enabled = false;
        deathSequence.enabled = true;
        deathSequence.spriteRenderer.enabled = true;
        deathSequence.Restart();
    }

}
