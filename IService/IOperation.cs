using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCoreStudy.IService
{
    public interface IOperation
    {
        Guid OperationId { get; }
    }
    //用于演示暂时生存期
    public interface IOperationTransient : IOperation
    {
    }
    //用于演示作用域生存期
    public interface IOperationScoped : IOperation
    {
    }
    //用于演示单例生存期
    public interface IOperationSingleton : IOperation
    {
    }
    //用于演示单例中空GUID
    public interface IOperationSingletonInstance : IOperation
    {
    }
}
