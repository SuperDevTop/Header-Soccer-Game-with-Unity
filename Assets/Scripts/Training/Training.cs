using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Training : MonoBehaviour
{
    // Start is called before the first frame update
    public static Training instance;
    public GameObject target_left, target_right;

    public GameObject btnLeft, btnRight, btnJump, btnShoot, btnLowShoot;
    private float horizontalAxis = 0.0f;

    public float _velocity;

    private Rigidbody2D _rigidbody;

    public float _jumpForce;

    public bool canShoot, isJump;

    public GameObject _ball;

    public Transform checkGround;

    public bool grounded, isHeadShoot;

    public LayerMask ground_layers;


    public Animator _animator;
    public float _shoot, addForceHead;
    public int
        celebrateHash,
    headShootHash;

    public Animator Anim { get { return _animator; } }
    public int moveHash;

    public int CelebrateHash { get { return celebrateHash; } }
    public bool isMove, isShoot;
    public float x_dir_Head, x_dir_Shoot, y_dir_Shoot;
    public bool isPerfect = false;
    public int level_training = 0;
    public GameObject load_panel_black;
    public Text txt_training;
    public bool is_headshot_training = false;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Use this for initialization
    void Start()
    {
        Menu.isTraining = true;
        int number = PlayerPrefs.GetInt(StringUtils.str_number_training, 0);
        if (number == 0)
        {
            PlayerPrefs.SetInt(StringUtils.str_number_training, 1);
        }
        level_training = 0;
        isPerfect = false;
        is_headshot_training = false;
        _rigidbody = GetComponent<Rigidbody2D>();
        canShoot = false;
        grounded = false;
        celebrateHash = Animator.StringToHash("Celebrate");
        headShootHash = Animator.StringToHash("HeadShoot");
        moveHash = Animator.StringToHash("Move");

        float _sizePlayer = 1.25f;
        x_dir_Shoot = 0.95f;
        y_dir_Shoot = 7.5f;
        x_dir_Head = 0.02f;
        _velocity = 420;
        _jumpForce = 10.25f;

        _shoot = 1.3f * Mathf.Sqrt(x_dir_Shoot * x_dir_Shoot +
             y_dir_Shoot * y_dir_Shoot);
        addForceHead = x_dir_Head;

        transform.localScale = new Vector2(_sizePlayer, _sizePlayer);
        transform.position = new Vector2(5f, -2.908148f);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        SetTraining();
    }

    private void Update()
    {
        //horizontalAxis = Input.GetAxis("Horizontal");

        if (!grounded)
        {
            isJump = true;
            _animator.SetBool(moveHash, false);
        }
        else
        {
            isJump = false;
            _animator.SetBool(headShootHash, false);
        }

        if (grounded == true)
        {
            _rigidbody.gravityScale = 2f;
        }

        else
        {
            if (_rigidbody.velocity.y >= 0)
            {
                _rigidbody.gravityScale = 4f;
            }
            else
            {
                _rigidbody.gravityScale = 5f;
            }

        }
    }
    void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(horizontalAxis * Time.deltaTime * _velocity, _rigidbody.velocity.y);
        grounded = Physics2D.OverlapCircle(checkGround.position, 0.25f, ground_layers);
    }

    public void SetTraining()
    {
        switch (level_training)
        {
            case 0:
                txt_training.text = "Move to the ball.";
                btnLeft.transform.parent.gameObject.SetActive(true);
                btnRight.transform.parent.gameObject.SetActive(false);
                btnJump.transform.parent.gameObject.SetActive(false);
                btnShoot.transform.parent.gameObject.SetActive(false);
                _ball.transform.position = new Vector2(0, -1);
                _ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                _ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
                break;
            case 1:
                txt_training.text = "Move to the ball.";
                transform.position = new Vector2(5f, -2.908148f);
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                _ball.transform.position = new Vector2(-5, -1);
                _ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                btnLeft.transform.parent.gameObject.SetActive(true);
                btnRight.transform.parent.gameObject.SetActive(false);
                btnJump.transform.parent.gameObject.SetActive(false);
                btnShoot.transform.parent.gameObject.SetActive(false);
                _ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                _ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
                break;
            case 2:
                txt_training.text = "Move to the ball.";
                transform.position = new Vector2(-5f, -2.908148f);
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                _ball.transform.position = new Vector2(0, -1);
                _ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                btnLeft.transform.parent.gameObject.SetActive(false);
                btnRight.transform.parent.gameObject.SetActive(true);
                btnJump.transform.parent.gameObject.SetActive(false);
                btnShoot.transform.parent.gameObject.SetActive(false);
                _ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                _ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
                break;
            case 3:
                txt_training.text = "Move to the ball.";
                transform.position = new Vector2(-5f, -2.908148f);
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                _ball.transform.position = new Vector2(5, -1);
                _ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                btnLeft.transform.parent.gameObject.SetActive(false);
                btnRight.transform.parent.gameObject.SetActive(true);
                btnJump.transform.parent.gameObject.SetActive(false);
                btnShoot.transform.parent.gameObject.SetActive(false);
                _ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                _ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
                break;
            case 4:
                txt_training.text = "Shot on the goal.";
                transform.position = new Vector2(5f, -2.908148f);
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                _ball.transform.position = new Vector2(3.5f, -1);
                _ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                btnLeft.transform.parent.gameObject.SetActive(false);
                btnRight.transform.parent.gameObject.SetActive(false);
                btnJump.transform.parent.gameObject.SetActive(false);
                btnShoot.transform.parent.gameObject.SetActive(true);
                _ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                _ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
                break;
            case 5:
                txt_training.text = "Shot on the goal.";
                transform.position = new Vector2(5f, -2.908148f);
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                _ball.transform.position = new Vector2(0, -1);
                _ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                btnLeft.transform.parent.gameObject.SetActive(true);
                btnRight.transform.parent.gameObject.SetActive(true);
                btnJump.transform.parent.gameObject.SetActive(false);
                btnShoot.transform.parent.gameObject.SetActive(true);
                _ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                _ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
                break;
            case 6:
                txt_training.text = "Shot on the goal.";
                transform.position = new Vector2(-5f, -2.908148f);
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                _ball.transform.position = new Vector2(5, -1);
                _ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                _ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                _ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
                btnLeft.transform.parent.gameObject.SetActive(true);
                btnRight.transform.parent.gameObject.SetActive(true);
                btnJump.transform.parent.gameObject.SetActive(false);
                btnShoot.transform.parent.gameObject.SetActive(true);
                break;
            case 7:

                txt_training.text = "To head the ball.";
                transform.position = new Vector2(-5f, -2.908148f);
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                _ball.transform.position = new Vector2(-4.5f, 1);
                _ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                btnLeft.transform.parent.gameObject.SetActive(false);
                btnRight.transform.parent.gameObject.SetActive(false);
                btnJump.transform.parent.gameObject.SetActive(true);
                btnShoot.transform.parent.gameObject.SetActive(false);
                break;
            //case 8:
            //    txt_training.text = "To head the ball.";
            //    transform.position = new Vector2(-5f, -2.908148f);
            //    transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            //    _ball.transform.position = new Vector2(-4f, 3);
            //    _ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            //    btnLeft.transform.parent.gameObject.SetActive(true);
            //    btnRight.transform.parent.gameObject.SetActive(true);
            //    btnJump.transform.parent.gameObject.SetActive(true);
            //    btnShoot.transform.parent.gameObject.SetActive(false);
            //    StartCoroutine(ResetHeadBall());
            //    break;
            case 8:
                Menu.mode = (int)Menu.MODE.FRIENDMATCH;
                PlayerPrefs.SetInt(StringUtils.value_team_player_friendmatch, 1);
                PlayerPrefs.SetInt(StringUtils.value_team_ai_friendmatch, 2);
                SceneManager.LoadScene("Loading");
                break;
        }
    }

    public void Move(int value)
    {

        if (value == -1 || value == 1)
        {
            isMove = true;
        }
        if (isJump == false)
        {
            _animator.SetBool(moveHash, true);

        }
        horizontalAxis = value;
        if (value == 1)
        {
            btnRight.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1);
            btnRight.GetComponent<Image>().CrossFadeAlpha(0.4f, 0.1f, true);
        }
        else
        {
            btnLeft.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1);
            btnLeft.GetComponent<Image>().CrossFadeAlpha(0.4f, 0.1f, true);
        }
    }


    public void StopMoveLeft()
    {
        isMove = false;
        btnLeft.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1);
        _animator.SetBool(moveHash, false);
        horizontalAxis = 0.0f;
        btnLeft.GetComponent<Image>().CrossFadeAlpha(1f, 0.1f, true);

    }
    public void StopMoveRigh()
    {
        isMove = false;
        _animator.SetBool(moveHash, false);
        horizontalAxis = 0.0f;
        btnRight.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1);
        btnRight.GetComponent<Image>().CrossFadeAlpha(1f, 0.1f, true);
    }

    public void StopJump()
    {
        btnJump.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1);
        btnJump.GetComponent<Image>().CrossFadeAlpha(1f, 0.1f, true);
    }
    public void StopShoot()
    {
        btnShoot.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1);
        btnShoot.GetComponent<Image>().CrossFadeAlpha(1f, 0.1f, true);
        isShoot = false;

    }

    public void Jump()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.jump.Play();
        }
        isJump = true;
        btnJump.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1);
        btnJump.GetComponent<Image>().CrossFadeAlpha(0.4f, 0.1f, true);

        if (grounded)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 1.75f * _jumpForce);

        }
    }

    public void Shoot()
    {

        btnShoot.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1);
        btnShoot.GetComponent<Image>().CrossFadeAlpha(0.4f, 0.1f, true);

        _animator.SetTrigger("Shoot");
        if (canShoot)
        {
            isShoot = true;
            if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
            {
                SoundManager.Instance.ballKick.Play();
            }
            _animator.SetBool(moveHash, false);
            _ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            if (transform.position.x > _ball.transform.position.x)
            {
                _ball.GetComponent<Rigidbody2D>().AddForce(
                    new Vector2(-_shoot, _shoot * Mathf.Tan(_ball.GetComponent<Ball>().angleOrientBall * Mathf.Deg2Rad)),
                        ForceMode2D.Impulse);
            }
            else
            {
                _ball.GetComponent<Rigidbody2D>().AddForce(
                    new Vector2(_shoot, _shoot * Mathf.Tan(_ball.GetComponent<Ball>().angleOrientBall * Mathf.Deg2Rad)),
                        ForceMode2D.Impulse);
            }
        }
    }

    void OnDisable()
    {
        _animator.SetBool(headShootHash, false);
    }
    public IEnumerator LoadPanelTraining()
    {
        yield return new WaitForSeconds(0.5f);
        load_panel_black.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SetTraining();
        isPerfect = false;
        is_headshot_training = false;
        load_panel_black.SetActive(false);
    }

    public IEnumerator ResetHeadBall()
    {
        yield return new WaitForSeconds(2f);
        if (is_headshot_training == false)
        {
            StartCoroutine(LoadPanelTraining());
        }
    }
}
