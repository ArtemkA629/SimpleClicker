using YG;
using UnityEngine;

public static class YGAdsProvider
{
    public const int FullScreenAdMaxChance = 101;

    public static void TryShowFullScreenAdWithChance()
    {
        var chance = Random.Range(0, FullScreenAdMaxChance);
        var random = Random.Range(0, FullScreenAdMaxChance);

        if (chance < random)
            return;

        YandexGame.FullscreenShow();
    }
}