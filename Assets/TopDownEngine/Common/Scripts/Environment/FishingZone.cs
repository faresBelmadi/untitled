using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

namespace MoreMountains.TopDownEngine
{
    /// <summary>
    /// Add this zone to a trigger collider and it'll trigger the ability to fish
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    [AddComponentMenu("TopDown Engine/Environment/Fishing Zone")]
    public class FishingZone : TopDownMonoBehaviour
    {
        protected CharacterFishing _characterFishing;

        /// <summary>
        /// On start we make sure our collider is set to trigger
        /// </summary>
        protected virtual void Start()
        {
            this.gameObject.MMGetComponentNoAlloc<Collider2D>().isTrigger = true;
        }

        /// <summary>
        /// On enter we allow fishing if we can
        /// </summary>
        /// <param name="collider"></param>
        protected virtual void OnTriggerEnter2D(Collider2D collider)
        {
            _characterFishing = collider.gameObject.MMGetComponentNoAlloc<Character>()?.FindAbility<CharacterFishing>();
            if (_characterFishing != null)
            {
                _characterFishing.CheckWater(true);
            }
        }

        /// <summary>
        /// On exit we stop force crouching
        /// </summary>
        /// <param name="collider"></param>
        protected virtual void OnTriggerExit2D(Collider2D collider)
        {
            _characterFishing = collider.gameObject.MMGetComponentNoAlloc<Character>()?.FindAbility<CharacterFishing>();
            if (_characterFishing != null)
            {
                _characterFishing.CheckWater(false);
            }
        }
    }
}