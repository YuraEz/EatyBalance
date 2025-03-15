using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using UnityEngine.Rendering;

public class Premium : MonoBehaviour
{
    [SerializeField] private string bgPlayerPref;
    [SerializeField] private int ProductPrice = 2500;
    [SerializeField] private int bgIndex;
    [SerializeField] private Button useBtn;
    [SerializeField] private GameObject hasprem;
    [SerializeField] private Button buyBtn;
    [SerializeField] private TextMeshProUGUI selectText;
    [SerializeField] private TextMeshProUGUI selectTextbg;

    [Space]
    [SerializeField] private string curProductType;

    [Space]
    [SerializeField] bool isDefault;

    [Space]
    [SerializeField] bool donateItem;
    [SerializeField] private string donateItemId;

    //  private ScoreManager scoreManager;

    [Space]
    [SerializeField] private GameObject playBtn;
    [SerializeField] private GameObject buyprem;

    private void Start()
    {
        OnStart();
        PremiumManager.Instance.onMs += OnStart;
    }

    private void OnStart(bool restore = false)
    {


        if (useBtn) useBtn.onClick.AddListener(SelectItem);

        if (donateItem)
        {
            if (restore)
            {
                PlayerPrefs.SetInt("premium", 1);
                playBtn.SetActive(true);
                buyprem.SetActive(false);
                hasprem.gameObject.SetActive(true);
                hasprem.gameObject.SetActive(false);
                BuyBG();
            }

            buyBtn.onClick.AddListener(() => TryBuyProduct(donateItemId));
        }
        else buyBtn.onClick.AddListener(BuyBG);



        //scoreManager = ServiceLocator.GetService<ScoreManager>();

        if (isDefault)
        {
            PlayerPrefs.SetInt(bgPlayerPref, 1);
            SelectItem();
        }

        int hasBG = PlayerPrefs.GetInt(bgPlayerPref, 0);

        if (hasBG == 1)
        {
            playBtn.SetActive(true);
            buyprem.SetActive(false);
            hasprem.gameObject.SetActive(true);
            buyBtn.gameObject.SetActive(false);
            if (useBtn) useBtn.gameObject.SetActive(true);
        }
        else
        {
            playBtn.SetActive(false);
            buyprem.SetActive(true);
            buyBtn.gameObject.SetActive(true);
            hasprem.gameObject.SetActive(false);
            if (useBtn) useBtn.gameObject.SetActive(false);
        }
    }


    private void Update()
    {
        if (PlayerPrefs.GetInt(curProductType, 0) != bgIndex)
        {
            if (selectText) selectText.text = "SELECT";
            if (selectTextbg) selectTextbg.text = "SELECT";
        }

        int hasBG = PlayerPrefs.GetInt(bgPlayerPref, 0);


        if (hasBG == 1)
        {
            playBtn.SetActive(true);
            buyprem.SetActive(false);
            hasprem.gameObject.SetActive(true);
            buyBtn.gameObject.SetActive(false);
            if (useBtn) useBtn.gameObject.SetActive(true);
        }
        else
        {
            playBtn.SetActive(false);
            buyprem.SetActive(true);
            buyBtn.gameObject.SetActive(true);
            hasprem.gameObject.SetActive(false);
            if (useBtn) useBtn.gameObject.SetActive(false);
        }
    }

    public void SelectItem()
    {
        PlayerPrefs.SetInt(curProductType, bgIndex);
        if (selectText) selectText.text = "SELECTED";
        if (selectTextbg) selectTextbg.text = "SELECTED";
    }

    public void BuyBG()
    {
        print("Buy");
        if (PlayerPrefs.GetInt("score", 0) >= ProductPrice)
        {
            //    scoreManager.ChangeValue(-ProductPrice, true);

            buyBtn.gameObject.SetActive(false);
            useBtn.gameObject.SetActive(true);

            PlayerPrefs.SetInt(bgPlayerPref, 1);
            SelectItem();
        }
    }



    private void TryBuyProduct(string stringId)
    {
        PremiumManager.Instance.buybg = this;

        if (!PremiumManager.Instance.IsInitialized())
        {
            Debug.Log("IAP is not initialized.");
            ///  PokupkaScreen.Instance.ShowFailed();
            return;
        }

        Product product = PremiumManager.Instance._storeController.products.WithID(stringId);

        // PokupkaScreen.Instance.ShowLoading();

        if (product != null && product.availableToPurchase)
        {
            Debug.Log($"Purchasing product asynchronously: '{product.definition.id}'");
            PremiumManager.Instance._storeController.InitiatePurchase(product);
            //BuyBG();
        }
        else
        {
            Debug.Log($"Could not initiate purchase for product ID: {stringId}. It might not be available for purchase.");
            //     PokupkaScreen.Instance.ShowFailed();
        }
    }
    //}
}
