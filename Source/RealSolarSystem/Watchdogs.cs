using UnityEngine;
using UnityEngine.Rendering;
using System.Linq;

namespace RealSolarSystem
{
    // The RSS watchdog is a general place to prevent RSS changes
    // from being reverted by other mods when our back is turned.

    [KSPAddon (KSPAddon.Startup.FlightAndKSC, false)]

    public class WatchDog : MonoBehaviour
    {
        private ConfigNode RSSSettings = null;

        private double delayCounter = 0.0d;

        private const double initialDelay = 1.0d;   //  Wait 1 second before updating the camera parameters.

        private bool watchdogRun = false;

        private bool isSuborbital = false;

        public void Start ()
        {
            RSSSettings = GameDatabase.Instance.GetConfigNodes("REALSOLARSYSTEM").FirstOrDefault(n => n.HasNode("ClipPlanes"));

            GameEvents.onVesselSOIChanged.Add (OnVesselSOIChanged);
            GameEvents.onVesselSituationChange.Add (OnVesselSituationChanged);
        }

        public void OnDestroy ()
        {
            GameEvents.onVesselSOIChanged.Remove (OnVesselSOIChanged);
            GameEvents.onVesselSituationChange.Remove (OnVesselSituationChanged);
        }

        public void Update ()
        {
            if (RSSSettings == null)
            {
                return;
            }

            if (watchdogRun)
            {
                return;
            }

            delayCounter += Time.deltaTime;

            if (delayCounter < initialDelay)
            {
                return;
            }

            watchdogRun = true;

            Camera [] cameras = Camera.allCameras;

            string bodyName = FlightGlobals.getMainBody ().name;

            ConfigNode clipPlaneSettings = null;

            if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.Direct3D11 || SystemInfo.graphicsDeviceType == GraphicsDeviceType.Direct3D12)
            {
                clipPlaneSettings = RSSSettings.GetNode("ClipPlanes")?.GetNode("DX11");
            }
            else
            {
                clipPlaneSettings = RSSSettings.GetNode("ClipPlanes")?.GetNode("Other");
            }

            if (clipPlaneSettings == null)
            {
                return;
            }

            foreach (Camera cam in cameras)
            {
                float farClip = -1.0f;
                float nearClip = -1.0f;

                if (cam.name.Equals ("Camera 00"))
                {
                    clipPlaneSettings.TryGetValue ("cam00FarClip", ref farClip);

                    if (clipPlaneSettings.HasNode (bodyName))
                    {
                        clipPlaneSettings.GetNode (bodyName).TryGetValue ("cam00FarClip", ref farClip);
                    }

                    clipPlaneSettings.TryGetValue ("cam00NearClip", ref nearClip);

                    if (clipPlaneSettings.HasNode (bodyName))
                    {
                        clipPlaneSettings.GetNode (bodyName).TryGetValue ("cam00NearClip", ref nearClip);
                    }
                }
                else if (cam.name.Equals ("Camera 01"))
                {
                    clipPlaneSettings.TryGetValue ("cam01FarClip", ref farClip);

                    if (clipPlaneSettings.HasNode (bodyName))
                    {
                        clipPlaneSettings.GetNode (bodyName).TryGetValue ("cam01FarClip", ref farClip);
                    }

                    clipPlaneSettings.TryGetValue ("cam01NearClip", ref nearClip);

                    if (clipPlaneSettings.HasNode (bodyName))
                    {
                        clipPlaneSettings.GetNode (bodyName).TryGetValue ("cam01NearClip", ref nearClip);
                    }
                }
                else if (cam.name.Equals ("Camera ScaledSpace"))
                {
                    clipPlaneSettings.TryGetValue ("camScaledSpaceFarClip", ref farClip);

                    if (clipPlaneSettings.HasNode (bodyName))
                    {
                        clipPlaneSettings.GetNode (bodyName).TryGetValue ("camScaledSpaceFarClip", ref farClip);
                    }

                    clipPlaneSettings.TryGetValue ("camScaledSpaceNearClip", ref nearClip);

                    if (clipPlaneSettings.HasNode (bodyName))
                    {
                        clipPlaneSettings.GetNode (bodyName).TryGetValue ("camScaledSpaceNearClip", ref nearClip);
                    }
                }

                if (nearClip > 0.0f)
                {
                    cam.nearClipPlane = nearClip;

                    Debug.Log (string.Format ("[RealSolarSystem]: Watchdog setting camera {0} near clip to {1} so camera now has {2}", cam.name, nearClip, cam.nearClipPlane));
                }

                if (farClip > 0.0f)
                {
                    cam.farClipPlane = farClip;

                    Debug.Log (string.Format ("[RealSolarSystem]: Watchdog setting camera {0} far clip to {1} so camera now has {2}", cam.name, farClip, cam.farClipPlane));
                }
            }
        }

        void OnVesselSituationChanged (GameEvents.HostedFromToAction<Vessel, Vessel.Situations> data)
        {
            Vessel curVessel = data.host;

            if (!curVessel.mainBody.isHomeWorld || !curVessel.isActiveVessel)
            {
                return;
            }

            if (data.from == Vessel.Situations.FLYING && data.to == Vessel.Situations.SUB_ORBITAL)
            {
                isSuborbital = true;
            }
            else if (isSuborbital && data.to == Vessel.Situations.FLYING)
            {
                isSuborbital = false;

                Debug.Log ("[RealSolarSystem]: Calling StartUpSphere() to prevent missing PQS tiles...");

                curVessel.mainBody.pqsController.StartUpSphere ();
            }
        }

        public void OnVesselSOIChanged (GameEvents.HostedFromToAction<Vessel, CelestialBody> evt)
        {
            watchdogRun = false;

            delayCounter = 0.0d;
        }
    }
}
