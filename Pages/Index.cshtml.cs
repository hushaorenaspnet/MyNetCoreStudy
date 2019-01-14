using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyNetCoreStudy.Service;
using MyNetCoreStudy.IService;
using Microsoft.Extensions.Logging;

namespace MyNetCoreStudy.Pages
{
    public class IndexModel : PageModel
    {
        public OperationService OperationService { get; }
        public IOperationTransient TransientOperation { get; }
        public IOperationScoped ScopedOperation { get; }
        public IOperationSingleton SingletonOperation { get; }
        public IOperationSingletonInstance SingletonInstanceOperation { get; }
   
        public IndexModel(
        OperationService operationService,
        IOperationTransient transientOperation,
        IOperationScoped scopedOperation,
        IOperationSingleton singletonOperation,
        IOperationSingletonInstance singletonInstanceOperation)
        {
            OperationService = operationService;
            TransientOperation = transientOperation;
            ScopedOperation = scopedOperation;
            SingletonOperation = singletonOperation;
            SingletonInstanceOperation = singletonInstanceOperation;
        }

        public string BindGUIDMsg { get; set; }
        public void OnGet()
        {
            BindGUIDMsg += "控制器操作: <br/> ";
            BindGUIDMsg += "暂时性:" + TransientOperation.OperationId.ToString() + "</br>";
            BindGUIDMsg += "有作用域:" + ScopedOperation.OperationId.ToString() + "</br>";
            BindGUIDMsg += "单一实例:" + SingletonOperation.OperationId.ToString() + "</br>";
            BindGUIDMsg += "实例:" + SingletonInstanceOperation.OperationId.ToString() + "</br>";

            BindGUIDMsg += "</br></br></br>OperationService操作:</br>";
            BindGUIDMsg += "暂时性:" + OperationService.TransientOperation.OperationId.ToString() + "</br>";
            BindGUIDMsg += "有作用域:" + OperationService.ScopedOperation.OperationId.ToString() + "</br>";
            BindGUIDMsg += "单一实例:" + OperationService.SingletonOperation.OperationId.ToString() + "</br>";
            BindGUIDMsg += "实例:" + OperationService.SingletonInstanceOperation.OperationId.ToString() + "</br>";
        }
    }
}
