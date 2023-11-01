using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class RadialMenuSection 
{
    public Sprite icon = null;
    public SpriteRenderer iconRenderer = null;
    public UnityEvent onPress= new UnityEvent();
}
