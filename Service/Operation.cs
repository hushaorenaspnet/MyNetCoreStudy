using MyNetCoreStudy.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCoreStudy.Service
{
    public class Operation : IOperationTransient,
     IOperationScoped,
     IOperationSingleton,
     IOperationSingletonInstance
    {
        /// <summary>
        /// 构造方法中生成GUID
        /// </summary>
        public Operation() : this(Guid.NewGuid())
        {
        }

        public Operation(Guid id)
        {
            OperationId = id;
        }

        /// <summary>
        /// 获取GUID
        /// </summary>
        public Guid OperationId { get; private set; }
    }
}
