using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_FrienPanel : UI_Base
{
    public string Name { get; set; }
    public int ID { get; set; }


    enum Texts
    {
        UserText
    }

    public override void Init()
    {
        Bind<Text>(typeof(Texts));

        if (string.IsNullOrEmpty(Name))
            return;

        Text text = GetText((int)Texts.UserText);
        text.text = Name;
    }

    public void RefreshUI()
    {
       // if (string.IsNullOrEmpty(Name))
       //     return;

       //Text text = GetText((int)Texts.UserText);
       // text.text = Name;
    }
}
