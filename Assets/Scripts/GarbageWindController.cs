using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageWindController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float WindForce = 0.01f;
    public bool invert = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (invert)
        {
            WindForce = Random.Range(-0.048f, -0.017f);//random to left force
        }
        else
        {
            WindForce = Random.Range(0.017f, 0.048f);//random to left force
        }
        StartCoroutine(RandomWindForce(Random.Range(0.8f, 1.4f)));//random delay to wind-up

    }

    IEnumerator RandomWindForce(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (invert)
        {
            rb.AddForce(transform.up * Random.Range(-2.9f, -1.8f), ForceMode2D.Impulse);
            StartCoroutine(RandomWindForce(Random.Range(0.8f, 1.4f)));
        }
        else
        {
            rb.AddForce(transform.up * Random.Range(1.8f, 2.9f), ForceMode2D.Impulse);
            StartCoroutine(RandomWindForce(Random.Range(0.8f, 1.4f)));
        }

    }

    void FixedUpdate()
    {
        rb.AddForce(Vector2.left * WindForce, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 9)// 9 is player
        {
            GameObject.Find("GameController").GetComponent<GameController>().AddPoints(1);
            if(this.gameObject.layer == 10)
            {
                GameObject.Find("GameController").GetComponent<GameController>().AddTime(1);
            }
        }
        if(col.gameObject.layer != 11)
        {
            Destroy(this.gameObject);
        }

    }
}
