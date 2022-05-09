using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMushroom
{
    GameObject GameObject { get; set; }
    IMushroom Parent { get; set; }
}
