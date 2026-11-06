using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class WelfareScript : MonoBehaviour
{
    public List<HabitatScript> habitatStatesList = new List<HabitatScript>();
    public List<KeyValuePair<string,int>> welfareScoresList = new List<KeyValuePair<string,int>>();

    private float zooWelfareScore;

    // Update is called once per frame
    void Update()
    {
        zooWelfareScore = calculateOverallWelfare();
    }

    public float calculateOverallWelfare()
    {
        float combinedScores = 0;

        foreach (HabitatScript habitat in habitatStatesList)
        {
            combinedScores += habitat.getWelfareScore();
        }

        return combinedScores/habitatStatesList.Count;
    }

}
