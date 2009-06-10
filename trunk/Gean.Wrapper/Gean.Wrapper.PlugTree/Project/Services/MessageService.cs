using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.PlugTree.Service
{
    public static class MessageService
    {
        public delegate void ShowErrorDelegate(Exception ex, string message);

        public static ShowErrorDelegate CustomErrorReporter { get; set; }

        public static void ShowError(Exception ex)
        {
            ShowError(ex, null);
        }

        public static void ShowError(string message)
        {
            ShowError(null, message);
        }

        public static void ShowError(Exception ex, string message)
        {
            if (message == null) message = string.Empty;

            if (ex != null)
            {
                LogService.Error(message, ex);
                LogService.Warn("Stack trace of last error log:\n" + Environment.StackTrace);
                //if (CustomErrorReporter != null)
                //{
                //    CustomErrorReporter(ex, message);
                //    return;
                //}
            }
            else
            {
                LogService.Error(message);
            }
        }

        public static void ShowWarning(string message)
        {
            LogService.Warn(message);
        }

        public static int ShowCustomDialog(string caption, string dialogText, int acceptButtonIndex, int cancelButtonIndex, params string[] buttontexts)
        {
            throw new NotImplementedException();
        }

        public static int ShowCustomDialog(string caption, string dialogText, params string[] buttontexts)
        {
            throw new NotImplementedException();
        }

        public static void ShowMessage(string message)
        {
            throw new NotImplementedException();
        }

        public static void ShowMessage(string message, string caption)
        {
            LogService.Info(message);
        }

        internal static void InformSaveError(string fileName, string message, string p, Exception e)
        {
            throw new NotImplementedException();
        }

    }
}
