using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LeaderBoard", menuName = "Scriptable Objects/LeaderBoard")]
public class LeaderBoard : ScriptableObject
{
    public List<string> names;
    public List<int> scores;
}
