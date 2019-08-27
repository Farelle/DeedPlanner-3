﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Warlander.Deedplanner.Logic;

namespace Warlander.Deedplanner.Gui
{
    public class MainMenuManager : MonoBehaviour
    {

        [SerializeField] private Button fullscreenButton = null;
        [SerializeField] private Button quitButton = null;

        [SerializeField] private TMP_Text versionText = null;

        private void Start()
        {
            bool mobile = Application.isMobilePlatform;
            bool web = Application.platform == RuntimePlatform.WebGLPlayer;

            if (mobile || web)
            {
                quitButton.gameObject.SetActive(false);
            }

            if (mobile)
            {
                fullscreenButton.gameObject.SetActive(false);
            }

            versionText.text = Constants.TitleString;
        }
        
        public void OnResizeMap()
        {
            GuiManager.Instance.ResizeMapWindow.gameObject.SetActive(true);
        }

        public void OnClearMap()
        {
            GuiManager.Instance.ClearMapWindow.gameObject.SetActive(true);
        }

        public void OnSaveMap()
        {
            GuiManager.Instance.SaveMapWindow.gameObject.SetActive(true);
        }

        public void OnLoadMap()
        {
            GuiManager.Instance.LoadMapWindow.gameObject.SetActive(true);
        }

        public void OnGraphicsSettings()
        {
            GuiManager.Instance.GraphicsSettingsWindow.gameObject.SetActive(true);
        }

        public void OnInputSettings()
        {
            GuiManager.Instance.InputSettingsWindow.gameObject.SetActive(true);
        }

        public void OnAbout()
        {
            GuiManager.Instance.AboutWindow.gameObject.SetActive(true);
        }

        public void OnToggleFullscreen()
        {
            if (Screen.fullScreen)
            {
                Screen.fullScreen = false;
            }
            else
            {
                // makes sure fullscreen mode always uses intended fullscreen window mode instead of native window or other fullscreen mode saved in user settings
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, FullScreenMode.FullScreenWindow);
            }
        }

        public void OnQuit()
        {
            // TODO: add auto-saving before quit logic
            Properties.Instance.SaveProperties();
            Application.Quit();
        }

        public void OnPatreon()
        {
            Application.OpenURL("https://www.patreon.com/warlander");
        }

        public void OnPaypal()
        {
            Application.OpenURL("https://www.paypal.me/MCyranowicz/10eur");
        }

    }
}
