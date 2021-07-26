using BackEnd.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
namespace BackEnd.BusinessComponents
{
    public static class Extensions
    {
       

        public static List<T> ToList<T>(this DataTable table) where T : new()
        {
            List<T> result;
            try
            {
                result = new List<T>();
                IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
                foreach (var row in table.Rows)
                {
                    var item = CreateItemFromRow<T>((DataRow)row, properties);
                    result.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
        {
            T item = new T();
            foreach (var property in properties)
            {
                if (HasProperty(item, property.Name))
                {
                    string name = string.Empty;
                    string szTipo = string.Empty;
                    try
                    {
                        if (row[property.Name] != DBNull.Value)
                        {
                            name = row[property.Name].ToString();
                        }
                        Type oTipe = row[property.Name].GetType();
                        szTipo = oTipe.ToString();
                        if (property.PropertyType == typeof(System.DayOfWeek))
                        {
                            DayOfWeek day = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), row[property.Name].ToString());
                            property.SetValue(item, day, null);
                        }
                        else
                        {
                            if (row[property.Name] == DBNull.Value)
                            {
                                property.SetValue(item, null, null);
                            }
                            else
                            {
                                switch (szTipo)
                                {
                                    case "System.DateTime":
                                        {
                                            try
                                            {
                                                property.SetValue(item, Convert.ToDateTime(row[property.Name]), null);
                                            }
                                            catch
                                            {
                                                property.SetValue(item, Convert.ToString(row[property.Name]), null);
                                            }
                                            break;
                                        }
                                    case "System.Byte":
                                        {
                                            property.SetValue(item, Convert.ToByte(row[property.Name]), null);
                                            break;
                                        }
                                    case "System.Guid":
                                    case "System.String":
                                        {
                                            property.SetValue(item, Convert.ToString(row[property.Name]), null);
                                            break;
                                        }
                                    case "System.Int16":
                                    case "System.Int32":
                                    case "System.Int64":
                                        {
                                            property.SetValue(item, Convert.ToInt32(row[property.Name]), null);
                                            break;
                                        }
                                    case "System.Decimal":
                                        {
                                            property.SetValue(item, Convert.ToDecimal(row[property.Name]), null);
                                            break;
                                        }
                                    case "System.Boolean":
                                        {
                                            property.SetValue(item, Convert.ToBoolean(row[property.Name]), null);
                                            break;
                                        }
                                    case "System.Double":
                                        {
                                            property.SetValue(item, Convert.ToDouble(row[property.Name]), null);
                                            break;
                                        }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            return item;
        }

        public static bool HasProperty(this object objectToCheck, string PropertyName)
        {
            var type = objectToCheck.GetType();
            return type.GetProperty(PropertyName) != null;
        }
    }
}
