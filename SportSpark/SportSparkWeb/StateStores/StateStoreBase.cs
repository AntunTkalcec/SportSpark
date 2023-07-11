using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace SportSparkWeb.StateStores;

public class StateStoreBase<T> where T : class
{
    protected BehaviorSubject<T> state;
    public StateStoreBase() => state = new BehaviorSubject<T>(default!);
    public IObservable<T> Value => state.AsObservable();
}
