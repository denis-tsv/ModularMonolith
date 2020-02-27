using System;
using System.Collections.Generic;

namespace Shop.Framework.Interfaces.Cancel
{
    public interface ICancelService
    {
        void AddCancel<TCancel, TCancelHandler>(string correlationId, TCancel cancel)
            where TCancel : ICancel
            where TCancelHandler : ICancelHandler<TCancel>;

        bool TryRemoveCancel(string correlationId, out ICollection<(Type CancelHandlerType, ICancel Cancel)> res);
    }
}
