using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameObject gameManager;
    bool deathAnim = false;
    float startingScaleY;
    float auxTimer = 0;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        startingScaleY = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (deathAnim)
        {
            auxTimer += 1 * Time.deltaTime;

            if (auxTimer > 0.5f)
            {
                gameManager.GetComponent<GameManager>().isAlive = false;
            }
        }
    }

    private float ScalePercentage(float maxTime, float currentTime)
    {
        float x = 1 - (currentTime / maxTime);
        return 1 - (currentTime / maxTime);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "hazard")
        {
            deathAnim = true;
            animator.SetBool("Death", true);
        }

        if (collision.gameObject.tag == "Victory")
        {
            gameManager.GetComponent<GameManager>().Victory();
        }
    }
}
