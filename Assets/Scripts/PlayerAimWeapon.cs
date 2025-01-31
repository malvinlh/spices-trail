using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAimWeapon : MonoBehaviour
{
    private Transform aimTransform;
    public SpriteRenderer characterRenderer, weaponRenderer;

    [SerializeField] GameObject tutorialPanel;

    private Player player;
    private ChangeWeapon changeWeapon;
    private Machete machete;
    private FlintlockPistol pistol;
    private GoldenMachete goldenMachete;
    private InteractableBox interactableBox;
    private bool canHandleInput = true;

    private bool isAttacking = false;
    private float lastAttackTime;
    public float attackDelay;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        changeWeapon = GetComponentInParent<ChangeWeapon>();
        aimTransform = transform.Find("PlayerAim");

        // Default weapon
        machete = GetComponentInChildren<Machete>();
        interactableBox = GetComponentInChildren<InteractableBox>();
    }

    private void Update()
    {
        HandleAiming();
        HandleAttack();
    }

    private void HandleAiming()
    {
        Vector3 mousePosition = UtilityFunctions.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        Vector3 localScale = Vector3.one;
        if (angle > 90 || angle < -90)
        {
            localScale.y = -1f;
        }
        else
        {
            localScale.y = +1f;
        }

        aimTransform.localScale = localScale;

        if (aimTransform.eulerAngles.z > 0 && aimTransform.eulerAngles.z < 180)
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
        }
        else
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
        }
    }

private void HandleAttack()
{
    if (canHandleInput && !isAttacking)
    {
        // Check if tutorialPanel is not null before accessing its properties or methods
        if (tutorialPanel == null || !tutorialPanel.activeSelf)
        {
            if (Input.GetMouseButtonDown(0) && (SceneManager.GetActiveScene().name != "Q1_D1_D2") && (SceneManager.GetActiveScene().name != "Q1_D3") && (SceneManager.GetActiveScene().name != "Q2_D1") && (SceneManager.GetActiveScene().name != "Q2_D2_D3"))
            {
                isAttacking = true;
                lastAttackTime = Time.time;

                if (changeWeapon.macheteSprite.activeSelf)
                {
                    machete.DetectColliders();
                }

                if (changeWeapon.pistolSprite.activeSelf)
                {
                    pistol.Shoot();
                }
            }

            if (Input.GetKeyDown(KeyCode.F) && (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Q2_ChickenFight") && (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Q1_5Bahan"))
            {
                interactableBox.DetectColliders();
            }
        }
    }

    if (isAttacking && Time.time - lastAttackTime >= attackDelay)
    {
        isAttacking = false;
    }
}


    public void ToggleInputHandling(bool enableInput)
    {
        canHandleInput = enableInput;
    }

    public void EquipMachete()
    {
        machete = GetComponentInChildren<Machete>();
    }

    public void EquipPistol()
    {
        pistol = GetComponentInChildren<FlintlockPistol>();
    }

    public void EquipGoldenMachete()
    {
        goldenMachete = GetComponentInChildren<GoldenMachete>();
    }
}