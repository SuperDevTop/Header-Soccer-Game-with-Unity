using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public float angleOrientBall, dirPlayerTarget, dirAITarget;
    public static Ball instance;
    public GameObject _effect;
    private GameObject player, _AIPlayer;
    public Rigidbody2D rigid2D;
    public GameObject particSystem;
    public bool isUp, isDown;
    public GameObject Goal_Effect, orientBall;
    public Transform goalPos;
    public bool isMove, isCheckGroundPlayer, isCheckGroundAI, isColBody;
    public GameObject shadow, eff_head;

    // Use this for initialization
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        rigid2D.AddForce(new Vector2(0, 150));
        player = GameObject.FindGameObjectWithTag("Player");
        if (Menu.mode != (int)Menu.MODE.TRAINING)
        {
            _AIPlayer = GameObject.FindGameObjectWithTag("AIPlayer");
            AIPlayer.instance._ball = GameObject.FindGameObjectWithTag("Ball");
        }
        GetComponent<SpriteRenderer>().sprite = SelectTeam.instance.sp_ball[PlayerPrefs.GetInt("indexBall", 0)];
        rigid2D.angularDrag = 1f;
    }
    private void FixedUpdate()
    {

    }
    private void Update()
    {
        if (rigid2D.velocity.y >= 0)
        {
            rigid2D.drag = 0.1f;
            rigid2D.gravityScale = 1f;
        }
        else
        {
            rigid2D.drag = 0.00f;
            rigid2D.gravityScale = 1.25f;
        }
        shadow.transform.position = new Vector2(transform.position.x, -2.9f);
        if (transform.position.y >= -0.5)
        {
            shadow.transform.localScale = new Vector2(0, 0);
        }
        else
        {
            shadow.transform.localScale = new Vector2(-transform.position.y * 0.035f, transform.position.y * 0.035f);
        }
        if (Menu.mode == (int)Menu.MODE.TRAINING)
        {

            dirPlayerTarget = Mathf.Abs(transform.position.x - Training.instance.target_left.transform.position.x);

            //if (Training.instance.canShoot == true)
            //{
            //    angleOrientBall = Mathf.Atan2(3f - Mathf.Abs(player.transform.position.x - transform.position.x), 0.2f * Training.instance._shoot) * Mathf.Rad2Deg;
            //    GameObject _orientBall = GameObject.FindGameObjectWithTag("OrientBall");
            //    if (player.transform.position.x > transform.position.x)
            //    {

            //        _orientBall.transform.rotation = Quaternion.Euler(0, 0, 90 - angleOrientBall);
            //    }
            //    else
            //    {

            //        _orientBall.transform.rotation = Quaternion.Euler(0, 0, -90 + angleOrientBall);
            //    }
            //    _orientBall.transform.position = transform.position;
            //}
        }
        else
        {
            if (Loadding.leftOrRight == 0)
            {

                dirAITarget = Mathf.Abs(transform.position.x - _AIPlayer.transform.position.x);
                dirPlayerTarget = Mathf.Abs(transform.position.x - GameController.Instance.targetLeft.transform.position.x);
            }
            else
            {
                dirAITarget = Mathf.Abs(transform.position.x - _AIPlayer.transform.position.x);
                dirPlayerTarget = Mathf.Abs(transform.position.x - GameController.Instance.targetRight.transform.position.x);
            }
            //if (Character2DController.instance.canShoot == true)
            //{
            //    angleOrientBall = Mathf.Atan2(3f - Mathf.Abs(player.transform.position.x - transform.position.x), 0.2f * Character2DController.instance._shoot) * Mathf.Rad2Deg;
            //    GameObject _orientBall = GameObject.FindGameObjectWithTag("OrientBall");
            //    if (player.transform.position.x > transform.position.x)
            //    {

            //        _orientBall.transform.rotation = Quaternion.Euler(0, 0, 90 - angleOrientBall);
            //    }
            //    else
            //    {

            //        _orientBall.transform.rotation = Quaternion.Euler(0, 0, -90 + angleOrientBall);
            //    }
            //    _orientBall.transform.position = transform.position;
            //}
        }
    }

    public IEnumerator WaitPhysic()
    {
        yield return new WaitForSeconds(0.02f);
        GetComponent<CircleCollider2D>().sharedMaterial.friction = 0.4f;
        GetComponent<CircleCollider2D>().sharedMaterial.bounciness = 0.7f;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "UpCol")
        {
            if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
            {
                SoundManager.Instance.ballHit.Play();
            }
            GetComponent<CircleCollider2D>().sharedMaterial.friction = 0.0f;
            GetComponent<CircleCollider2D>().sharedMaterial.bounciness = 0.7f;
            StartCoroutine(WaitPhysic());
        }

        if (col.gameObject.tag == "Wall")
        {
            if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
            {
                SoundManager.Instance.ballHit.Play();
            }
        }


        if (col.gameObject.tag == "Body2" && col.gameObject.tag == "Body1")
        {
            isColBody = true;
            rigid2D.velocity = new Vector2(0, 12);
        }

    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Body2" || col.gameObject.tag == "Body1")
        {
            isColBody = false;
        }
        if (col.gameObject.tag == "UpCol")
        {
            if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
            {
                SoundManager.Instance.ballHit.Play();
            }
            GetComponent<CircleCollider2D>().sharedMaterial.friction = 0.4f;
            GetComponent<CircleCollider2D>().sharedMaterial.bounciness = 0.7f;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "CheckHead")
        {
            AIPlayer.instance.canHead = true;
        }

        if (col.tag == "BehindCol")
        {
            rigid2D.velocity = new Vector2(0, 0);
            if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
            {
                SoundManager.Instance.ballHit.Play();
            }
        }
        if (Menu.mode == (int)Menu.MODE.TRAINING)
        {

            if (col.tag == "RightNet")
            {
                if (Training.instance.isPerfect == false && Training.instance.level_training == 6)
                {
                    if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
                    {
                        SoundManager.Instance.goal1.Play();
                        SoundManager.Instance.goal2.Play();
                    }
                    Training.instance.isPerfect = true;
                    Instantiate(Goal_Effect, goalPos.position, Quaternion.identity);
                    Training.instance.level_training++;
                    StartCoroutine(Training.instance.LoadPanelTraining());

                }
                else if (Training.instance.isPerfect == false && (Training.instance.level_training == 4 || Training.instance.level_training == 5))
                {
                    Training.instance.isPerfect = true;
                    StartCoroutine(Training.instance.LoadPanelTraining());
                }
                if (Training.instance.level_training == 7 && Training.instance.isPerfect == false)
                {
                    if (Training.instance.is_headshot_training == true)
                    {
                        Training.instance.isPerfect = true;
                        //Training.instance.level_training++;
                        //StartCoroutine(Training.instance.LoadPanelTraining());
                        Menu.mode = (int)Menu.MODE.FRIENDMATCH;
                        PlayerPrefs.SetInt(StringUtils.value_team_player_friendmatch, 1);
                        PlayerPrefs.SetInt(StringUtils.value_team_ai_friendmatch, 2);
                        SceneManager.LoadScene("Loading");
                    }
                }

            }
            if (col.tag == "LeftNet")
            {
                if (Training.instance.isPerfect == false && (Training.instance.level_training == 4 || Training.instance.level_training == 5))
                {
                    Training.instance.isPerfect = true;
                    Instantiate(Goal_Effect, goalPos.position, Quaternion.identity);
                    if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
                    {
                        SoundManager.Instance.goal1.Play();
                        SoundManager.Instance.goal2.Play();
                    }
                    Training.instance.level_training++;
                    StartCoroutine(Training.instance.LoadPanelTraining());
                }
                else if (Training.instance.isPerfect == false && (Training.instance.level_training == 6 || Training.instance.level_training == 8))
                {
                    Training.instance.isPerfect = true;
                    StartCoroutine(Training.instance.LoadPanelTraining());
                }

            }

            if (col.gameObject.tag == "Player")
            {
                Training.instance.canShoot = true;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                if (Training.instance.level_training <= 3 && Training.instance.isPerfect == false)
                {
                    Training.instance.isPerfect = true;
                    Training.instance.level_training++;
                    StartCoroutine(Training.instance.LoadPanelTraining());
                }
            }

        }
        else
        {
            if (!GameController.Instance.Scored && !GameController.Instance.EndMatch)
            {
                if (col.tag == "RightNet")
                {
                    Instantiate(Goal_Effect, goalPos.position, Quaternion.identity);

                    if (Loadding.leftOrRight == 0)
                    {
                        GameController.Instance.ScoredAgainst(false);
                    }
                    else
                    {
                        GameController.Instance.ScoredAgainst(true);
                    }

                    if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
                    {
                        SoundManager.Instance.goal1.Play();
                        SoundManager.Instance.goal2.Play();
                    }

                }
                if (col.tag == "LeftNet")
                {

                    Instantiate(Goal_Effect, goalPos.position, Quaternion.identity);

                    if (Loadding.leftOrRight == 0)
                    {
                        GameController.Instance.ScoredAgainst(true);
                    }
                    else
                    {
                        GameController.Instance.ScoredAgainst(false);
                    }

                    if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
                    {
                        SoundManager.Instance.goal1.Play();
                        SoundManager.Instance.goal2.Play();
                    }

                }

                if (col.gameObject.tag == "Player")
                {
                    Character2DController.instance.canShoot = true;
                }

                if (col.gameObject.tag == "Shoot2")
                {

                    _AIPlayer.GetComponent<AIPlayer>().canShoot = true;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {

        if (col.tag == "CheckHead")
        {
            AIPlayer.instance.canHead = false;
        }


        if (col.gameObject.tag == "Player")
        {
            if (Menu.mode == (int)Menu.MODE.TRAINING)
            {
                Training.instance.canShoot = false;
            }
            else
            {
                Character2DController.instance.canShoot = false;
            }

            //GameObject[] _orientBall = GameObject.FindGameObjectsWithTag("OrientBall");
            //for (int i = 0; i < _orientBall.Length; i++)
            //{
            //    Destroy(_orientBall[i]);
            //}
        }

        if (col.gameObject.tag == "Shoot2")
        {
            _AIPlayer.GetComponent<AIPlayer>().canShoot = false;
        }
    }
}
