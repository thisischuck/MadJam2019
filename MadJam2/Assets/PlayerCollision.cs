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
            float scale = ScalePercentage(0.5f, auxTimer);
            transform.localScale = new Vector3(transform.localScale.x, startingScaleY * scale, transform.localScale.z);

            if (transform.localScale.y <= 0.01f || scale <= 0.01f)
            {
                gameManager.GetComponent<GameManager>().isAlive = false;
            }

            animator.SetBool("Death", true);
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
        }
    }
}
