using System.Runtime.InteropServices;

namespace com.genteure.cqp.AntiQQFudai
{
    internal static partial class CoolQApi
    {
        private static class NativeMethods
        {
            [DllExport("AppInfo")]
            public static string AppInfo() => "9," + Main.APP_ID;

            [DllExport("Initialize", CallingConvention.StdCall)]
            public static int Initialize(int i) { ac = i; return 0; }


            [DllImport("CQP.DLL")]
            public static extern int CQ_sendPrivateMsg(int AuthCode, long QQID, string Message);

            [DllImport("CQP.DLL")]
            public static extern int CQ_sendGroupMsg(int AuthCode, long GroupID, string Message);

            [DllImport("CQP.DLL")]
            public static extern int CQ_sendDiscussMsg(int AuthCode, long DiscussID, string Message);

            [DllImport("CQP.DLL")]
            public static extern int CQ_sendLike(int AuthCode, long QQID);

            [DllImport("CQP.DLL")]
            public static extern int CQ_setGroupKick(int AuthCode, long GroupID, long QQID, bool NeverAllowAgain = false);

            [DllImport("CQP.DLL")]
            public static extern int CQ_setGroupBan(int AuthCode, long GroupID, long QQID, long Seconds);

            [DllImport("CQP.DLL")]
            public static extern int CQ_setGroupAdmin(int AuthCode, long GroupID, long QQID, bool isAdmin);

            [DllImport("CQP.DLL")]
            public static extern int CQ_setGroupWholeBan(int AuthCode, long GroupID, bool isBan);

            [DllImport("CQP.DLL")]
            public static extern int CQ_setGroupAnonymousBan(int AuthCode, long GroupID, string AnomymousID, long Seconds);

            [DllImport("CQP.DLL")]
            public static extern int CQ_setGroupAnonymous(int AuthCode, long GroupID, bool isEnable);

            [DllImport("CQP.DLL")]
            public static extern int CQ_setGroupCard(int AuthCode, long GroupID, long QQID, string NewName);

            [DllImport("CQP.DLL")]
            public static extern int CQ_setGroupLeave(int AuthCode, long GroupID, bool isDisband);

            [DllImport("CQP.DLL")]
            public static extern int CQ_setGroupSpecialTitle(int AuthCode, long GroupID, long QQID, string Title, long Seconds);

            [DllImport("CQP.DLL")]
            public static extern int CQ_setDiscussLeave(int AuthCode, long DiscussID);

            [DllImport("CQP.DLL")]
            public static extern int CQ_setFriendAddRequest(int AuthCode, string ResponseFlag, Request Operation, string Remark);

            [DllImport("CQP.DLL")]
            public static extern int CQ_setGroupAddRequestV2(int AuthCode, string ResponseFlag, Request Type, Request Operation, string Reason);

            [DllImport("CQP.DLL")]
            public static extern string CQ_getGroupMemberInfoV2(int AuthCode, long GroupID, long QQID, bool NoCache);

            [DllImport("CQP.DLL")]
            public static extern string CQ_getStrangerInfo(int AuthCode, long QQID, bool NoCache);

            [DllImport("CQP.DLL")]
            public static extern int CQ_addLog(int AuthCode, LogLevel Priority, string Category, string Content);

            [DllImport("CQP.DLL")]
            public static extern string CQ_getCookies(int AuthCode);

            [DllImport("CQP.DLL")]
            public static extern int CQ_getCsrfToken(int AuthCode);

            [DllImport("CQP.DLL")]
            public static extern int CQ_getLoginQQ(int AuthCode);

            [DllImport("CQP.DLL")]
            public static extern string CQ_getLoginNick(int AuthCode);

            [DllImport("CQP.DLL")]
            public static extern string CQ_getAppDirectory(int AuthCode);

            [DllImport("CQP.DLL")]
            public static extern int CQ_setFatal(int AuthCode, string ErrorInfo);

            [DllImport("CQP.DLL")]
            public static extern string CQ_getRecord(int AuthCode, string File, string Format);

            [DllImport("CQP.DLL")]
            public static extern int CQ_deleteMsg(int AuthCode, int messageId);

            // [DllImport("CQP.DLL")]
            // public static extern int CQ______________(int AuthCode, long someshit);

        }

        // -------------
    }

}
