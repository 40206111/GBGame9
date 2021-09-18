using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEntityBase : MonoBehaviour
{
    protected EntityBaseInfo EntityInfo;
    protected StatGrowth StatGrowth;

    BaseStatBlock BaseStats = new BaseStatBlock();
    protected int Level = 3;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void CalculatedStatsForLevel()
    {
        int levelMult = Level - 1;
        BaseStats = EntityInfo + StatGrowth * levelMult;
    }
}
