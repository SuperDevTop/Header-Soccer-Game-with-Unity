using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    public static AIPlayer instance;
    public GameObject _ball;
    public int topAI;
    private GameObject _player, _AI;
    public Rigidbody2D _rigidbody, rig2d_ball;
    public float _velocity;
    public float rangeOfDefense;
    public Transform defensePos;
    public bool canShoot;
    public Transform checkGround, checkHead;
    public bool grounded, canHead, isJump;
    public LayerMask ground_layers, ball_layer;
    public float _jumpForce;
    public Animator anim;
    public int CelebrateHash { get; set; }
    public int moveHash, headShoot;
    public int teamAI;
    public float x_dir_Head, x_dir_Shoot, y_dir_Shoot;
    public float a_velocity, a_jum, _shoot, h_attack, angle_shot;
    public GameObject eff_x2_ai, eff_ice_block, eff_explo_ice;
    public bool isIce, isX2;
    public static int number_x2, time, rd_time_x2, rd_time_ice, number_ice;
    public GameObject shadow;
    public float time_fire_ball, time_jump;
    public static int random_item;
    public float t_jump = 0;
    float _sizePlayer;
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

        time = 0;
        time_fire_ball = 0;
        time_jump = 4;
        rd_time_x2 = Random.Range(5, 20);
        rd_time_ice = Random.Range(20, 35);
        random_item = Random.Range(0, 4);
        if (Menu.mode == (int)Menu.MODE.SIRVIVAL && PlayerPrefs.GetInt(StringUtils.match_survival) < 5)
        {
            random_item = 0;
        }
        number_ice = 1;
        isIce = false;
        CelebrateHash = Animator.StringToHash("Celebrate");
        moveHash = Animator.StringToHash("Move");
        headShoot = Animator.StringToHash("HeadShoot");
        _ball = GameObject.FindGameObjectWithTag("Ball");
        _AI = GameObject.FindGameObjectWithTag("AIPlayer");
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
        number_x2 = 1;
        if (Menu.mode == (int)Menu.MODE.WORLDCUP)
        {
            teamAI = PlayerPrefs.GetInt(StringUtils.value_team_player_wc);
        }
        else
        {
            teamAI = PlayerPrefs.GetInt(StringUtils.value_team_player_friendmatch);

        }
        StartCoroutine(waitInfoAI());




    }
    public void X2AI()
    {
        if (random_item == 1 || random_item == 3)
        {
            float _sizePlayer = 2f;
            transform.localScale = new Vector2(-_sizePlayer, _sizePlayer);
            eff_x2_ai.SetActive(true);
            isX2 = true;
            StartCoroutine(WaitX2AI());
        }

    }
    IEnumerator WaitX2AI()
    {
        yield return new WaitForSeconds(5);
        isX2 = false;
        transform.localScale = new Vector2(-_sizePlayer, _sizePlayer);
    }

    public IEnumerator waitIce()
    {
        if (random_item == 2 || random_item == 3)
        {
            Character2DController.instance.isIce = true;
            Character2DController.instance.Anim.SetBool(Character2DController.instance.CelebrateHash, false);
            Character2DController.instance.Anim.SetBool(Character2DController.instance.moveHash, true);
            Character2DController.instance.Anim.enabled = false;
            yield return new WaitForSeconds(0.25f);
            Character2DController.instance.eff_ice_block.SetActive(true);
            yield return new WaitForSeconds(4f);
            Character2DController.instance.Anim.enabled = true;
            Character2DController.instance.eff_ice_block.SetActive(false);

            if (Character2DController.instance.eff_explo_ice != null)
            {
                Character2DController.instance.eff_explo_ice.SetActive(true);
                Character2DController.instance.eff_explo_ice.transform.SetParent(transform.parent);
            }
            yield return new WaitForSeconds(0.5f);
            Character2DController.instance.isIce = false;
        }
    }
    IEnumerator waitInfoAI()
    {
        yield return new WaitForSeconds(0.1f);
        switch (Loadding.teamAI)
        {
            case 1:

                _velocity = 310;
                _jumpForce = 9f;
                h_attack = 0.50f;
                _sizePlayer = 1.1f;
                break;
            case 2:

                _velocity = 309;
                _jumpForce = 9.05f;
                h_attack = 0.48f;
                _sizePlayer = 1.12f;
                break;
            case 3:

                _velocity = 311;
                _jumpForce = 9.02f;
                h_attack = 0.47f;
                _sizePlayer = 1.11f;
                break;
            case 4:

                _velocity = 306;
                _jumpForce = 9.01f;
                h_attack = 0.460f;
                _sizePlayer = 1.1f;
                break;
            case 5:

                _velocity = 307;
                _jumpForce = 9.04f;
                h_attack = 0.50f;
                _sizePlayer = 1.1f;
                break;
            case 6:

                _velocity = 312;
                _jumpForce = 9.07f;
                h_attack = 0.50f;
                _sizePlayer = 1.14f;
                break;
            case 7:

                _velocity = 316f;
                _jumpForce = 9.05f;
                h_attack = 0.50f;
                _sizePlayer = 1.16f;
                break;
            case 8:

                _velocity = 312;
                _jumpForce = 9.15f;
                h_attack = 0.50f;
                _sizePlayer = 1.11f;
                break;
            case 9:

                _velocity = 315;
                _jumpForce = 9.1f;
                h_attack = 0.50f;
                _sizePlayer = 1.14f;
                break;
            case 10:

                _velocity = 315;
                _jumpForce = 9.2f;
                h_attack = 0.50f;
                _sizePlayer = 1.15f;
                break;
            case 11:

                _velocity = 318;
                _jumpForce = 9.23f;
                h_attack = 0.50f;
                _sizePlayer = 1.17f;
                break;
            case 12:

                _velocity = 317;
                _jumpForce = 9.25f;
                h_attack = 0.49f;
                _sizePlayer = 1.175f;
                break;
            case 13:

                _velocity = 318;
                _jumpForce = 9.26f;
                h_attack = 0.48f;
                _sizePlayer = 1.175f;
                break;
            case 14:

                _velocity = 319;
                _jumpForce = 9.22f;
                h_attack = 0.47f;
                _sizePlayer = 1.18f;
                break;
            case 15:

                _velocity = 320;
                _jumpForce = 9.28f;
                h_attack = 0.46f;
                _sizePlayer = 1.185f;
                break;
            case 16:

                _velocity = 326;
                _jumpForce = 9.3f;
                h_attack = 0.45f;
                _sizePlayer = 1.187f;

                break;
            case 17:

                _velocity = 326;
                _jumpForce = 9.31f;
                h_attack = 0.44f;
                _sizePlayer = 1.188f;
                break;
            // top 7
            case 18:

                _velocity = 327;
                _jumpForce = 9.34f;
                h_attack = 0.43f;
                _sizePlayer = 1.187f;
                break;
            case 19:

                _velocity = 329;
                _jumpForce = 9.32f;
                h_attack = 0.42f;
                _sizePlayer = 1.185f;
                break;
            case 20:

                _velocity = 328;
                _jumpForce = 9.38f;
                h_attack = 0.50f;
                _sizePlayer = 1.186f;
                break;
            case 21:

                _velocity = 328;
                _jumpForce = 9.35f;
                h_attack = 0.41f;
                _sizePlayer = 1.185f;
                break;
            case 22:

                _velocity = 329;
                _jumpForce = 9.37f;
                h_attack = 0.40f;
                _sizePlayer = 1.188f;
                break;

            // Champion Cup
            case 23:

                _velocity = 332;
                _jumpForce = 9.40f;
                h_attack = 0.39f;
                _sizePlayer = 1.1885f;
                break;
            // top 5
            case 24:

                _velocity = 333;
                _jumpForce = 9.38f;
                h_attack = 0.38f;
                _sizePlayer = 1.186f;
                break;
            case 25:

                _velocity = 336;
                _jumpForce = 9.38f;
                h_attack = 0.37f;
                _sizePlayer = 1.186f;
                break;
            case 26:

                _velocity = 338;
                _jumpForce = 9.39f;
                h_attack = 0.36f;
                _sizePlayer = 1.185f;
                break;
            // top 4

            case 27:

                _velocity = 339;
                _jumpForce = 9.42f;
                h_attack = 0.34f;
                _sizePlayer = 1.189f;
                break;
            case 28:

                _velocity = 340;
                _jumpForce = 9.45f;
                h_attack = 0.32f;
                _sizePlayer = 1.19f;
                break;
            case 29:

                _velocity = 341;
                _jumpForce = 9.43f;
                h_attack = 0.3f;
                _sizePlayer = 1.191f;
                break;
            // top 3
            case 30:

                _velocity = 342;
                _jumpForce = 9.45f;
                h_attack = 0.29f;
                _sizePlayer = 1.19f;
                break;
            case 31:

                _velocity = 343;
                _jumpForce = 9.5f;
                h_attack = 0.28f;
                break;
            case 32:

                _velocity = 342;
                _jumpForce = 9.52f;
                h_attack = 0.27f;
                break;
            // top 2
            case 33:

                _velocity = 344;
                _jumpForce = 9.6f;
                h_attack = 0.26f;
                break;
            case 34:

                _velocity = 347;
                _jumpForce = 9.5f;
                h_attack = 0.25f;
                break;
            case 35:

                _velocity = 344;
                _jumpForce = 9.55f;
                h_attack = 0.24f;
                break;
            // top 1
            case 36:

                _velocity = 346;
                _jumpForce = 9.65f;
                h_attack = 0.23f;
                break;
            case 37:

                _velocity = 348;
                _jumpForce = 9.75f;
                h_attack = 0.22f;
                break;
            case 38:

                _velocity = 353;
                _jumpForce = 9.9f;
                h_attack = 0.21f;
                break;

            case 39:

                _velocity = 353;
                _jumpForce = 10f;
                h_attack = 0.2f;
                break;

            case 40:

                _velocity = 355;
                _jumpForce = 10.1f;
                h_attack = 0.18f;
                break;
            case 41:

                _velocity = 358;
                _jumpForce = 10.2f;
                h_attack = 0.17f;
                break;
            case 42:

                _velocity = 357;
                _jumpForce = 10.2f;
                h_attack = 0.15f;
                break;
            case 43:

                _velocity = 360;
                _jumpForce = 10.2f;
                h_attack = 0.12f;
                break;
            case 44:

                _velocity = 365;
                _jumpForce = 10.3f;
                h_attack = 0.11f;
                break;
            case 45:

                _velocity = 368;
                _jumpForce = 10.4f;
                h_attack = 0.1f;
                break;
            case 46:

                _velocity = 370;
                _jumpForce = 10.5f;
                h_attack = 0f;
                break;
        }
        int lv_user = PlayerPrefs.GetInt(StringUtils.level_user, 1);
        if (Menu.mode == (int)Menu.MODE.DAILY_CHALLENGE)
        {
            a_velocity = 1.2f * _velocity;
            a_jum = 1.2f * _jumpForce;
        }
        else
        {
            if (lv_user <= 2)
            {
                if (Menu.mode == (int)Menu.MODE.WORLDCUP)
                {
                    a_velocity = 1.0f * _velocity;
                    a_jum = 1.0f * _jumpForce;
                }
                else
                {
                    a_velocity = 0.65f * _velocity;
                    a_jum = 0.7f * _jumpForce;
                }

            }
            else
            {
                if (Menu.mode == (int)Menu.MODE.WORLDCUP)
                {
                    a_velocity = 1.0f * _velocity;
                    a_jum = 1.0f * _jumpForce;
                }
                else
                {
                    int win_rate = (int)((float)(PlayerPrefs.GetInt(StringUtils.win, 0)
                        / (float)(PlayerPrefs.GetInt(StringUtils.win, 0) + PlayerPrefs.GetInt(StringUtils.draw, 0) + PlayerPrefs.GetInt(StringUtils.lose, 0))) * 100);
                    if (win_rate <= 45)
                    {
                        a_velocity = _velocity;
                        a_jum = _jumpForce;
                    }
                    else
                    {
                        a_velocity = (0.95f + (float)(win_rate / 950f)) * _velocity;
                        a_jum = (0.95f + (float)(win_rate / 950f)) * _jumpForce;
                    }
                }
            }
        }

        _sizePlayer = 1.25f;
        if (Loadding.leftOrRight == 0)
        {
            transform.localScale = new Vector2(-_sizePlayer, _sizePlayer);
            transform.position = new Vector2(-7.5f, -2.908148f);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        }
        else
        {
            transform.localScale = new Vector2(-_sizePlayer, _sizePlayer);
            transform.position = new Vector2(7.5f, -2.908148f);
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

        }
        _shoot = 11;
        if (Menu.isTraining == true)
        {
            checkHead.localPosition = new Vector2(0.6f, 3);
            if (Loadding.leftOrRight == 0)
            {
                defensePos.position = new Vector2(-5.5f, 0);
            }
            else
            {
                defensePos.position = new Vector2(5.5f, 0);
            }
            rangeOfDefense = 8f;

        }
        else
        {
            if (lv_user <= 2)
            {
                if (Loadding.leftOrRight == 0)
                {
                    defensePos.position = new Vector2(-7.5f, 0);
                }
                else
                {
                    defensePos.position = new Vector2(7.5f, 0);
                }
            }
            else
            {
                if (Loadding.leftOrRight == 0)
                {
                    defensePos.position = new Vector2(-9f, 0);
                }
                else
                {
                    defensePos.position = new Vector2(9f, 0);
                }
            }
            if (Loadding.teamAI >= 40)
            {
                checkHead.localPosition = new Vector2(-3.5f / (47 - Loadding.teamAI + 6), 3);

                rangeOfDefense = 16;
            }
            else if (Loadding.teamAI >= 30)
            {
                checkHead.localPosition = new Vector2(1 / (47 - Loadding.teamAI), 3);

                rangeOfDefense = 14f;
            }
            else if (Loadding.teamAI >= 20)
            {
                checkHead.localPosition = new Vector2(1.25f / (47 - Loadding.teamAI), 3);

                rangeOfDefense = 12f;
            }
            else if (Loadding.teamAI >= 10)
            {
                checkHead.localPosition = new Vector2(1.5f / (47 - Loadding.teamAI), 3);

                rangeOfDefense = 10f;
            }
            else
            {
                checkHead.localPosition = new Vector2(2 / (47 - Loadding.teamAI), 3);

                rangeOfDefense = 8f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
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
        // shadow
        shadow.transform.position = new Vector2(transform.position.x, -2.9f);
        if (transform.position.y >= 0 || transform.gameObject.activeSelf == false)
        {
            shadow.transform.localScale = new Vector2(0, 0);
        }
        else
        {
            shadow.transform.localScale = new Vector2(-transform.position.y * 0.05f * transform.localScale.y / 1.3f,
                transform.position.y * 0.05f * transform.localScale.y / 1.3f);
        }

        if (time_fire_ball <= 16)
        {
            time_fire_ball += Time.deltaTime;
        }

        if (time_jump <= 4)
        {
            time_jump += Time.deltaTime;
        }

        if (!GameController.Instance.Scored && !GameController.Instance.EndMatch && GameController.Instance.start_game == true)
        {
            if (isIce == true)
            {
                return;
            }
            if (!grounded)
            {
                isJump = true;
            }
            else
            {
                isJump = false;
                anim.SetBool(headShoot, false);
            }


            if (_rigidbody.velocity.x != 0 && grounded)
            {
                if (isJump == false)
                {
                    anim.SetBool(moveHash, true);

                }
            }
            else
            {
                anim.SetBool(moveHash, false);
            }

            if (_ball == null)
                return;

            if (Loadding.leftOrRight == 0)
            {
                if (_ball.transform.position.x < -4)
                    defensePos.position = new Vector2(-9.5f, 0);
                else
                {
                    defensePos.position = new Vector2(-6f, 0);
                }
            }
            else
            {
                if (_ball.transform.position.x > 4)
                    defensePos.position = new Vector2(9.5f, 0);
                else
                {
                    defensePos.position = new Vector2(6f, 0);
                }
            }

            if (canHead && _ball.transform.position.y > -1f && grounded && GameController.Instance.start_game == true)
            {
                if (isX2 == true)
                {
                    if (time_jump >= 1.5f)
                        Jump();
                }
                else
                {
                    if (time_jump >= t_jump)
                        Jump();
                }

            }
            if (Menu.mode != (int)Menu.MODE.SIRVIVAL)
            {
                if (time < 40 && time > 5)
                {
                    if (Character2DController.instance.canShoot == true && number_x2 == 1 && isIce == false)
                    {
                        number_x2--;
                        X2AI();
                    }
                }
                if (time <= rd_time_ice + 30 && canShoot == true && number_ice == 1
                    && Character2DController.instance.isX2 == false && Character2DController.instance.isIce == false)
                {
                    if (_ball.transform.position.x > -4 && _ball.transform.position.x < 4)
                    {
                        number_ice--;
                        StartCoroutine(waitIce());
                    }
                }
            }
            else
            {
                if (time >= rd_time_x2 && Character2DController.instance.canShoot == true && number_x2 == 1 && isIce == false)
                {
                    number_x2--;
                    X2AI();
                }
                if (time >= rd_time_ice && canShoot == true && number_ice == 1
                    && Character2DController.instance.isX2 == false && Character2DController.instance.isIce == false)
                {
                    if (_ball.transform.position.x > -4 && _ball.transform.position.x < 4)
                    {
                        number_ice--;
                        StartCoroutine(waitIce());
                    }
                }
            }


            if (canShoot && GameController.Instance.start_game == true)
            {
                if (Character2DController.instance.canShoot == false)
                {
                    if (_ball.transform.position.y > -1f)
                    {
                        if (time_fire_ball < 15)
                        {

                            return;
                        }
                        else
                        {
                            time_fire_ball = 0;
                            _ball.transform.GetChild(1).gameObject.SetActive(true);
                            _ball.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
                            _ball.transform.GetChild(0).GetComponent<ParticleSystem>().startColor = new Color(255, 162, 0, 25);
                            StartCoroutine(WaitEffShoot(0.02f));
                            StartCoroutine(Character2DController.instance.waitDisEffBallFire());

                            if (Loadding.leftOrRight == 0)
                            {
                                rig2d_ball.velocity = new Vector2(25, 4);
                            }
                            else
                            {
                                rig2d_ball.velocity = new Vector2(-25, 4);
                            }
                        }

                    }
                    else
                        Shoot();
                }
                else
                {
                    rig2d_ball.velocity = new Vector2(0, 8);
                }
            }


            if (GameController.Instance.isMoveBoss == false)
                return;

            if (Loadding.leftOrRight == 0)
            {

                if (_ball.GetComponent<Ball>().dirAITarget > 2 && _ball.transform.position.y > 0.75f
                    && _ball.transform.position.x - 0.2f > transform.position.x)
                {
                    _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
                }
                else if (Mathf.Abs(_ball.transform.position.x - transform.position.x)
                       < Mathf.Abs(_ball.transform.position.x - _player.transform.position.x)
                    && _ball.transform.position.x - 0.2f > transform.position.x
                    && _ball.transform.position.y < -1)
                {
                    _rigidbody.velocity = new Vector2(_velocity * Time.deltaTime * 1f, _rigidbody.velocity.y);
                }
                else
                {
                    if (Mathf.Abs(_ball.transform.position.x - transform.position.x) <= rangeOfDefense)
                    {
                        float _directionMove = (_ball.transform.position.x > transform.position.x) ? 1f : -1f;
                        if (_ball.transform.position.y > h_attack - 0.25f)
                        {
                            if (transform.position.x > defensePos.position.x)
                                _rigidbody.velocity = new Vector2(-_velocity * Time.deltaTime * 1f, _rigidbody.velocity.y);
                            else _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
                        }
                        else
                        {
                            _rigidbody.velocity = new Vector2(_velocity * _directionMove * Time.deltaTime, _rigidbody.velocity.y);
                        }
                    }
                    else if (transform.position.x <= defensePos.position.x)
                    {
                        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
                    }
                    else
                    {
                        _rigidbody.velocity = new Vector2(-_velocity * Time.deltaTime * 1f, _rigidbody.velocity.y);
                    }
                }
            }
            else
            {
                if (_ball.GetComponent<Ball>().dirAITarget > 2 && _ball.transform.position.y > 0.75f
                    && _ball.transform.position.x < transform.position.x - 0.2f)
                {
                    _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
                }
                else if (Mathf.Abs(_ball.transform.position.x - transform.position.x)
                       < Mathf.Abs(_ball.transform.position.x - _player.transform.position.x)
                    && _ball.transform.position.x < transform.position.x - 0.2f
                    && _ball.transform.position.y < -1)
                {
                    _rigidbody.velocity = new Vector2(-_velocity * Time.deltaTime * 1f, _rigidbody.velocity.y);
                }

                else
                {
                    if (Mathf.Abs(_ball.transform.position.x - transform.position.x) <= rangeOfDefense)
                    {
                        float _directionMove = (_ball.transform.position.x < transform.position.x) ? 1f : -1f;
                        if (_ball.transform.position.y > h_attack - 0.25f)
                        {
                            if (transform.position.x < defensePos.position.x)
                                _rigidbody.velocity = new Vector2(_velocity * Time.deltaTime * 1f, _rigidbody.velocity.y);
                            else _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
                        }
                        else
                        {
                            _rigidbody.velocity = new Vector2(-_velocity * Time.deltaTime * _directionMove, _rigidbody.velocity.y);
                        }
                    }
                    else if (transform.position.x >= defensePos.position.x)
                    {
                        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
                    }
                    else
                    {
                        _rigidbody.velocity = new Vector2(_velocity * Time.deltaTime * 1f, _rigidbody.velocity.y);
                    }
                }
            }
        }
    }

    IEnumerator WaitEffShoot(float t)
    {
        yield return new WaitForSeconds(t);
        Instantiate(Ball.instance.eff_head, _ball.transform.position, Quaternion.identity);
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(checkGround.position, 0.2f, ground_layers);
    }

    void Shoot()
    {
        if (!GameController.Instance.Scored && !GameController.Instance.EndMatch && GameController.Instance.start_game == true)
        {
            anim.SetTrigger("Shoot");

            if (canShoot)
            {
                if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
                {
                    SoundManager.Instance.ballKick.Play();
                }
                StartCoroutine(WaitEffShoot(0.03f));
                anim.SetBool(moveHash, false);
                if (Loadding.leftOrRight == 0)
                {
                    if (Mathf.Abs(_player.transform.position.x - GameController.Instance.targetRight.transform.position.x)
                       > Mathf.Abs(_AI.transform.position.x - GameController.Instance.targetRight.transform.position.x)
                       || (Character2DController.instance.grounded == false
                       && Character2DController.instance.checkGround.position.y >= _ball.transform.position.y))
                    {
                        if (time_fire_ball < 15)
                        {
                            rig2d_ball.velocity = new Vector2(15, 0);
                            return;
                        }
                        else
                        {
                            time_fire_ball = 0;
                            _ball.transform.GetChild(1).gameObject.SetActive(true);
                            _ball.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
                            _ball.transform.GetChild(0).GetComponent<ParticleSystem>().startColor = new Color(255, 162, 0, 25);
                            rig2d_ball.velocity = new Vector2(30, 0);

                            StartCoroutine(Character2DController.instance.waitDisEffBallFire());
                        }
                    }
                    else
                    {
                        _ball.GetComponent<Rigidbody2D>().velocity = new Vector2(1 * _shoot,
                        _shoot * Mathf.Atan(angle_shot * Mathf.Deg2Rad));
                    }
                }
                else
                {
                    if (Mathf.Abs(_player.transform.position.x - GameController.Instance.targetLeft.transform.position.x)
                        > Mathf.Abs(_AI.transform.position.x - GameController.Instance.targetLeft.transform.position.x)
                        || (Character2DController.instance.grounded == false
                        && Character2DController.instance.checkGround.position.y >= _ball.transform.position.y))
                    {
                        if (time_fire_ball < 15)
                        {
                            rig2d_ball.velocity = new Vector2(-15, 0);
                            return;
                        }
                        else
                        {
                            time_fire_ball = 0;
                            _ball.transform.GetChild(1).gameObject.SetActive(true);
                            _ball.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
                            _ball.transform.GetChild(0).GetComponent<ParticleSystem>().startColor = new Color(255, 162, 0, 25);
                            rig2d_ball.velocity = new Vector2(-30, 0);
                            StartCoroutine(Character2DController.instance.waitDisEffBallFire());
                        }

                    }
                    else
                    {
                        rig2d_ball.velocity = new Vector2(-1 * _shoot, _shoot * Mathf.Atan(angle_shot * Mathf.Deg2Rad));
                    }
                }
                canShoot = false;
            }
        }
    }

    public void Jump()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.jump.Play();
        }
        isJump = true;
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x / 3, (a_jum + (Loadding.teamAI / 45f)));
        time_jump = 0;
        t_jump = Random.Range(0f, (46 - Loadding.teamAI) / 80f);
    }

    void OnDisable()
    {
        anim.SetBool(headShoot, false);
    }
}
