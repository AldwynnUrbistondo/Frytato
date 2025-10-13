using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class FryerManager : MonoBehaviour
{
    Animator anim;
    [Header("Fryer Variables")]
    public bool isCooking = false;
    public int capacity = 10;
    public int currentFries = 0;
    public bool canAddFries = true;

    [Header("Fries in Basket")]
    [SerializeField] GameObject particle;
    public List<FriesInBasket> friesInBasket = new List<FriesInBasket>();

    [Header("Cooking Variables")]
    public float cookTimer = 0;
    public float cookTimeCumulatve;
    public float[] cookTimeRequired;
    public Slider timerSlider;
    public CookState cookState;

    float maxTime;


    // Swipe variables
    private Vector2 startTouchPos;
    private Vector2 endTouchPos;
    [SerializeField] private float swipeThreshold = 100f; // how far before we consider it a swipe

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        for (int i = 0; i < cookTimeRequired.Length; i++)
        {
            float cumulative = cookTimeCumulatve * (i + 1);
            cookTimeRequired[i] = cumulative;
        }

        maxTime = cookTimeRequired[cookTimeRequired.Length - 1] + cookTimeCumulatve;
        timerSlider.maxValue = maxTime;
    }

    private void Update()
    {
        if (isCooking && currentFries > 0)
        {
            cookTimer += Time.deltaTime;
            cookTimer = Mathf.Clamp(cookTimer, 0, maxTime);
            timerSlider.value = cookTimer;

            if (cookTimer >= cookTimeRequired[3])
            {
                cookState = CookState.Burnt;
                ChangeFriesTexture();
            }
            else if (cookTimer >= cookTimeRequired[2])
            {
                cookState = CookState.Overcook;
                ChangeFriesTexture();
            }
            else if (cookTimer >= cookTimeRequired[1])
            {
                cookState = CookState.Cook;
                ChangeFriesTexture();
            }
            else if (cookTimer >= cookTimeRequired[0])
            {
                cookState = CookState.Undercook;
                ChangeFriesTexture();
            }
        }

    }

    void ChangeFriesTexture()
    {
        foreach (var item in friesInBasket)
        {
            Renderer rend = item.friesObject.GetComponent<Renderer>();
            Material friesMat = rend.material;

            switch (cookState)
            {
                case CookState.Raw:
                    friesMat.mainTexture = item.friesData.cookingMaterial.rawTex;
                    break;
                case CookState.Undercook:
                    friesMat.mainTexture = item.friesData.cookingMaterial.undercookTex;
                    break;
                case CookState.Cook:
                    friesMat.mainTexture = item.friesData.cookingMaterial.cookTex;
                    break;
                case CookState.Overcook:
                    friesMat.mainTexture = item.friesData.cookingMaterial.overcookTex;
                    break;
                case CookState.Burnt:
                    friesMat.mainTexture = item.friesData.cookingMaterial.burntTex;
                    break;
            }
        }
    }

    public void AddFriesToBasket(RawFriesObject friesData, GameObject friesObject)
    {
        FriesInBasket fries = new FriesInBasket(friesData, friesObject);
        friesInBasket.Add(fries);

        SaveManager.Instance.itemsExisitingInScene.Add(friesData);
    }

    public void AddCookFriesToInventory(CookFriesObject friesData)
    {
        InventoryManager.Instance.AddItem(friesData);
    }

    #region Input Actions
    private bool didSwipe = false; // flag

    private void OnMouseDown()
    {
        startTouchPos = Input.mousePosition; // record start pos
        didSwipe = false; // reset each press
    }

    private void OnMouseUp()
    {
        endTouchPos = Input.mousePosition;
        Vector2 swipeDelta = endTouchPos - startTouchPos;

        if (swipeDelta.magnitude < swipeThreshold && !didSwipe)
        {
            // only handle click if no swipe detected
            HandleClick();
        }
        else
        {
            // Check direction (only right swipe here)
            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y) && swipeDelta.x > 0)
            {
                didSwipe = true;
                HandleSwipeRight();
            }
        }
    }

    private void HandleClick()
    {
        if (GameManager.Instance.gameState != GameState.Fry)
            return;

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.normalizedTime < 1f && (stateInfo.IsName("Cook") || stateInfo.IsName("Uncook") || stateInfo.IsName("Swipe")))
            return;

        if (!isCooking)
        {
            anim.Play("Cook");

            if (currentFries != 0)
            {
                particle.SetActive(true);
                canAddFries = false;
            }
        }
        else
        {
            anim.Play("Uncook");
            particle.SetActive(false);
        }

        isCooking = !isCooking;
    }

    private void HandleSwipeRight()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        // prevent spamming if animation still running
        if (stateInfo.normalizedTime < 1f && (stateInfo.IsName("Cook") || stateInfo.IsName("Uncook") || stateInfo.IsName("Swipe")))
            return;

        if (isCooking)
            return;

        anim.Play("Swipe");

        foreach (var item in friesInBasket)
        {
            Rigidbody rb = item.friesObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }

        StartCoroutine(ClearFries());

        Debug.Log("Swiped Right on Fryer!");
    }

    IEnumerator ClearFries()
    {
        yield return new WaitForSeconds(1);

        foreach (var item in friesInBasket)
        {
            switch (cookState)
            {
                case CookState.Raw:
                    AddCookFriesToInventory(item.friesData.Raw);
                    break;
                case CookState.Undercook:
                    AddCookFriesToInventory(item.friesData.Undercook);
                    break;
                case CookState.Cook:
                    AddCookFriesToInventory(item.friesData.Cook);
                    break;
                case CookState.Overcook:
                    AddCookFriesToInventory(item.friesData.Overcook);
                    break;
                case CookState.Burnt:
                    AddCookFriesToInventory(item.friesData.Burnt);
                    break;
            }

            SaveManager.Instance.itemsExisitingInScene.Remove(item.friesData);
            Destroy(item.friesObject);
        }
        friesInBasket.Clear();
        currentFries = 0;
        timerSlider.value = 0;
        cookTimer = 0;
        canAddFries = true;
    }
    #endregion

    [System.Serializable]
    public class FriesInBasket
    {
        public RawFriesObject friesData;
        public GameObject friesObject;

        public FriesInBasket(RawFriesObject friesData, GameObject friesObject)
        {
            this.friesData = friesData;
            this.friesObject = friesObject;
        }
    }
}
