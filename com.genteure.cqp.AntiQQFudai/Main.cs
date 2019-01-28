using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace com.genteure.cqp.AntiQQFudai
{
    internal static class Main
    {
        internal const string APP_ID = "com.genteure.cqp.AntiQQFudai";

        private static string DB_File;
        private static long[] GroupList = { 95349372L, 627565437L, 423768065L, 549858724L };


        [DllExport("_eventEnable", CallingConvention.StdCall)]
        public static CoolQApi.Event ProcessEnable()
        {
            DB_File = CoolQApi.GetAppDirectory() + "db.txt";

            try
            {
                GroupList = File.ReadAllLines(CoolQApi.GetAppDirectory() + "group.txt").Select(long.Parse).ToArray();
            }
            catch (Exception ex)
            {
                CoolQApi.AddLog(CoolQApi.LogLevel.Warning, "群号初始化错误", ex.ToString());
            }


            return CoolQApi.Event.Ignore;
        }

        [DllExport("_eventGroupMsg", CallingConvention.StdCall)]
        public static CoolQApi.Event ProcessGroupMessage(int subType, int messageId, long fromGroup,
            long fromQQ, string fromAnonymous, string msg, int font)
        {
            try
            {
                if (!GroupList.Any(x => x == fromGroup))
                {
                    return CoolQApi.Event.Ignore;
                }

                if (msg == "收到福袋，请使用新版手机QQ查看")
                {
                    string qqstring = fromQQ.ToString();
                    if (File.ReadAllLines(DB_File).Any(x => x == qqstring))
                    {
                        // 文件里有这个人，踢出群
                        CoolQApi.SendGroupMsg(fromGroup, "禁止发QQ福袋。第二次触发，已自动踢出群。");
                        CoolQApi.SetGroupKick(fromGroup, fromQQ);
                    }
                    else
                    {
                        // 文件里没有这个人，警告并禁言
                        File.AppendAllLines(DB_File, new[] { qqstring });
                        CoolQApi.SendGroupMsg(fromGroup, "禁止发QQ福袋。第一次禁言，第二次自动踢出群。");
                        CoolQApi.SetGroupBan(fromGroup, fromQQ, 60 * 60); // 禁言 1 小时
                    }
                    Task.Run(async () =>
                    {
                        await Task.Delay(3000);
                        CoolQApi.DeleteMsg(messageId);
                    });
                }
                return CoolQApi.Event.Ignore;
            }
            catch (Exception ex)
            {
                CoolQApi.AddLog(CoolQApi.LogLevel.Error, "出错", ex.ToString());
                return CoolQApi.Event.Ignore;
            }
        }
    }
}
