using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadPlayer : MonoBehaviour
{

    public GameObject _ball, _player;
    // Use this for initialization
    void Start()
    {
        //_ball = GameObject.FindGameObjectWithTag("Ball");
        _player = GameObject.FindGameObjectWithTag("Player");
    }


    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if (Character2DController.instance.isJump == true)
            {
                if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
                {
                    SoundManager.Instance.ballKick.Play();
                }
                Character2DController.instance._animator.SetBool(Character2DController.instance.headShootHash, true);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (Menu.mode == (int)Menu.MODE.TRAINING)
        {
            if (col.gameObject.tag == "Ball")
            {

                if (Training.instance.isJump == true)
                {
                    Training.instance._animator.SetBool(Training.instance.headShootHash, true);
                    _ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                    Training.instance.is_headshot_training = true;
                    if (Training.instance.level_training == 7 && Training.instance.isPerfect == false)
                    {
                        Training.instance.isPerfect = true;
                        Training.instance.level_training++;
                        StartCoroutine(Training.instance.LoadPanelTraining());
                    }
                }

            }
        }
        else
        {
            if (!GameController.Instance.Scored)
            {
                if (col.gameObject.tag == "Ball")
                {
                    if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
                    {
                        SoundManager.Instance.ballKick.Play();
                    }

                    StartCoroutine(_ball.GetComponent<Ball>().WaitPhysic());
                    if (Character2DController.instance.isJump == true)
                    {
                        Character2DController.instance._animator.SetBool(Character2DController.instance.headShootHash, true);

                        if (Loadding.leftOrRight == 0)
                        {
                            if (_ball.transform.position.x <= _player.transform.position.x)
                                _ball.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 5);
                        }
                        else
                        {
                            if (_ball.transform.position.x >= _player.transform.position.x)
                                _ball.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 5);
                        }
                        if (_ball.GetComponent<Rigidbody2D>().velocity.x != 0 || _ball.GetComponent<Rigidbody2D>().velocity.y != 0)
                            Instantiate(Ball.instance.eff_head, _ball.transform.position, Quaternion.identity);
                    }
                    else
                    {
                        _ball.GetComponent<CircleCollider2D>().sharedMaterial.friction = 0.0f;
                        _ball.GetComponent<CircleCollider2D>().sharedMaterial.bounciness = 0.7f;
                    }
                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
            {
                SoundManager.Instance.ballKick.Play();
            }
            _ball.GetComponent<CircleCollider2D>().sharedMaterial.friction = 0.4f;
            _ball.GetComponent<CircleCollider2D>().sharedMaterial.bounciness = 0.7f;
        }

    }
}