using System;
using System.IO;
using UnityEngine;

namespace RealSolarSystem
{
    [KSPAddon (KSPAddon.Startup.Instantly, true)]

    class InstallationCheck : MonoBehaviour
    {
        public void Start ()
        {
            try
            {
                CheckTexturesInstalled ();
            }
            catch (Exception exceptionStack)
            {
                Debug.Log (string.Format ("[RealSolarSystem]: InstallationCheck.Start() caught an exception: {0}", exceptionStack));
            }
            finally
            {
                Destroy (this);
            }
        }

        private void CheckTexturesInstalled ()
        {
            string szTextureFolderPath = string.Format ("{0}GameData{1}RSS-Textures", KSPUtil.ApplicationRootPath, Path.AltDirectorySeparatorChar);

            if (!Directory.Exists (szTextureFolderPath))
            {
                const string szUserMessage = "The texture pack for RealSolarSystem is missing from your installation!\n\nDownload it from the GitHub repository.";

                Debug.Log ("[RealSolarSystem]: No texture pack detected!");

                PopupDialog.SpawnPopupDialog
                (
                    new Vector2 (0.0f, 0.0f),
                    new Vector2 (0.0f, 0.0f),
                    new MultiOptionDialog
                    (
                        "RSSInstallationCheck", szUserMessage, "Missing RSS Texture Pack", HighLogic.UISkin, new Rect (0.25f, 0.75f, 320.0f, 80.0f), new DialogGUIFlexibleSpace (), new DialogGUIButton
                        (
                            "Download", delegate
                            {
                                OnOpenDownloadPage ();
                            },
                            140.0f, 30.0f, true
                        )
                    ),
                    false, HighLogic.UISkin, true, string.Empty
                );
            }
        }

        private void OnOpenDownloadPage ()
        {
            Application.OpenURL ("https://github.com/PhineasFreak/RealSolarSystem/releases/latest");
        }
    }
}
