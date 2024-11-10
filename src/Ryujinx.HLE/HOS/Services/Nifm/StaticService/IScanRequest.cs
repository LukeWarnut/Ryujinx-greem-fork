using Ryujinx.Common.Logging;
using Ryujinx.HLE.HOS.Ipc;
using Ryujinx.HLE.HOS.Kernel.Threading;
using Ryujinx.HLE.HOS.Services.Account.Acc.AsyncContext;
using Ryujinx.Horizon.Common;
using System;

namespace Ryujinx.HLE.HOS.Services.Nifm.StaticService
{
    class IScanRequest : IpcService
    {
        private readonly KEvent _event0;
        private readonly KEvent _event1;

        private int _event0Handle;
        private int _event1Handle;

        public IScanRequest(Horizon system)
        {
            _event0 = new KEvent(system.KernelContext);
            _event1 = new KEvent(system.KernelContext);
        }

        [CommandCmif(1)]
        // IsProcessing() -> b8
        public ResultCode IsProcessing(ServiceCtx context)
        {
            Logger.Info?.Print(LogClass.ServiceNifm, $"1");

            return ResultCode.Success;

        }

        [CommandCmif(3)]
        // GetSystemEventReadableHandle() -> handle<copy>
        public ResultCode GetSystemEventReadableHandle(ServiceCtx context)
        {
            if (context.Process.HandleTable.GenerateHandle(_event0.ReadableEvent, out int systemEventHandle) != Result.Success)
            {
                throw new InvalidOperationException("Out of handles!");
            }

            context.Response.HandleDesc = IpcHandleDesc.MakeCopy(systemEventHandle + 8);

            return ResultCode.Success;

        }

        [CommandCmif(4)]
        // SetChannels
        public ResultCode SetChannels(ServiceCtx context)
        {
            Logger.Info?.Print(LogClass.ServiceNifm, $"4");

            return ResultCode.Success;

        }
    }
}
