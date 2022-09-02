using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Character2DController : MonoBehaviour
{
	public static Character2DController instance;

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
	public float a_XShoot, a_YShoot, a_XHead, a_velocity, a_jum, angle_shot;
	public GameObject eff_x2_player, eff_ice_block, eff_explo_ice;
	public bool isIce, isX2;
	public static float time_ok_x2;
	public GameObject shadow, eff_power;
	public Image img_circle_loadding, img_shot_power;
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
		time_ok_x2 = 0;
		img_shot_power.color = Color.gray;
		img_shot_power.CrossFadeAlpha(0.4f, 0.1f, true);
		isIce = false;
		isX2 = false;
		isHeadShoot = false;
		_rigidbody = GetComponent<Rigidbody2D>();
		canShoot = false;
		grounded = false;
		celebrateHash = Animator.StringToHash("Celebrate");
		headShootHash = Animator.StringToHash("HeadShoot");
		moveHash = Animator.StringToHash("Move");
		int id = PlayerPrefs.GetInt(StringUtils.id_player, 17);
		StartCoroutine(waitInfoPlayer());



	}

	private void Update()
	{
		//horizontalAxis = Input.GetAxis("Horizontal");
		if (Input.GetKey(KeyCode.L))
		{
			Jump();
		}
		if (Input.GetKey(KeyCode.K))
		{
			Shoot();
		}
		if (Input.GetKey(KeyCode.J))
		{
			Shoot2();
		}
		if (GameController.Instance.start_game == true)
		{
			if (time_ok_x2 < 15)
			{
				time_ok_x2 += Time.deltaTime;
				img_circle_loadding.fillAmount = time_ok_x2 / 15f;
				img_shot_power.color = Color.gray;
				img_shot_power.CrossFadeAlpha(0.4f, 0.1f, true);
				eff_power.SetActive(false);
			}
			else
			{
				img_circle_loadding.fillAmount = 1;
				img_shot_power.color = Color.white;
				img_shot_power.CrossFadeAlpha(1, 0.1f, true);
				eff_power.SetActive(true);
			}
		}

		if (!GameController.Instance.Scored && !GameController.Instance.EndMatch)
		{
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
		shadow.transform.position = new Vector2(transform.position.x, -2.9f);
		if (transform.position.y >= 0 || transform.gameObject.activeSelf == false)
		{
			shadow.transform.localScale = new Vector2(0, 0);
		}
		else
			shadow.transform.localScale = new Vector2(-transform.position.y * 0.05f * transform.localScale.y / 1.3f
				, -transform.position.y * 0.05f * transform.localScale.y / 1.3f);
	}
	void FixedUpdate()
	{
		_rigidbody.velocity = new Vector2(horizontalAxis * Time.deltaTime * 0.8f * _velocity, _rigidbody.velocity.y);
		grounded = Physics2D.OverlapCircle(checkGround.position, 0.25f, ground_layers);
	}
	public void ButtonX2Player(GameObject btn)
	{
		if (btn.transform.GetChild(0).GetComponent<Image>().fillAmount < 1)
		{
			return;
		}
		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.buttonClick.Play();
		}
		if (Menu.mode == (int)Menu.MODE.SIRVIVAL)
		{
			StringUtils.item_x2_survival--;
		}
		float _sizePlayer2 = 2f;
		isX2 = true;
		transform.localScale = new Vector2(_sizePlayer2, _sizePlayer2);
		eff_x2_player.SetActive(true);
		btn.SetActive(false);
		StartCoroutine(WaitX2Player());
		if (isIce == true)
		{
			Anim.enabled = true;
			eff_ice_block.SetActive(false);

			if (eff_explo_ice != null)
			{
				eff_explo_ice.SetActive(true);
				eff_explo_ice.transform.SetParent(transform.parent);
			}
			isIce = false;
			StopCoroutine(AIPlayer.instance.waitIce());
		}
	}
	IEnumerator WaitX2Player()
	{

		yield return new WaitForSeconds(5);
		isX2 = false;
		float _sizePlayer2 = _sizePlayer;
		transform.localScale = new Vector2(_sizePlayer2, _sizePlayer2);
	}
	public void Move(int value)
	{
		if (!GameController.Instance.Scored && !GameController.Instance.EndMatch && GameController.Instance.start_game == true)
		{
			if (isIce == true)
			{
				return;
			}
			if ((Loadding.leftOrRight == 0 && value == -1) || (Loadding.leftOrRight == 1 && value == 1))
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
	public void StopShoot(GameObject obj)
	{
		obj.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1);
		obj.GetComponent<Image>().CrossFadeAlpha(1f, 0.1f, true);
		isShoot = false;

	}

	public void Jump()
	{
		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.jump.Play();
		}
		if (isIce == true)
		{
			return;
		}
		isJump = true;
		btnJump.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1);
		btnJump.GetComponent<Image>().CrossFadeAlpha(0.4f, 0.1f, true);
		if (!GameController.Instance.Scored && !GameController.Instance.EndMatch && GameController.Instance.start_game == true)
		{
			if (grounded)
			{
				_rigidbody.velocity = new Vector2(_rigidbody.velocity.x / 3, _jumpForce);

			}
		}
	}

	public void Shoot()
	{

		btnShoot.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1);
		btnShoot.GetComponent<Image>().CrossFadeAlpha(0.4f, 0.1f, true);
		if (!GameController.Instance.Scored && !GameController.Instance.EndMatch && GameController.Instance.start_game == true)
		{
			if (isIce == true)
			{
				return;
			}
			_animator.SetTrigger("Shoot");
			if (canShoot)
			{
				if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
				{
					SoundManager.Instance.ballKick.Play();
				}

				if (isShoot == false)
				{
					Instantiate(Ball.instance.eff_head, _ball.transform.position, Quaternion.identity);
					isShoot = true;
				}
				_animator.SetBool(moveHash, false);
				if (isJump == false)
				{
					if (Loadding.leftOrRight == 0)
					{
						_ball.GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * _shoot,
							_shoot * Mathf.Atan(angle_shot * Mathf.Deg2Rad));
					}
					else
					{
						_ball.GetComponent<Rigidbody2D>().velocity = new Vector2(1 * _shoot,
						   _shoot * Mathf.Atan(angle_shot * Mathf.Deg2Rad));
					}
				}
				else
				{
					if (Loadding.leftOrRight == 0)
					{
						_ball.GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * _shoot,
							_shoot * Mathf.Atan(35 * Mathf.Deg2Rad));
					}
					else
					{
						_ball.GetComponent<Rigidbody2D>().velocity = new Vector2(1 * _shoot,
						   _shoot * Mathf.Atan(35 * Mathf.Deg2Rad));
					}
				}

			}
		}
	}

	public void Shoot2()
	{
		if (img_circle_loadding.fillAmount < 1)
		{
			return;
		}
		btnLowShoot.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1);
		btnLowShoot.GetComponent<Image>().CrossFadeAlpha(0.4f, 0.1f, true);
		if (!GameController.Instance.Scored && !GameController.Instance.EndMatch && GameController.Instance.start_game == true)
		{
			if (isIce == true)
			{
				return;
			}
			_animator.SetTrigger("Shoot");
			if (canShoot)
			{

				time_ok_x2 = 0;
				if (isShoot == false)
				{
					Instantiate(Ball.instance.eff_head, _ball.transform.position, Quaternion.identity);
					isShoot = true;
				}
				if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
				{
					SoundManager.Instance.ballKick.Play();
				}
				_animator.SetBool(moveHash, false);
				_ball.transform.GetChild(1).gameObject.SetActive(true);
				_ball.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
				_ball.transform.GetChild(0).GetComponent<ParticleSystem>().startColor = new Color(255, 162, 0, 25);
				if (Loadding.leftOrRight == 0)
				{
					_ball.GetComponent<Rigidbody2D>().velocity = new Vector2(-25, 4);
				}
				else
				{
					_ball.GetComponent<Rigidbody2D>().velocity = new Vector2(25, 4);
				}
				StartCoroutine(waitDisEffBallFire());

				if (Menu.mode != (int)Menu.MODE.SIRVIVAL)
				{
					if (AIPlayer.time > 40 && AIPlayer.time <= 80)
					{
						if (AIPlayer.number_x2 == 1 && _ball.transform.position.y >= -0.5f && AIPlayer.instance.isIce == false)
						{
							AIPlayer.number_x2--;
							AIPlayer.instance.X2AI();
						}
					}
				}
			}
		}
	}
	public IEnumerator waitDisEffBallFire()
	{
		yield return new WaitForSeconds(1f);
		_ball.transform.GetChild(1).gameObject.SetActive(false);
		_ball.transform.GetChild(1).GetComponent<ParticleSystem>().Stop();
		_ball.transform.GetChild(0).GetComponent<ParticleSystem>().startColor = new Color(255, 255, 255, 25);
	}
	public IEnumerator waitDisIce()
	{
		AIPlayer.instance.isIce = true;
		AIPlayer.instance.anim.SetBool(AIPlayer.instance.moveHash, true);
		AIPlayer.instance.anim.SetBool(AIPlayer.instance.CelebrateHash, false);
		AIPlayer.instance.anim.enabled = false;

		yield return new WaitForSeconds(0.15f);
		AIPlayer.instance.eff_ice_block.SetActive(true);
		yield return new WaitForSeconds(4f);
		AIPlayer.instance.anim.enabled = true;
		AIPlayer.instance.eff_ice_block.SetActive(false);

		if (AIPlayer.instance.eff_explo_ice != null)
		{
			AIPlayer.instance.eff_explo_ice.SetActive(true);
			AIPlayer.instance.eff_explo_ice.transform.SetParent(transform.parent);
		}

		yield return new WaitForSeconds(0.5f);
		AIPlayer.instance.isIce = false;
	}

	public void ButtonItemIce(GameObject btn)
	{
		if (btn.transform.GetChild(0).GetComponent<Image>().fillAmount < 1)
		{
			return;
		}
		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.buttonClick.Play();
		}
		btn.SetActive(false);
		if (Menu.mode == (int)Menu.MODE.SIRVIVAL)
		{
			StringUtils.item_ice_survival--;
		}
		if (AIPlayer.instance.isX2 == true)
		{
			AIPlayer.instance.isX2 = false;
			AIPlayer.instance.eff_x2_ai.SetActive(false);
			AIPlayer.instance.transform.localScale = new Vector2(-_sizePlayer, _sizePlayer);
		}
		StartCoroutine(waitDisIce());
	}

	IEnumerator waitInfoPlayer()
	{
		int id = PlayerPrefs.GetInt(StringUtils.id_player, 17);
		yield return new WaitForSeconds(0.1f);
		switch (id)
		{
			case 46:

				_velocity = 305;
				_jumpForce = 9.5f;
				angle_shot = 44;
				_shoot = 12;
				break;
			case 45:

				_velocity = 305;
				_jumpForce = 9.5f;
				angle_shot = 44;
				_shoot = 12;
				break;
			case 44:

				_velocity = 305;
				_jumpForce = 9.5f;
				angle_shot = 44;
				_shoot = 12;
				break;
			case 43:

				_velocity = 305;
				_jumpForce = 9.5f;
				angle_shot = 44;
				_shoot = 12;
				break;
			case 42:

				_velocity = 305;
				_jumpForce = 9.5f;
				angle_shot = 44;
				_shoot = 12;
				break;
			case 41:

				_velocity = 305;
				_jumpForce = 9.5f;
				angle_shot = 44;
				_shoot = 12;
				break;
			case 40:

				_velocity = 305;
				_jumpForce = 9.5f;
				angle_shot = 44;
				_shoot = 12;
				break;
			case 39:

				_velocity = 305;
				_jumpForce = 9.5f;
				angle_shot = 44;
				_shoot = 12;
				break;
			case 38:

				_velocity = 305;
				_jumpForce = 9.5f;
				angle_shot = 44;
				_shoot = 12;
				break;
			case 37:

				_velocity = 305;
				_jumpForce = 9.5f;
				angle_shot = 44;
				_shoot = 12;
				break;
			case 36:

				_velocity = 305;
				_jumpForce = 9.5f;
				angle_shot = 44;
				_shoot = 12;
				break;
			case 35:

				_velocity = 305;
				_jumpForce = 9.5f;
				angle_shot = 44;
				_shoot = 12;
				break;
			case 34:

				_velocity = 305;
				_jumpForce = 9.5f;
				angle_shot = 44;
				_shoot = 12;
				break;
			case 33:

				_velocity = 305;
				_jumpForce = 9.5f;
				angle_shot = 44;
				_shoot = 12;
				break;
			case 32:

				_velocity = 305;
				_jumpForce = 9.5f;
				angle_shot = 44;
				_shoot = 12;
				break;
			case 31:

				_velocity = 308;
				_jumpForce = 9.6f;
				angle_shot = 45f;
				_shoot = 11.9f;
				break;
			case 30:

				_velocity = 309;
				_jumpForce = 9.7f;
				angle_shot = 45.5f;
				_shoot = 11.8f;
				break;
			// top 7
			case 29:

				_velocity = 310;
				_jumpForce = 9.8f;
				angle_shot = 46f;
				_shoot = 11.7f;
				break;
			case 28:

				_velocity = 311;
				_jumpForce = 9.9f;
				angle_shot = 46.5f;
				_shoot = 11.6f;
				break;
			case 27:

				_velocity = 313;
				_jumpForce = 10f;
				angle_shot = 50.5f;
				_shoot = 12;
				break;
			case 26:

				_velocity = 314;
				_jumpForce = 10.1f;
				angle_shot = 50.5f;
				_shoot = 12;
				break;
			case 25:

				_velocity = 315;
				_jumpForce = 10.2f;
				angle_shot = 51;
				_shoot = 11;
				break;

			// Champion Cup
			case 24:

				_velocity = 316;
				_jumpForce = 10.3f;
				angle_shot = 51;
				_shoot = 11;
				break;
			// top 5
			case 23:

				_velocity = 318;
				_jumpForce = 10.4f;
				angle_shot = 51.5f;
				_shoot = 11;
				break;
			case 22:

				_velocity = 319;
				_jumpForce = 10.6f;
				angle_shot = 51.5f;
				_shoot = 11;
				break;
			case 21:

				_velocity = 320;
				_jumpForce = 10.7f;
				angle_shot = 51.75f;
				_shoot = 11;
				break;
			// top 4

			case 20:

				_velocity = 321;
				_jumpForce = 10.8f;
				angle_shot = 51.75f;
				_shoot = 11;
				break;
			case 19:

				_velocity = 323;
				_jumpForce = 10.9f;
				angle_shot = 52;
				_shoot = 11;
				break;
			case 18:

				_velocity = 325;
				_jumpForce = 11f;
				angle_shot = 52;
				_shoot = 11;
				break;
			// top 3
			case 17:

				_velocity = 326;
				_jumpForce = 11f;
				angle_shot = 52.5f;
				_shoot = 11;
				break;
			case 16:

				_velocity = 328;
				_jumpForce = 11f;
				angle_shot = 53f;
				_shoot = 11;
				break;
			case 15:

				_velocity = 329;
				_jumpForce = 11.1f;
				angle_shot = 54;
				_shoot = 11;
				break;
			// top 2
			case 14:

				_velocity = 330;
				_jumpForce = 11.2f;
				angle_shot = 55;
				_shoot = 11;
				break;
			case 13:

				_velocity = 332;
				_jumpForce = 10.5f;
				angle_shot = 55;
				_shoot = 11;
				break;
			case 12:

				_velocity = 334;
				_jumpForce = 10.5f;
				angle_shot = 56;
				_shoot = 11;
				break;
			// top 1
			case 11:

				_velocity = 335;
				_jumpForce = 11.35f;
				angle_shot = 57;
				_shoot = 11;
				break;
			case 10:

				_velocity = 336;
				_jumpForce = 11.5f;
				angle_shot = 58;
				_shoot = 11;
				break;
			case 9:

				_velocity = 338;
				_jumpForce = 11.4f;
				angle_shot = 60;
				_shoot = 11;
				break;

			case 8:

				_velocity = 340;
				_jumpForce = 11.5f;
				angle_shot = 61;
				_shoot = 11;
				break;

			case 7:

				_velocity = 342;
				_jumpForce = 10.6f;
				angle_shot = 62;
				_shoot = 11;
				break;
			case 6:

				_velocity = 343;
				_jumpForce = 11.6f;
				angle_shot = 63;
				_shoot = 11;
				break;
			case 5:

				_velocity = 405;
				_jumpForce = 11.6f;
				angle_shot = 64;
				_shoot = 11;
				break;
			case 4:

				_velocity = 344;
				_jumpForce = 11.7f;
				angle_shot = 65;
				_shoot = 11;
				break;
			case 3:

				_velocity = 345;
				_jumpForce = 11.8f;
				angle_shot = 68;
				_shoot = 10.7f;
				break;
			case 2:

				_velocity = 350;
				_jumpForce = 11.9f;
				angle_shot = 69;
				_shoot = 10.6f;
				break;
			case 1:

				_velocity = 350;
				_jumpForce = 12f;
				angle_shot = 70;
				_shoot = 10.5f;
				break;
		}
		_velocity = PlayerPrefs.GetFloat(StringUtils.str_speed + (id - 1));
		_shoot = PlayerPrefs.GetFloat(StringUtils.str_shoot + (id - 1));
		_sizePlayer = 1.25f;
		_jumpForce = PlayerPrefs.GetFloat(StringUtils.str_jump + (id - 1));
		angle_shot = PlayerPrefs.GetFloat(StringUtils.str_angle_shoot + (id - 1));
		if (Loadding.leftOrRight == 0)
		{
			transform.localScale = new Vector2(_sizePlayer, _sizePlayer);
			transform.position = new Vector2(7.5f, -2.908148f);
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
		}
		else
		{
			transform.localScale = new Vector2(_sizePlayer, _sizePlayer);
			transform.position = new Vector2(-7.5f, -2.908148f);
			transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

		}
	}
	void OnDisable()
	{
		_animator.SetBool(headShootHash, false);
	}


}
