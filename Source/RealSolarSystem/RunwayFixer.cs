using System.Collections;
using System.Linq;
using UnityEngine;
using static RunwayCollisionHandler;

namespace RealSolarSystem
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]

    public class RunwayFixer : MonoBehaviour
    {
        internal bool hold = false;

        public bool debug = false;

        public float holdThreshold = 2700.0f;
        public float holdThresholdSqr;

        public float originalThreshold = 0.0f;
        public float originalThresholdSqr = 0.0f;

        private readonly int layerMask = 1 << 15;

        private int frameSkip = 0;
        internal bool isOnRunway = false;
        internal string lastHitColliderName;

        internal bool collidersDisabled = false;
        private bool waiting = false;
        private IEnumerator waitCoro = null;
        private bool coroComplete = false;

        private Coroutine _sectionsLoadRoutine;

        public static RunwayFixer Instance { get; private set; } = null;

        public void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance);
            }

            Instance = this;
        }

        public void Start()
        {
            ConfigNode rwSettingsNode = GameDatabase.Instance.GetConfigNodes("REALSOLARSYSTEM").FirstOrDefault(n => n.HasNode("RSSRUNWAYFIX"))?.GetNode("RSSRUNWAYFIX");

            if (rwSettingsNode != null)
            {
                if (bool.TryParse(rwSettingsNode.GetValue("debug"), out bool bTemp))
                {
                    debug = bTemp;
                }

                if (float.TryParse(rwSettingsNode.GetValue("holdThreshold"), out float fTemp))
                {
                    holdThreshold = fTemp;
                }
            }

            GameEvents.onVesselGoOffRails.Add(OnVesselGoOffRails);
            GameEvents.onVesselGoOnRails.Add(OnVesselGoOnRails);
            GameEvents.onVesselSwitching.Add(OnVesselSwitching);
            GameEvents.onVesselSituationChange.Add(OnVesselSituationChange);

            DestructibleBuilding.OnLoaded.Add(OnSectionLoaded);
        }

        public void OnDestroy()
        {
            GameEvents.onVesselGoOffRails.Remove(OnVesselGoOffRails);
            GameEvents.onVesselGoOnRails.Remove(OnVesselGoOnRails);
            GameEvents.onVesselSwitching.Remove(OnVesselSwitching);
            GameEvents.onVesselSituationChange.Remove(OnVesselSituationChange);

            DestructibleBuilding.OnLoaded.Remove(OnSectionLoaded);
        }

        private void OnSectionLoaded(DestructibleBuilding data)
        {
            // At the end of the frame, KSP will fire this event for every destructible KSC prop.
            // We wait until the next frame so that all of the runway sections are guaranteed to be loaded.

            if (!collidersDisabled && _sectionsLoadRoutine == null)
            {
                _sectionsLoadRoutine = StartCoroutine(SectionsLoadRoutine());
            }
        }

        private IEnumerator SectionsLoadRoutine()
        {
            yield return null;

            TryDisableColliders();

            _sectionsLoadRoutine = null;
        }

        public void OnVesselGoOnRails(Vessel v)
        {
            FloatingOrigin.fetch.threshold = originalThreshold;
            FloatingOrigin.fetch.thresholdSqr = originalThresholdSqr;

            hold = false;
        }

        public void OnVesselGoOffRails(Vessel v)
        {
            originalThreshold = FloatingOrigin.fetch.threshold;
            originalThresholdSqr = FloatingOrigin.fetch.thresholdSqr;

            holdThresholdSqr = holdThreshold * holdThreshold;

            if (!collidersDisabled)
            {
                hold = false;

                return;
            }

            hold = true;
            waiting = false;
        }

        public void OnVesselSwitching(Vessel from, Vessel to)
        {
            if (to == null || to.situation != Vessel.Situations.LANDED)
            {
                // FIXME: Do we need PRELAUNCH here?

                return;
            }

            waiting = false;
        }

        private Vector3 GetDownwardVector()
        {
            Vessel v = FlightGlobals.ActiveVessel;

            return (v.CoM - v.mainBody.transform.position).normalized * -1;
        }

        public void OnVesselSituationChange(GameEvents.HostedFromToAction<Vessel, Vessel.Situations> data)
        {
            if (data.host != FlightGlobals.ActiveVessel)
            {
                return;
            }

            hold = data.to == Vessel.Situations.LANDED;

            if (!hold && FloatingOrigin.fetch.threshold > originalThreshold && originalThreshold > 0)
            {
                if (waitCoro != null && !coroComplete)
                {
                    StopCoroutine(waitCoro);
                }

                waitCoro = RestoreThreshold();

                coroComplete = false;

                StartCoroutine(waitCoro);
            }
        }

        private IEnumerator RestoreThreshold()
        {
            while (!hold && !waiting && FlightGlobals.ActiveVessel.radarAltitude < 10)
            {
                waiting = true;

                yield return new WaitForSeconds(5);

                waiting = false;
            }

            // Check again as situation could have changed.

            if (!hold && FloatingOrigin.fetch.threshold > originalThreshold && originalThreshold > 0)
            {
                FloatingOrigin.fetch.threshold = originalThreshold;
                FloatingOrigin.fetch.thresholdSqr = originalThresholdSqr;
            }

            coroComplete = true;
        }

        public void FixedUpdate()
        {
            frameSkip++;

            if (frameSkip < 25)
            {
                return;
            }

            frameSkip = 0;

            if (!CheckRunway())
            {
                if (isOnRunway)
                {
                    isOnRunway = false;
                }

                return;
            }

            FloatingOrigin.fetch.threshold = holdThreshold;
            FloatingOrigin.fetch.thresholdSqr = holdThresholdSqr;

            if (!isOnRunway)
            {
                isOnRunway = true;
            }

            FloatingOrigin.SetSafeToEngage(false);
        }

        private bool CheckRunway()
        {
            if (!hold)
            {
                return false;
            }

            Vessel v = FlightGlobals.ActiveVessel;

            if (v == null || (v.situation != Vessel.Situations.LANDED && v.situation != Vessel.Situations.PRELAUNCH))
            {
                return false;
            }

            Vector3 down = GetDownwardVector();

            bool hit = Physics.Raycast(v.transform.position, down, out RaycastHit raycastHit, 100, layerMask);

            if (!hit)
            {
                return false;
            }

            lastHitColliderName = raycastHit.collider.gameObject.name;

            return lastHitColliderName == "runway_collider";
        }

        private void TryDisableColliders()
        {
            // Once disabled, the colliders will stay disabled.

            if (collidersDisabled)
            {
                return;
            }

            var rwHandler = FindObjectOfType<RunwayCollisionHandler>();

            if (rwHandler == null)
            {
                return;
            }

            foreach (RunwaySection section in rwHandler.runwaySections)
            {
                Collider sc = section.sectionCollider;

                sc.enabled = false;
            }

            collidersDisabled = true;
        }
    }
}
