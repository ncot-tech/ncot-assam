using System;
using Nez;

namespace ncot_assam
{
    class ExitHitDetector : Component, ITriggerListener
    {
        public void onTriggerEnter(Collider other, Collider local)
        {
            Debug.log("onTriggerEnter: {0} entered {1}", other, local);
        }

        public void onTriggerExit(Collider other, Collider local)
        {
            Debug.log("onTriggerExit: {0} entered {1}", other, local);
        }
    }
}
