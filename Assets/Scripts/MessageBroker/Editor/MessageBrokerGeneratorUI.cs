using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace MessageBroker.Editor
{
    public class MessageBrokerGeneratorUI : EditorWindow
    {
        private static TaskCompletionSource<DialogResult> _tcs;

        private string uxmlFilePath;
        private VisualTreeAsset rootXML;

        [SerializeField] private VisualTreeAsset mVisualTreeAsset;

        private TextField txtNamespace;
        private TextField txtOutputFolder;
        private TextField txtCategoryPrefix;
        private TextField txtDefaultCategoryName;

        [MenuItem("Window/UI Toolkit/MessageBrokerGeneratorUI")]
        public static Task<DialogResult> ShowDialog()
        {
            _tcs = new TaskCompletionSource<DialogResult>();

            var wnd = GetWindow<MessageBrokerGeneratorUI>();
            wnd.titleContent = new GUIContent("Message Broker Generator UI");
            wnd.ShowModal();
            return _tcs.Task;
        }

        public void CreateGUI()
        {

            if (string.IsNullOrEmpty(uxmlFilePath))
            {
                var path = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this));
                path = $"{path.Remove(path.Length - 2)}uxml";
                uxmlFilePath = path;
            }

            // Load the UXML file.
            rootXML = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(uxmlFilePath);

            if (rootXML == null)
            {
                Debug.LogError($"UXML not found at: {uxmlFilePath}");
                return;
            }

            // Instantiate the UXML.
            var root = rootXML.Instantiate();

            // Add to the window
            rootVisualElement.Add(root);

            // Each editor window contains a root VisualElement object

            var btnStart = root.Q<Button>("btnStart");
            btnStart.clicked += BtnStartOnclicked;

            txtNamespace = root.Q<TextField>("txtNamespace");
            txtOutputFolder = root.Q<TextField>("txtOutputFolder");
            txtCategoryPrefix = root.Q<TextField>("txtCategoryPrefix");
            txtDefaultCategoryName = root.Q<TextField>("txtDefaultCategoryName");

        }

        private void BtnStartOnclicked()
        {
            if (CheckInputParameters() == false)
            {
                Debug.Log("Not all input parameters' checks are passed");
                return;
            }

            var result = new DialogResult()
            {
                Namespace = txtNamespace.text,
                OutputFolder = txtOutputFolder.text,
                CategoryPrefix = txtCategoryPrefix.text,
                DefaultCategoryName = txtDefaultCategoryName.text,
            };

            _tcs.SetResult(result);
            Close();
        }

        private bool CheckInputParameters()
        {
            var result = true;
            var namespaceRegex =
                new Regex("^([a-zA-Z_][a-zA-Z0-9_]*)(\\.[a-zA-Z_][a-zA-Z0-9_]*)*$"); // Regexp for namespaces

            if (!namespaceRegex.IsMatch(txtNamespace.text))
            {
                Debug.LogWarning("Namespace must contain only letters, numbers, spaces, and underscores.");
                result = false;
            }

            var outputFolderRegex = new Regex("^([a-zA-Z0-9 _\\-\\.]+\\/)*[a-zA-Z0-9 _\\-\\.]+\\/?$");
            if (!outputFolderRegex.IsMatch(txtOutputFolder.text))
            {
                Debug.LogWarning("Output folder must be a valid relative path.");
                result = false;
            }

            var categoryPrefixRegex = new Regex("^[a-zA-Z]{2,4}$");
            if (!categoryPrefixRegex.IsMatch(txtCategoryPrefix.text))
            {
                Debug.LogWarning("Category prefix must be from 2 to 4 letters long.");
                result = false;
            }

            var defaultCategoryNameRegex = new Regex("^[a-zA-Z]{3,40}$");
            if (!defaultCategoryNameRegex.IsMatch(txtDefaultCategoryName.text))
            {
                Debug.LogWarning("Default category name must be from 3 to 40 letters long.");
                result = false;
            }

            return result;
        }

        public class DialogResult
        {
            public string Namespace;
            public string OutputFolder;
            public string CategoryPrefix;
            public string DefaultCategoryName;
        }
    }
}