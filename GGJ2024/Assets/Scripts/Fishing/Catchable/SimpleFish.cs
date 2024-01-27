using UnityEngine;

namespace GGJ.Fishing.Catchable
{
    public class SimpleFish : CatchableItemBase
    {
        public override void OnCatchEnd(PlayerFishing fisher)
        {
            Destroy(gameObject);
        }
    }
}