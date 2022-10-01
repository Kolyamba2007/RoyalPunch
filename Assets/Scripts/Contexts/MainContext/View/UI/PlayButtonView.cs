using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;

public class PlayButtonView : View
{
    [SerializeField] private Button button;

    public Signal ClickSignal { get; } = new Signal();
    
    private void OnValidate()
    {
        button = GetComponent<Button>();
    }
    
    protected override void Start()
    {
        base.Start();

        button.onClick.AddListener(() =>
        {
            ClickSignal.Dispatch();
            gameObject.SetActive(false);
        });
    }
}
