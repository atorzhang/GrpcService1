using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace FJRH.CertificationCenter.BLL
{
	public static class GUIDHelper
	{
		/// <summary>
		/// 创建一个GUID,此时Category部份的值为是m_ran.Next(900, 999)。
		/// </summary>
		/// <returns></returns>
		public static Guid CreateGUID()
		{
			return CreateGUID(new Random(Guid.NewGuid().GetHashCode()).Next(900, 999));
		}

		/// <summary>
		/// 创建一个GUID
		/// </summary>
		/// <param name="category">类型(0到999的一个int数)随机时=900~999；系统的=800~899；用户自定义=0~799</param>
		/// <returns></returns>
		public static Guid CreateGUID(int category)
		{
			DateTime now = DateTime.Now;
			int num = new Random(Guid.NewGuid().GetHashCode()).Next(999);
			return new Guid(now.ToString("yyMMddHHmmssfff") + category.ToString("000") + (now.DayOfYear + now.Millisecond + num).ToString("00000") + new Random(Guid.NewGuid().GetHashCode()).Next(99999).ToString("000000") + num.ToString("000"));
		}

		/// <summary>
		/// 从 Guid 字符口串返回 Guid 格式。
		/// </summary>
		/// <param name="guidStr"></param>
		/// <returns></returns>
		public static Guid GetGuid(this string guidStr)
		{
			try
			{
				return new Guid(guidStr);
			}
			catch
			{
				return Guid.Empty;
			}
		}

		/// <summary>
		/// 返回没有分隔符的GUID字符
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static string GetGUIDString(this Guid id)
		{
			return id.ToString("N");
		}

		/// <summary>
		/// 获取ID里的时间戳
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static DateTime GetDateTime(this Guid id)
		{
			try
			{
				return DateTime.ParseExact("20" + GetGUIDString(id).Substring(0, 15), "yyyyMMddHHmmssfff", CultureInfo.CurrentCulture);
			}
			catch (Exception)
			{
			}
			return DateTime.MinValue;
		}

		/// <summary>
		/// 取得ID里的年
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static int GetYear(this Guid id)
		{
			return Convert.ToInt32("20" + GetGUIDString(id).Substring(0, 2));
		}

		/// <summary>
		/// 取得月
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static int GetMonth(this Guid id)
		{
			return Convert.ToInt32(GetGUIDString(id).Substring(2, 2));
		}

		/// <summary>
		/// 取得日
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static int GetDay(this Guid id)
		{
			return Convert.ToInt32(GetGUIDString(id).Substring(4, 2));
		}

		/// <summary>
		/// 取得小时部份
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static int GetHour(this Guid id)
		{
			return Convert.ToInt32(GetGUIDString(id).Substring(6, 2));
		}

		/// <summary>
		/// 取得分钟部份
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static int GetMinute(this Guid id)
		{
			return Convert.ToInt32(GetGUIDString(id).Substring(8, 2));
		}
	}
}
