using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonView : View
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
            Disable();
        });
    }
    
    public void Enable()
    {
        gameObject.SetActive(true);
    }
    
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
