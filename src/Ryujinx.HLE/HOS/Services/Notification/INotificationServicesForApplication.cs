namespace Ryujinx.HLE.HOS.Services.Notification
{
    [Service("notif:a")] // 9.0.0+
    class INotificationServicesForApplication : IpcService
    {
        private int AlarmSettingsCount;

        public INotificationServicesForApplication(ServiceCtx context)
        {
            AlarmSettingsCount = 0;
        }

        [CommandCmif(520)]
        public ResultCode ListAlarmSettings(ServiceCtx context)
        {
            context.ResponseData.Write(AlarmSettingsCount);

            return ResultCode.Success;
        }
    }
}
