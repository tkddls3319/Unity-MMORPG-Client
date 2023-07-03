using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameSceneBag : UI_Scene
{
    public UI_Message MessageUI { get; private set; }
    // Start is called before the first frame update
    public override void Init()
    {
        base.Init();

        MessageUI = GetComponentInChildren<UI_Message>();
        MessageUI.gameObject.SetActive(true);
    }
}
