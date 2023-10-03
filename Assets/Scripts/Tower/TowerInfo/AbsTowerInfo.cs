using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsTowerInfo : MonoBehaviour
{
    [SerializeField] protected Sprite _icon;
    [SerializeField] protected string _label;
    [SerializeField] protected int _price;
    [SerializeField] protected int _upgradePrice;
    [SerializeField] protected float _firingRadius;
    [SerializeField] protected string _description;

    public Sprite Icon { get; }
    public string Label { get; }
    public int Price { get; }
    public int UpgradePrice { get; }
    public float FiringRadius { get; }
    public string Description { get; }
}
