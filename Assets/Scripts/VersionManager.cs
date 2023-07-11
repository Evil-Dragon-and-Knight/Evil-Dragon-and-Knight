using TMPro;
using UnityEngine;

public class VersionManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI versionTextField;
    [SerializeField] private string versionText = "v{VERSION}";

    private void Start()
    {
        versionTextField.text = versionText
            .Replace("{VERSION}", Application.version)
            .Replace("{UNITY_VERSION}", Application.unityVersion)
            .Replace("{GUID}", Application.buildGUID);
    }
}
