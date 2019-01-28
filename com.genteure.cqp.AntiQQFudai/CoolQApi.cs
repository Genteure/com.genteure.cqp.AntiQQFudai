using System;
using System.Linq;
using System.Text;

namespace com.genteure.cqp.AntiQQFudai
{
    internal static partial class CoolQApi
    {
        private static int ac;


        #region - Tools  -

        public static string Encode(string str, bool comma = true)
            => comma ?
                str.Replace("&", "&amp;").Replace("[", "&#91;").Replace("]", "&#93;").Replace(",", "&#44;") :
                str.Replace("&", "&amp;").Replace("[", "&#91;").Replace("]", "&#93;");
        public static string Decode(string str)
            => str.Replace("&amp;", "&").Replace("&#91;", "[").Replace("&#93;", "]").Replace("&#44", ",");

        public static string CQC_At(long QQID)
            => $@"[CQ:at,qq={(QQID == -1 ? "all" : QQID.ToString())}]";

        public static string CQC_Emoji(long ID)
            => $@"[CQ:emoji,id={ID}]";

        public static string CQC_Face(long ID)
            => $@"[CQ:face,id={ID}]";

        public static string CQC_BFace(long ID)
            => $@"[CQ:bface,id={ID}]";

        public static string CQC_Shake()
            => $@"[CQ:shake]";

        public static string CQC_Record(string file, bool magic)
            => $@"[CQ:record,file={Encode(file)}{(magic ? ",magic=true" : "")}]";

        public static string CQC_RPS(int ID)
            => $@"[CQ:rps,id={ID}]";

        public static string CQC_Dice(int ID)
            => $@"[CQ:dice,id={ID}]";

        public static string CQC_Share(string url, string title, string content, string image)
            => $@"[CQ:share,url={Encode(url)},title={Encode(title)},content={Encode(content)},image={Encode(image)}]";

        public static string CQC_Contact(string type, long ID)
            => $@"[CQ:contact,type={type},id={ID}]";

        public static string CQC_Anonymous(bool force = true)
            => $@"[CQ:anonymous{(force ? "" : "ignore")}]";

        public static string CQC_Image(string file)
            => $@"[CQ:image,file={Encode(file)}]";

        public static string CQC_Music(string source, long musicID, bool newstyle = true)
            => $@"[CQ:music.type={source},id={musicID}{(newstyle ? ",style=1" : "")}]";

        public static string CQC_MusicCustom(string url, string audio, string title, string content, string image)
            => $@"[CQ:music,type=custom,url={Encode(url)},audio={Encode(audio)}" +
            (title == "" ? "" : ",title=" + Encode(title)) +
            (content == "" ? "" : ",content=" + Encode(content)) +
            (image == "" ? "" : ",image=" + Encode(image)) +
            "]";

        public static string CQC_Location(float latitude, float longitude, long zoom, string title, string address)
            => $@"[CQ:location,lat={latitude},lon={longitude}" +
            (zoom > 0 ? ",zoom=" + zoom : "") +
            $@",title={Encode(title)},content={Encode(address)}]";

        #endregion

        #region - API -

        /// <summary>
        /// 发送私聊消息
        /// </summary>
        /// <param name="QQID">QQ号</param>
        /// <param name="Message">消息内容</param>
        /// <returns></returns>
        public static int SendPrivateMsg(long QQID, string Message) => NativeMethods.CQ_sendPrivateMsg(ac, QQID, Message);

        /// <summary>
        /// 发送群聊消息
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="Message">消息内容</param>
        /// <returns></returns>
        public static int SendGroupMsg(long GroupID, string Message) => NativeMethods.CQ_sendGroupMsg(ac, GroupID, Message);

        /// <summary>
        /// 发送讨论组消息
        /// </summary>
        /// <param name="DiscussID">讨论组ID</param>
        /// <param name="Message">消息内容</param>
        /// <returns></returns>
        public static int SendDiscussMsg(long DiscussID, string Message) => NativeMethods.CQ_sendDiscussMsg(ac, DiscussID, Message);

        /// <summary>
        /// 发送赞
        /// </summary>
        /// <param name="QQID">QQ号</param>
        /// <returns></returns>
        public static int SendLike(long QQID) => NativeMethods.CQ_sendLike(ac, QQID);

        /// <summary>
        /// 踢出群成员
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="QQID">QQ号</param>
        /// <param name="NeverAllowAgain">不再接收加群申请</param>
        /// <returns></returns>
        public static int SetGroupKick(long GroupID, long QQID, bool NeverAllowAgain = false) => NativeMethods.CQ_setGroupKick(ac, GroupID, QQID, NeverAllowAgain);

        /// <summary>
        /// 禁言群成员
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="QQID">QQ号</param>
        /// <param name="Seconds">禁言秒数</param>
        /// <returns></returns>
        public static int SetGroupBan(long GroupID, long QQID, long Seconds) => NativeMethods.CQ_setGroupBan(ac, GroupID, QQID, Seconds);

        /// <summary>
        /// 设置群管理
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="QQID">QQ号</param>
        /// <param name="isAdmin">是否为管理员</param>
        /// <returns></returns>
        public static int SetGroupAdmin(long GroupID, long QQID, bool isAdmin) => NativeMethods.CQ_setGroupAdmin(ac, GroupID, QQID, isAdmin);

        /// <summary>
        /// 群全员禁言
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="isBan">是否禁言状态</param>
        /// <returns></returns>
        public static int SetGroupWholeBan(long GroupID, bool isBan) => NativeMethods.CQ_setGroupWholeBan(ac, GroupID, isBan);

        /// <summary>
        /// 禁言匿名成员
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="AnomymousID">匿名标识</param>
        /// <param name="Seconds">禁言秒数</param>
        /// <returns></returns>
        public static int SetGroupAnonymousBan(long GroupID, string AnomymousID, long Seconds) => NativeMethods.CQ_setGroupAnonymousBan(ac, GroupID, AnomymousID, Seconds);

        /// <summary>
        /// 设置匿名状态
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="isEnable">是否启用</param>
        /// <returns></returns>
        public static int SetGroupAnonymous(long GroupID, bool isEnable) => NativeMethods.CQ_setGroupAnonymous(ac, GroupID, isEnable);

        /// <summary>
        /// 设置群名片
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="QQID">QQ号</param>
        /// <param name="NewName">群名片</param>
        /// <returns></returns>
        public static int SetGroupCard(long GroupID, long QQID, string NewName) => NativeMethods.CQ_setGroupCard(ac, GroupID, QQID, NewName);

        /// <summary>
        /// 退出群
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="isDisband">是否解散群</param>
        /// <returns></returns>
        public static int SetGroupLeave(long GroupID, bool isDisband) => NativeMethods.CQ_setGroupLeave(ac, GroupID, isDisband);

        /// <summary>
        /// 设置专属头衔
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="QQID">QQ号</param>
        /// <param name="Title">头衔 留空取消</param>
        /// <param name="Seconds">有效秒数</param>
        /// <returns></returns>
        public static int SetGroupSpecialTitle(long GroupID, long QQID, string Title, long Seconds) => NativeMethods.CQ_setGroupSpecialTitle(ac, GroupID, QQID, Title, Seconds);

        /// <summary>
        /// 退出讨论组
        /// </summary>
        /// <param name="DiscussID">讨论组ID</param>
        /// <returns></returns>
        public static int SetDiscussLeave(long DiscussID) => NativeMethods.CQ_setDiscussLeave(ac, DiscussID);

        /// <summary>
        /// 处理加好友请求
        /// </summary>
        /// <param name="ResponseFlag">标识</param>
        /// <param name="Operation">操作</param>
        /// <param name="Remark">好友备注</param>
        /// <returns></returns>
        public static int SetFriendAddRequest(string ResponseFlag, Request Operation, string Remark) => NativeMethods.CQ_setFriendAddRequest(ac, ResponseFlag, Operation, Remark);

        /// <summary>
        /// 处理加群请求
        /// </summary>
        /// <param name="ResponseFlag">标识</param>
        /// <param name="Type">类型</param>
        /// <param name="Operation">操作</param>
        /// <param name="Reason">理由</param>
        /// <returns></returns>
        public static int SetGroupAddRequestV2(string ResponseFlag, Request Type, Request Operation, string Reason) => NativeMethods.CQ_setGroupAddRequestV2(ac, ResponseFlag, Type, Operation, Reason);

        /// <summary>
        /// 获取群成员信息
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="QQID">QQ号</param>
        /// <param name="NoCache">不使用缓存</param>
        /// <returns></returns>
        public static string GetGroupMemberInfoV2(long GroupID, long QQID, bool NoCache = false) => NativeMethods.CQ_getGroupMemberInfoV2(ac, GroupID, QQID, NoCache);

        /// <summary>
        /// 获取陌生人信息
        /// </summary>
        /// <param name="QQID">QQ号</param>
        /// <param name="NoCache">不使用缓存</param>
        /// <returns></returns>
        public static string GetStrangerInfo(long QQID, bool NoCache = false) => NativeMethods.CQ_getStrangerInfo(ac, QQID, NoCache);

        /// <summary>
        /// 打日志
        /// </summary>
        /// <param name="Priority">日志级别</param>
        /// <param name="Category">分类</param>
        /// <param name="Content">内容</param>
        /// <returns></returns>
        public static int AddLog(LogLevel Priority, string Category, string Content) => NativeMethods.CQ_addLog(ac, Priority, Category, Content);

        /// <summary>
        /// 获取 Cookies
        /// </summary>
        /// <returns></returns>
        public static string GetCookies() => NativeMethods.CQ_getCookies(ac);

        /// <summary>
        /// 获取 CSRF Token
        /// </summary>
        /// <returns></returns>
        public static int GetCsrfToken() => NativeMethods.CQ_getCsrfToken(ac);

        /// <summary>
        /// 获取当前登录的 QQ 号
        /// </summary>
        /// <returns></returns>
        public static int GetLoginQQ() => NativeMethods.CQ_getLoginQQ(ac);

        /// <summary>
        /// 获取当前登录的账号昵称
        /// </summary>
        public static string GetLoginNick() => NativeMethods.CQ_getLoginNick(ac);

        /// <summary>
        /// 获取数据储存文件夹路径
        /// </summary>
        /// <returns></returns>
        public static string GetAppDirectory() => NativeMethods.CQ_getAppDirectory(ac);

        /// <summary>
        /// 丢出严重错误
        /// </summary>
        /// <param name="ErrorInfo">错误信息</param>
        /// <returns></returns>
        public static int SetFatal(string ErrorInfo) => NativeMethods.CQ_setFatal(ac, ErrorInfo);

        /// <summary>
        /// 获取语音消息文件
        /// </summary>
        /// <param name="File">文件ID</param>
        /// <param name="Format">期望文件格式</param>
        /// <returns></returns>
        public static string GetRecord(string File, string Format) => NativeMethods.CQ_getRecord(ac, File, Format);

        /// <summary>
        /// 撤回消息
        /// </summary>
        /// <param name="MessageId">消息ID</param>
        /// <returns></returns>
        public static int DeleteMsg(int MessageId) => NativeMethods.CQ_deleteMsg(ac, MessageId);

        #endregion

        /// <summary>
        /// 语音消息音频格式
        /// </summary>
        internal struct RecordFormat
        {
            private const string MP3 = "mp3";
            private const string AMR = "amr";
            private const string WMA = "wma";
            private const string M4A = "m4a";
            private const string SPX = "spx";
            private const string OGG = "ogg";
            private const string WAV = "wav";
            private const string FLAC = "flac";
        }

        /// <summary>
        /// 事件
        /// </summary>
        internal enum Event : Int32
        {
            /// <summary>
            /// 忽略
            /// </summary>
            Ignore = 0,
            /// <summary>
            /// 截拦
            /// </summary>
            Block = 1
        }

        /// <summary>
        /// 请求
        /// </summary>
        internal enum Request : Int32
        {
            /// <summary>
            /// 通过
            /// </summary>
            Allow = 1,
            /// <summary>
            /// 拒绝
            /// </summary>
            Deny = 2,
            /// <summary>
            /// 群添加
            /// </summary>
            GroupAdd = 1,
            /// <summary>
            /// 群邀请
            /// </summary>
            GourpInvite = 2
        }

        /// <summary>
        /// 酷Q日志记录等级
        /// </summary>
        internal enum LogLevel : Int32
        {
            /// <summary>
            /// 调试 灰色
            /// </summary>
            Debug = 0,
            /// <summary>
            /// 信息 黑色
            /// </summary>
            Info = 10,
            /// <summary>
            /// 信息(成功) 紫色
            /// </summary>
            InfoSuccess = 11,
            /// <summary>
            /// 信息(接收) 蓝色
            /// </summary>
            InfoRecv = 12,
            /// <summary>
            /// 信息(发送) 绿色
            /// </summary>
            InfoSend = 13,
            /// <summary>
            /// 警告 橙色
            /// </summary>
            Warning = 20,
            /// <summary>
            /// 错误 红色
            /// </summary>
            Error = 30,
            /// <summary>
            /// 致命错误 深红
            /// </summary>
            Fatal = 40
        }


        private class Unpack
        {
            private readonly byte[] _source;
            private int _location = 0;
            public Unpack(byte[] source) => _source = source;
            public byte[] GetAll() => _source.SubArray(_location, _source.Length - _location);
            public int Len() => _source.Length - _location;
            public byte[] GetBin(int len) { if (len <= 0) { return null; } _location += len; return _source.SubArray(_location, len); }
            public byte GetByte() { _location += 1; return (byte)_source.SubArray(_location, 1).GetValue(0); }
            public int GetInt() { _location += 4; return _source.SubArray(_location, 4).ToInt(); }
            public long GetLong() { _location += 8; return _source.SubArray(_location, 8).ToLong(); }
            public short GetShort() { _location += 2; return _source.SubArray(_location, 2).ToShort(); }
            public string GetLenStr() { try { return Encoding.GetEncoding("GB2312").GetString(GetBin(GetShort())); } catch { return ""; } }
            public byte[] GetToken() { return GetBin(GetShort()); }
        }

        private static bool ConvertAnsiHexToGroupMemberInfo(byte[] source, ref GroupMemberInfo gm)
        {
            if (source == null || source.Length < 40)
            {
                return false;
            }

            var u = new Unpack(source);
            gm.GroupId = u.GetLong();
            gm.Number = u.GetLong();
            gm.NickName = u.GetLenStr();
            gm.InGroupName = u.GetLenStr();
            gm.Gender = u.GetInt() == 0 ? "男" : " 女";
            gm.Age = u.GetInt();
            gm.Area = u.GetLenStr();
            gm.JoinTime = new DateTime(1970, 1, 1, 0, 0, 0).ToLocalTime().AddSeconds(u.GetInt());
            gm.LastSpeakingTime = new DateTime(1970, 1, 1, 0, 0, 0).ToLocalTime().AddSeconds(u.GetInt());
            gm.Level = u.GetLenStr();
            var manager = u.GetInt();
            gm.Authority = manager == 3 ? "群主" : (manager == 2 ? "管理员" : "成员");
            gm.HasBadRecord = (u.GetInt() == 1);
            gm.Title = u.GetLenStr();
            gm.TitleExpirationTime = u.GetInt();
            gm.CanModifyInGroupName = (u.GetInt() == 1);
            return true;
        }

        /// <summary>
        /// 表示一个群成员的信息。
        /// </summary>
        public sealed class GroupMemberInfo
        {
            /// <summary>
            /// 此群成员在其个人资料上所填写的年龄。
            /// </summary>
            /// <returns></returns>
            public int Age { get; set; }

            /// <summary>
            /// 此群成员在其个人资料上所填写的区域。
            /// </summary>
            /// <returns></returns>
            public string Area { get; set; }

            /// <summary>
            /// 此群成员的身份。
            /// </summary>
            /// <returns></returns>
            public string Authority { get; set; }

            /// <summary>
            /// 指示此群成员是否能够修改所有群成员名片的值。
            /// </summary>
            /// <returns></returns>
            public bool CanModifyInGroupName { get; set; }

            /// <summary>
            /// 此群成员在其个人资料上所填写的性别。
            /// </summary>
            /// <returns></returns>
            public string Gender { get; set; }

            /// <summary>
            /// 此群成员的群名片。
            /// </summary>
            /// <returns></returns>
            public string InGroupName { get; set; }

            /// <summary>
            /// 此群成员的头衔。
            /// </summary>
            /// <returns></returns>
            public string Title { get; set; }

            /// <summary>
            /// 此群成员所在群号。
            /// </summary>
            /// <returns></returns>
            public long GroupId { get; set; }

            /// <summary>
            /// 指示此群成员是否有不良记录的值。
            /// </summary>
            /// <returns></returns>
            public bool HasBadRecord { get; set; }

            /// <summary>
            /// 头衔过期时间。
            /// </summary>
            /// <returns></returns>
            public int TitleExpirationTime { get; set; }

            /// <summary>
            /// 此群成员的入群时间。
            /// </summary>
            /// <returns></returns>
            public DateTime JoinTime { get; set; }

            /// <summary>
            /// 此群成员最后发言日期。
            /// </summary>
            /// <returns></returns>
            public DateTime LastSpeakingTime { get; set; }

            /// <summary>
            /// 此群成员的群内等级。
            /// </summary>
            /// <returns></returns>
            public string Level { get; set; }

            /// <summary>
            /// 此群成员的昵称。
            /// </summary>
            /// <returns></returns>
            public string NickName { get; set; }

            /// <summary>
            /// 此群成员的QQ号码。
            /// </summary>
            /// <returns></returns>
            public long Number { get; set; }
        }

        // -------------
    }

    internal static class DataConvertExtensions
    {
        public static long ToLong(this byte[] bytes) { Array.Reverse(bytes); return BitConverter.ToInt64(bytes, 0); }
        public static int ToInt(this byte[] bytes) { Array.Reverse(bytes); return BitConverter.ToInt32(bytes, 0); }
        public static short ToShort(this byte[] bytes) { Array.Reverse(bytes); return BitConverter.ToInt16(bytes, 0); }

        /// <summary>
        /// 从此实例检索子数组
        /// </summary>
        /// <param name="source">要检索的数组</param>
        /// <param name="startIndex">起始索引号</param>
        /// <param name="length">检索最大长度</param>
        /// <returns>与此实例中在 startIndex 处开头、长度为 length 的子数组等效的一个数组</returns>
        public static byte[] SubArray(this byte[] source, int startIndex, int length)
            => (startIndex < 0 || startIndex > source.Length || length < 0) ?
                throw new ArgumentOutOfRangeException(nameof(startIndex)) :
                startIndex + length <= source.Length ?
                    source.Skip(startIndex).Take(length).ToArray() :
                    source.Skip(startIndex).ToArray();
    }

}
