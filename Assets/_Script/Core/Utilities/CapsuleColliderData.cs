using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Core
{
    public class CapsuleColliderData
    {
        public CapsuleCollider2D collider;
        public Vector2 centerInLocalSpace;

        public void Initialize(GameObject gameObject)
        {
            if (collider != null) return;

            collider = gameObject.GetComponent<CapsuleCollider2D>();
            UpdateCollderData();
        }

        private void UpdateCollderData()
        {
            centerInLocalSpace = collider.offset;
        }
    }
}
