using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class MenuHandler : MonoBehaviour {

    // dragged in editor
    public GameObject UpgradePanel;
    public GameObject OptionPanel;

    private bool Open = false;
    private Vector3 vecPosition;
    private float LerpAmount = 0f;

    public void OpenClose( ) => Open = !Open;

    (float start, float end) bounds;
    void Start( ) {

        if (UpgradePanel != null)
        {
            vecPosition = UpgradePanel.transform.localPosition;
            bounds = (vecPosition.x, vecPosition.x + 830);
        }

        if (OptionPanel != null)
        {
            vecPosition = OptionPanel.transform.localPosition;
            bounds = (vecPosition.x, vecPosition.x - 830);
        }
    }

    void Update( ) {

        if (UpgradePanel != null)
        {
            float difference = UpgradePanel.transform.localPosition.x - (Open ? bounds.end : bounds.start);
            float scale = (0.05f * (Mathf.Abs(difference) / 830f));
            LerpAmount = Mathf.Clamp01(LerpAmount + (Open ? scale : -scale));
            UpgradePanel.transform.localPosition = new(Mathf.Lerp(bounds.start, bounds.end, LerpAmount), vecPosition.y, vecPosition.z);
        }

        if (OptionPanel != null)
        {
            float difference = OptionPanel.transform.localPosition.x - (Open ? bounds.end : bounds.start);
            float scale = (0.05f * (Mathf.Abs(difference) / 830f));
            LerpAmount = Mathf.Clamp01(LerpAmount + (Open ? scale : -scale));
            OptionPanel.transform.localPosition = new(Mathf.Lerp(bounds.start, bounds.end, LerpAmount), vecPosition.y, vecPosition.z);
        }
    }
}
