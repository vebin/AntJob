using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;
using NewLife;
using NewLife.Data;
using NewLife.Log;
using NewLife.Model;
using NewLife.Reflection;
using NewLife.Threading;
using NewLife.Web;
using XCode;
using XCode.Cache;
using XCode.Configuration;
using XCode.DataAccessLayer;
using XCode.Membership;

namespace AntJob.Data.Entity
{
    /// <summary>作业日志</summary>
    public partial class JobLog : EntityBase<JobLog>
    {
        #region 对象操作
        static JobLog()
        {
            // 累加字段
            //var df = Meta.Factory.AdditionalFields;
            //df.Add(__.AppID);

            // 过滤器 UserModule、TimeModule、IPModule
            Meta.Modules.Add<TimeModule>();
        }

        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew">是否插入</param>
        public override void Valid(Boolean isNew)
        {
            // 如果没有脏数据，则不需要进行任何处理
            if (!HasDirty) return;

            // 在新插入数据或者修改了指定字段时进行修正
            //if (isNew && !Dirtys[nameof(CreateTime)]) nameof(CreateTime) = DateTime.Now;
            //if (!Dirtys[nameof(UpdateTime)]) nameof(UpdateTime) = DateTime.Now;
        }

        ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //protected override void InitData()
        //{
        //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
        //    if (Meta.Session.Count > 0) return;

        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化JobLog[作业日志]数据……");

        //    var entity = new JobLog();
        //    entity.ID = 0;
        //    entity.AppID = 0;
        //    entity.JobID = 0;
        //    entity.LinkID = 0;
        //    entity.Client = "abc";
        //    entity.Start = DateTime.Now;
        //    entity.End = DateTime.Now;
        //    entity.Row = 0;
        //    entity.Step = 0;
        //    entity.BatchSize = 0;
        //    entity.Offset = 0;
        //    entity.Total = 0;
        //    entity.Success = 0;
        //    entity.Error = 0;
        //    entity.Times = 0;
        //    entity.Speed = 0;
        //    entity.FetchSpeed = 0;
        //    entity.Cost = 0;
        //    entity.FullCost = 0;
        //    entity.Status = 0;
        //    entity.MsgCount = 0;
        //    entity.Server = "abc";
        //    entity.ProcessID = 0;
        //    entity.ThreadID = 0;
        //    entity.Key = "abc";
        //    entity.Data = "abc";
        //    entity.Message = "abc";
        //    entity.CreateTime = DateTime.Now;
        //    entity.UpdateTime = DateTime.Now;
        //    entity.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化JobLog[作业日志]数据！");
        //}

        ///// <summary>已重载。基类先调用Valid(true)验证数据，然后在事务保护内调用OnInsert</summary>
        ///// <returns></returns>
        //public override Int32 Insert()
        //{
        //    return base.Insert();
        //}

        ///// <summary>已重载。在事务保护范围内处理业务，位于Valid之后</summary>
        ///// <returns></returns>
        //protected override Int32 OnDelete()
        //{
        //    return base.OnDelete();
        //}
        #endregion

        #region 扩展属性
        /// <summary>应用</summary>
        [XmlIgnore]
        //[ScriptIgnore]
        public App App { get { return Extends.Get(nameof(App), k => App.FindByID(AppID)); } }

        /// <summary>应用</summary>
        [XmlIgnore]
        //[ScriptIgnore]
        [DisplayName("应用")]
        [Map(__.AppID, typeof(App), "ID")]
        public String AppName { get { return App?.Name; } }
        /// <summary>作业</summary>
        [XmlIgnore]
        //[ScriptIgnore]
        public Job Job { get { return Extends.Get(nameof(Job), k => Job.FindByID(JobID)); } }

        /// <summary>作业</summary>
        [XmlIgnore]
        //[ScriptIgnore]
        [DisplayName("作业")]
        [Map(__.JobID, typeof(Job), "ID")]
        public String JobName { get { return Job?.Name; } }
        #endregion

        #region 扩展查询
        /// <summary>根据编号查找</summary>
        /// <param name="id">编号</param>
        /// <returns>实体对象</returns>
        public static JobLog FindByID(Int32 id)
        {
            if (id <= 0) return null;

            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.ID == id);

            // 单对象缓存
            return Meta.SingleCache[id];

            //return Find(_.ID == id);
        }

        /// <summary>根据应用、客户端、状态查找</summary>
        /// <param name="appid">应用</param>
        /// <param name="client">客户端</param>
        /// <param name="status">状态</param>
        /// <returns>实体列表</returns>
        public static IList<JobLog> FindAllByAppIDAndClientAndStatus(Int32 appid, String client, Int32 status)
        {
            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.AppID == appid && e.Client == client && e.Status == status);

            return FindAll(_.AppID == appid & _.Client == client & _.Status == status);
        }
        #endregion

        #region 高级查询
        #endregion

        #region 业务操作
        #endregion
    }
}