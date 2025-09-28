using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI; 

public class FryerClick : MonoBehaviour
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

    float maxTime;


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
        if (isCooking)
        {

            cookTimer += Time.deltaTime;
            cookTimer = Mathf.Clamp(cookTimer, 0, maxTime);
            timerSlider.value = cookTimer;

            if (cookTimer >= cookTimeRequired[3])
            {
                ChangeFriesTexture(3);
            }
            else if (cookTimer >= cookTimeRequired[2])
            {
                ChangeFriesTexture(2);
            }
            else if (cookTimer >= cookTimeRequired[1])
            {
                ChangeFriesTexture(1);
            }
            else if (cookTimer >= cookTimeRequired[0])
            {
                ChangeFriesTexture(0);
            }
        }
        else
        {
            return;
        }
    }


    void ChangeFriesTexture(int cookValue)
    {
        foreach (var item in friesInBasket)
        {
            Renderer rend = item.friesObject.GetComponent<Renderer>();
            Material friesMat = rend.material;
            switch (cookValue)
            {
                case 0:
                    friesMat.mainTexture = item.friesData.cookingMaterial.undercookTex;
                    break;
                case 1:
                    friesMat.mainTexture = item.friesData.cookingMaterial.cookTex;
                    break;
                case 2:
                    friesMat.mainTexture = item.friesData.cookingMaterial.overcookTex;
                    break;
                case 3:
                    friesMat.mainTexture = item.friesData.cookingMaterial.burntTex;
                    break;
                case 4:
                    friesMat.mainTexture = item.friesData.cookingMaterial.rawTex;
                    break;
            }
        }
    }

    public void AddFriesToBasket(RawFriesObject friesData, GameObject friesObject)
    {
        FriesInBasket fries = new FriesInBasket(friesData,friesObject);
        friesInBasket.Add(fries);
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.gameState != GameState.Fry)
            return;

        // Get info about the current animation
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        // Prevent changes if animation is still playing
        if (stateInfo.normalizedTime < 1f && stateInfo.IsName("Cook") || stateInfo.IsName("Uncook"))
            return;

        // Play animations only if the previous one is finished
        if (!isCooking)
        {
            anim.Play("Cook");

            if(currentFries != 0)
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
