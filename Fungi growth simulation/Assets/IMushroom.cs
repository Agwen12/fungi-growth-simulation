using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMushroom
{
    GameObject _gameObject { get; set; }
    IMushroom _parent { get; set; }
}
