using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.NCFrameWork.Utility
{
	public static class DataTableExtensions
	{
		private static NCLogger Log = null;
		public static List<T> ToList<T>(this DataTable dataTable) where T : new()
		{
			List<T> objectList = new List<T>();
			var properties = typeof(T).GetProperties()
									  .Where(p => p.CanWrite)
									  .ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);

			foreach (DataRow row in dataTable.Rows)
			{
				T obj = new T();

				foreach (DataColumn column in dataTable.Columns)
				{
					if (properties.TryGetValue(column.ColumnName, out var property) &&
						row[column] != DBNull.Value)
					{
						try
						{
							var value = row[column];
							var targetType = property.PropertyType;

							if (targetType.IsGenericType &&
								targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
							{
								targetType = Nullable.GetUnderlyingType(targetType);
							}

							var convertedValue = Convert.ChangeType(value, targetType);
							property.SetValue(obj, convertedValue);
						}
						catch (Exception ex)
						{
							if (Log == null)
							{
								NCLogger.SetLogType(LogTypeManager.AllUser);
								Log = NCLogger.GetInstance();
								Log.WriteExceptionLog(ex);
							}
						}
					}
				}

				objectList.Add(obj);
			}
			return objectList;
		}
	}
}
