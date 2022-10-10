using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Prize Rule", menuName = "SBF/PrizeRule")]
public class PrizeRule : ScriptableObject
{
    [SerializeField] List<Prize> rules;

    public Prize GetCurrentRule()
    {
        int total = -1;
        int tba = 0;

        int maxLevel = GetMaxPrizeLevel();

        int lvl = SaveLoadManager.GetLevel() % maxLevel;

        for (int i = 0; i < rules.Count; i++)
        {
            total += rules[i].requiredPassCount;

            if (SaveLoadManager.HasPrizeTaken(i))
            {
                if (SaveLoadManager.GetLevel() >= GetMaxPrizeLevel())
                    tba += rules[i].requiredPassCount;
                continue;
            }

            if ((lvl + tba) <= total)
            {

                rules[i].lvl = lvl;
                rules[i].tba = tba;
                rules[i].total = total;
                rules[i].percentage = (rules[i].requiredPassCount - (total - (lvl + tba))) / (rules[i].requiredPassCount * 1f);

                return rules[i];
            }
            else if (SaveLoadManager.GetLevel() > maxLevel && GetUnusedMaxPrizeLevel() <= lvl)
            {
                lvl %= GetUnusedMaxPrizeLevel();

                if ((lvl + tba) <= total)
                {

                    rules[i].lvl = lvl;
                    rules[i].tba = tba;
                    rules[i].total = total;
                    rules[i].percentage = (rules[i].requiredPassCount - (total - (lvl + tba))) / (rules[i].requiredPassCount * 1f);

                    return rules[i];
                }
            }
        }

        return null;
    }

    int GetMaxPrizeLevel()
    {
        int tot = 0;

        for (int i = 0; i < rules.Count; i++)
        {
            tot += rules[i].requiredPassCount;
        }

        return tot;
    }

    int GetUnusedMaxPrizeLevel()
    {
        int tot = 0;

        for (int i = 0; i < rules.Count; i++)
        {
            if(!SaveLoadManager.HasPrizeTaken(i))
                tot += rules[i].requiredPassCount;
        }

        return tot;
    }

    [System.Serializable] public class Prize
    {
        public int requiredPassCount = 4;
        public Sprite sprite;
        internal float percentage;
        internal int total, lvl, tba;
    }
}
