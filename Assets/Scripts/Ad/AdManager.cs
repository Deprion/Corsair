using System;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;

public class AdManager : MonoBehaviour
{
    private RewardedAd rewardedAd;

    public static AdManager inst;

    public Action AdReward;

    private void Awake()
    {
        inst = this;

        DontDestroyOnLoad(this);
#if !UNITY_EDITOR

        RequestRewardedAd();
#endif
    }

    public void RequestRewardedAd()
    {
        try
        {
            MobileAds.SetAgeRestrictedUser(true);

            if (rewardedAd != null)
            {
                rewardedAd.Destroy();
            }

            string adUnitId = "R-M-2558191-1";

            rewardedAd = new RewardedAd(adUnitId);

            rewardedAd.OnRewarded += HandleRewarded;

            rewardedAd.LoadAd(CreateAdRequest());
        }
        catch (Exception e) { Debug.Log(e.Message); }
    }

    private void HandleRewarded(object sender, Reward e)
    {
        AdReward?.Invoke();
        RequestRewardedAd();
    }

    public void ShowRewardedAd()
    {
        if (rewardedAd == null)
        {
            RequestRewardedAd();
            return;
        }

        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }
}
