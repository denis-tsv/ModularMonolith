using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Framework.Interfaces.Cancel
{
    public interface ICancelService
    {
        void AddCancel<TCancel>(TCancel cancel)
            where TCancel : ICancel;
            

        Task CancelAllAsync();
    }
}
