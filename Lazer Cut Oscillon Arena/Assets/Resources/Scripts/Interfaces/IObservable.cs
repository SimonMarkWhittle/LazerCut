using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObservable {

    void PingObservers(Dictionary<string, object> qwargs);

    void AddObserver(IObserver _observer);

    bool RemoveObserver(IObserver _observer);
}
