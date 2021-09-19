using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] Button _showAdButton;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOsAdUnitId = "Rewarded_iOS";
    string _adUnitId;
    [SerializeField] SceneAndGUI sceneAndGUI;

    private bool adShown = false;
    public bool AdShown
    {
        get { return adShown; }
    }
    private bool AdLoadAttempted = false;
    private bool AdLoaded = false;
    void Awake()
    {
        // Get the Ad Unit ID for the current platform:
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsAdUnitId
            : _androidAdUnitId;

        //Disable button until ad is ready to show
        _showAdButton.interactable = false;
        //if it is already initialized we call LoadAd() here if not the Listener on AdsInitializer script will call LoadAd() when ads are ready
       
    }
    void Update()
    {
        if (adShown)//so it remains true only for one frame
        {
            adShown = false;
        }
        if (AdsInitializer.AlreadyInitialized && !AdLoadAttempted)
        {
            AdLoadAttempted = true;
            LoadAd();
        }
        if(AdLoaded && !_showAdButton.interactable)
        {
            _showAdButton.interactable = true;
        } else if(!AdLoaded)
        {
            _showAdButton.interactable = false ;
        }
    }
    // Load content to the Ad Unit:
    private void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);

        if (adUnitId.Equals(_adUnitId))
        {
            // Configure the button to call the ShowAd() method when clicked:
            //  _showAdButton.onClick.AddListener(ShowAd);
            // Enable the button for users to click:
            // _showAdButton.interactable = true;
            AdLoaded = true;
        }
    }

    // Implement a method to execute when the user clicks the button.
    public void ShowAd()
    {
        // Disable the button: 
        _showAdButton.interactable = false;
        // Then show the ad:
        Advertisement.Show(_adUnitId, this);
    }

    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            // Grant a reward.
            GameSystem.Life = GameSystem.Life+1;
            adShown = true;
            AdLoaded = false;
            AdLoadAttempted = false;
            // Load another ad:
            Advertisement.Load(_adUnitId, this);
        }
       
    }

    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
        AdLoadAttempted = false;
        AdLoaded = false;
        //  StartCoroutine(sceneAndGUI.AdFailed());
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
        AdLoadAttempted = false;
        AdLoaded = false;
        //   StartCoroutine(sceneAndGUI.AdFailed());
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    void OnDestroy()
    {
        // Clean up the button listeners:
        _showAdButton.onClick.RemoveAllListeners();
    }
}