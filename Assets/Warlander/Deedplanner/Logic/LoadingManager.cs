using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Warlander.Deedplanner.Data;

namespace Warlander.Deedplanner.Logic
{
    public class LoadingManager : MonoBehaviour
    {

        [SerializeField] private GameObject splashRoot;
        [SerializeField] private GameObject managersRoot;
        [SerializeField] private MultiCamera[] cameras;

        [SerializeField] private TMP_Text text;
        [SerializeField] private Slider loadingBar;
        [SerializeField] private Animator fadeAnimator;
        
        private void Start()
        {
            splashRoot.SetActive(true);
            StartCoroutine(Load());
        }

        private IEnumerator Load()
        {
            text.text = "Loading database";
            loadingBar.value = 0.0f;
            
            Debug.Log("Loading data");
            yield return DataLoader.LoadData();
            yield return null;
            Debug.Log("Data loaded");

            text.text = "Creating map";
            loadingBar.value = 0.5f;
            
            Debug.Log("Creating map");
            GameManager.Instance.CreateNewMap(25, 25);
            yield return null;
            Debug.Log("Map created");
            
            text.text = "Loading complete";
            loadingBar.value = 1.0f;
            
            managersRoot.SetActive(true);
            foreach (MultiCamera multiCamera in cameras)
            {
                multiCamera.enabled = true;
            }
            fadeAnimator.enabled = true;
            Destroy(gameObject);
        }

    }
}