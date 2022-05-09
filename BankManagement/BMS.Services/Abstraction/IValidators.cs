using System.Collections.Generic;

namespace BMS.Services.Abstraction
{
    public interface IValidators<TData>
    {
        IEnumerable<string> Validate(TData data);
    }
}
