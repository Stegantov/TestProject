using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tools/Game/LevelData")]
public class LevelData : ScriptableObject
{
    public int rows;
    public int columns;
    public List<string> items; 
}
