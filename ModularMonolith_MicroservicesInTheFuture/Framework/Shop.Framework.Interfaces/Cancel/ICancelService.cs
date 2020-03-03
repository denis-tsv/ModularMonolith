using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Framework.Interfaces.Cancel
{
    public interface ICancelService
    {
        void AddCancel<TCancel, TCancelHandler>(TCancel cancel)
            where TCancel : ICancel
            where TCancelHandler : ICancelHandler<TCancel>;

        Task CancelAllAsync();
    }
}
