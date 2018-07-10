using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver {

    void Ping(Dictionary<string, object> quargs);

}
