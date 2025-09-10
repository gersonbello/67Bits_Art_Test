using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private bool useRootMotion;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Slider _lifeBar;
    private float currentLife;

    int woodAmount;
    int xpAmount;

    [SerializeField] int maxXP;

    [SerializeField] UIManager uiManager;

    public Animator Anim { get; private set; }
    private Rigidbody rigg;
    private void Awake()
    {
        Anim = GetComponentInChildren<Animator>();
        Anim.applyRootMotion = useRootMotion;
        _lifeBar.value = _lifeBar.maxValue = currentLife = 100;
        rigg = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        uiManager.SetPlayerUI(maxXP);
    }

    private void Update()
    {
        var mobilejoystick = MobileJoystick.GetJoystickAxis();
        var joystick = mobilejoystick.magnitude > 0 ? mobilejoystick : JoystickAxis();
        var direction = new Vector3(joystick.x, 0, joystick.y);
        if (!useRootMotion) rigg.velocity = direction * speed * Time.deltaTime;
        Anim.SetFloat("Movement", joystick.magnitude, .25f, Time.deltaTime);
        if (direction.magnitude != 0)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        _lifeBar.transform.position = new Vector3(transform.position.x, _lifeBar.transform.position.y, transform.position.z);
        _lifeBar.value = Mathf.Lerp(_lifeBar.value, currentLife, Time.deltaTime * 2.5f);
    }
    public void TakeDamage(int damage)
    {
        currentLife -= damage;
        if (currentLife <= 0) Anim.SetTrigger("Death");
        else Anim.SetTrigger("Hit");
    }
    private Vector2 JoystickAxis()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        return new Vector2(x, y);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out GroundButton Event))
        {
            Event.Cancell();
            Event.StartCoroutine(Event.Fill(transform));
            Event.PlayAnimation(Anim);
        }
        if (other.TryGetComponent(out EnemyDamage Enemy))
        {
            Enemy.gameObject.SetActive(false);
            TakeDamage(30);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out GroundButton Event))
        {
            Event.StopAnimation();
            Event.Cancell();
        }
    }

    public Animator GetAnimator(){ return Anim; }

    public int UseWood(int woodRequired)
    {
        if(woodRequired > woodAmount)
        {
            var wood = woodAmount;
            woodAmount = 0;
            uiManager.UpdateWoodUI(woodAmount);
            return wood;
        }
        else
        {
            woodAmount -= woodRequired;
            uiManager.UpdateWoodUI(woodAmount);
            return woodRequired;
        }
    }

    public void CollectWood(int wood)
    {
        Anim.SetBool("Collect", false);
        woodAmount += wood;

        uiManager.UpdateWoodUI(woodAmount);
    }

    public void CellectXP(int xp)
    {
        xpAmount += xp;
        uiManager.UpdateXPSlider(xpAmount);
    }
}
