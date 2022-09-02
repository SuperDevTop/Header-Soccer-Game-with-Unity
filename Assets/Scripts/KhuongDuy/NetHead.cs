using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetHead : MonoBehaviour
{
    public GameObject _ball, _head;

    // Use this for initialization
    void Start()
    {
        //_ball = GameObject.FindGameObjectWithTag("Ball");
        //_head = GameObject.FindGameObjectWithTag("Head2");
    }

    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if (AIPlayer.instance.isJump == true)
            {
                if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
                {
                    SoundManager.Instance.ballKick.Play();
                }
                AIPlayer.instance.anim.SetBool(AIPlayer.instance.headShoot, true);

            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!GameController.Instance.Scored)
        {
            if (col.gameObject.tag == "Ball")
            {
                
                if (AIPlayer.instance.isJump == true)
                {

                    AIPlayer.instance.anim.SetBool(AIPlayer.instance.headShoot, true);
                    if (_ball.GetComponent<Rigidbody2D>().velocity.x != 0 || _ball.GetComponent<Rigidbody2D>().velocity.y != 0)
                        Instantiate(Ball.instance.eff_head, _ball.transform.position, Quaternion.identity);
                }
                if (AIPlayer.instance.canShoot == false && AIPlayer.instance.isJump == false)
                {
                    _ball.GetComponent<CircleCollider2D>().sharedMaterial.friction = 0.0f;
                    _ball.GetComponent<CircleCollider2D>().sharedMaterial.bounciness = 0.7f;
                }
                StartCoroutine(_ball.GetComponent<Ball>().WaitPhysic());
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {

            _ball.GetComponent<CircleCollider2D>().sharedMaterial.friction = 0.4f;
            _ball.GetComponent<CircleCollider2D>().sharedMaterial.bounciness = 0.7f;
        }

    }
}
