using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NumberData : MonoBehaviour
{
    [System.Serializable]
    public class DigitData
    {
        public int digit;
        public Sprite image;
    }

    public List<DigitData> numberData = new List<DigitData>();
}
