using LibHac.Ns;
using Ryujinx.Common.Utilities;
using System;
using System.Linq;

namespace Ryujinx.HLE.HOS.Services.Ns
{
    [Service("ns:am")]
    class IApplicationManagerInterface : IpcService
    {
        public IApplicationManagerInterface(ServiceCtx context) { }

        [CommandCmif(0)]
        // ListApplicationRecord(unknown<4>) -> (unknown<4>, buffer<unknown, 6>)
        public ResultCode ListApplicationRecord(ServiceCtx context)
        {
            // input (unknown<4>)
            string hex = "01008CF01BAAC00003020000000000000000000000000000";
            // this returns 1322173
            //int intValue = int.Parse(hex, System.Globalization.NumberStyles.HexNumber);

            byte[] record = Enumerable.Range(0, hex.Length)
                     .Where(x => x % 2 == 0)
                     .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                     .ToArray();

            //01008CF01BAAC000 EoW

            context.Memory.Write(context.Request.ReceiveBuff[0].Position, record);

            return ResultCode.Success;
        }

        [CommandCmif(400)]
        // GetApplicationControlData(u8, u64) -> (unknown<4>, buffer<unknown, 6>)
        public ResultCode GetApplicationControlData(ServiceCtx context)
        {
#pragma warning disable IDE0059 // Remove unnecessary value assignment
            byte source = (byte)context.RequestData.ReadInt64();
            ulong titleId = context.RequestData.ReadUInt64();
#pragma warning restore IDE0059

            ulong position = context.Request.ReceiveBuff[0].Position;

            ApplicationControlProperty nacp = context.Device.Processes.ActiveApplication.ApplicationControlProperties;

            context.Memory.Write(position, SpanHelpers.AsByteSpan(ref nacp).ToArray());

            return ResultCode.Success;
        }
    }
}
