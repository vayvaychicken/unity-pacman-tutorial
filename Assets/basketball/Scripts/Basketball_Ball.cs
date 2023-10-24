using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basketball_Ball : MonoBehaviour
{
    public float yDistance = 15;
    public float time = 5;
    public float ballForce = 500;
    public float hoopHeight = 10;
    public float gravity = 32;

    Rigidbody2D rb;
    GameObject arrow;
    bool isShooting = false;

    float arrowAngle = 90;
    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        arrow = this.gameObject.transform.GetChild(0).gameObject;
        arrow.SetActive(false);
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            arrow.SetActive(true);
            arrowAngle = Mathf.PingPong(Time.time * 100, 90);
            arrow.transform.localEulerAngles = new Vector3(0, 0, arrowAngle);
        }
        if (Input.GetMouseButtonUp(0))
        {
            arrow.SetActive(false);
            ShootBall(arrowAngle);
        }
        if (Input.GetMouseButtonDown(2))
        {
            transform.position = startPosition;
        }
    }

    void ShootBall(float angle)
    {
        float xForce = Mathf.Cos(angle * Mathf.PI / 180) * ballForce;
        float yForce = Mathf.Sin(angle * Mathf.PI / 180) * ballForce;
        rb.AddForce(new Vector2(xForce, yForce));
    }
}