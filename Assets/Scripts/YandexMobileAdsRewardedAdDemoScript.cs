using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YandexMobileAds;
using YandexMobileAds.Base;

public class YandexMobileAdsRewardedAdDemoScript : MonoBehaviour
{
    [SerializeField] private TMP_Text testText;

    private String message = "";

    private RewardedAd rewardedAd;

    [SerializeField] private Button load, show;

    public void RequestRewardedAd()
    {
        //Sets COPPA restriction for user age under 13
        MobileAds.SetAgeRestrictedUser(true);

        if (this.rewardedAd != null)
        {
            this.rewardedAd.Destroy();
        }

        // Replace demo Unit ID 'demo-rewarded-yandex' with actual Ad Unit ID
        string adUnitId = "R-M-2558191-1";

        this.rewardedAd = new RewardedAd(adUnitId);

        this.rewardedAd.OnRewardedAdLoaded += this.HandleRewardedAdLoaded;
        this.rewardedAd.OnRewardedAdFailedToLoad += this.HandleRewardedAdFailedToLoad;
        this.rewardedAd.OnReturnedToApplication += this.HandleReturnedToApplication;
        this.rewardedAd.OnLeftApplication += this.HandleLeftApplication;
        this.rewardedAd.OnAdClicked += this.HandleAdClicked;
        //this.rewardedAd.OnRewardedAdShown += this.HandleRewardedAdShown;
        //this.rewardedAd.OnRewardedAdDismissed += this.HandleRewardedAdDismissed;
        //this.rewardedAd.OnImpression += this.HandleImpression;
        this.rewardedAd.OnRewarded += this.HandleRewarded;
        //this.rewardedAd.OnRewardedAdFailedToShow += this.HandleRewardedAdFailedToShow;

        this.rewardedAd.LoadAd(this.CreateAdRequest());
        this.DisplayMessage("Rewarded Ad is requested");
    }

    public void ShowRewardedAd()
    {
        if (rewardedAd.IsLoaded())
            this.rewardedAd.Show();
        else
            DisplayMessage("not loaded yet");
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    private void DisplayMessage(String message)
    {
        this.message = message + (this.message.Length == 0 ? "" : "\n--------\n" + this.message);
        testText.text = message;
    }

    #region Rewarded Ad callback handlers

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailureEventArgs args)
    {
        this.DisplayMessage(
            "HandleRewardedAdFailedToLoad event received with message: " + args.Message);
    }

    public void HandleReturnedToApplication(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleReturnedToApplication event received");
    }

    public void HandleLeftApplication(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleLeftApplication event received");
    }

    public void HandleAdClicked(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleAdClicked event received");
    }

    public void HandleRewardedAdShown(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleRewardedAdShown event received");
    }

    public void HandleRewardedAdDismissed(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleRewardedAdDismissed event received");
    }

    public void HandleImpression(object sender, ImpressionData impressionData)
    {
        var data = impressionData == null ? "null" : impressionData.rawData;
        this.DisplayMessage("HandleImpression event received with data: " + data);
    }

    public void HandleRewarded(object sender, Reward args)
    {
        this.DisplayMessage("HandleRewarded event received: amout = " + args.amount + ", type = " + args.type);
    }

    public void HandleRewardedAdFailedToShow(object sender, AdFailureEventArgs args)
    {
        this.DisplayMessage(
            "HandleRewardedAdFailedToShow event received with message: " + args.Message);
    }

    #endregion
}
