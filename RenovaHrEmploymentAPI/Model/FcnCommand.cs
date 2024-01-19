using Microsoft.EntityFrameworkCore;
using RenovaHrEmploymentAPI.Contracts;
using RenovaHrEmploymentAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;
using System.Collections;

namespace RenovaHrEmploymentAPI.Services
{
    public class FcnCommand
    {
        IList<FcnParameter> parameters = new List<FcnParameter>();
        string FcnName { get; set; }
        public bool IsNotProcedure = false;

        public FcnCommand(string Name)
        {
            FcnName = Name;
        }

        public static T GetValue<T>(dynamic obj, string name) {


            try
            {
                if (obj[name] == null)
                {
                   var res =  default(T);
                    return res;
                }

                var oType = obj[name].GetType();
                if (oType.GetProperty("IsNull") != null)
                {
                    return obj[name].IsNull ? default(T) : obj[name].Value;
                }
                else
                {
                    return (T)obj[name];
                }
            }catch(Exception e)
            {
                return default(T);
            }
            
        
        }

        public void AddInput(string Name, object Value, OracleDbType Type = OracleDbType.Varchar2)
        {
            AddInput(Name, Value, 0, Type);
        }
        public void AddInput(string Name, object Value, Decimal Size = 0, OracleDbType Type = OracleDbType.Varchar2)
        {
            FcnParameter parameter = new FcnParameter(Name, Value, Type);
            parameter.SetSize(Size);
            parameter.SetDirection(ParameterDirection.Input);
            parameters.Add(parameter);

        }

        public void AddOutput(string Name, object Value, OracleDbType Type)
        {
            AddOutput(Name, Value, 0, Type);
        }
        public void AddOutput(string Name, object Value, Decimal Size = 0, OracleDbType Type = OracleDbType.Varchar2)
        {
            FcnParameter parameter = new FcnParameter(Name, Value, Type);
            if(Size > 0)
            {
                parameter.SetSize(Size);
            }
            parameter.SetDirection(System.Data.ParameterDirection.Output);
            parameters.Add(parameter);
        }

        public void AddReturn(string Name, object Value, OracleDbType Type)
        {
            AddReturn(Name, Value, 0, Type);
        }
        public void AddReturn(string Name, object Value, Decimal Size = 0, OracleDbType Type = OracleDbType.Varchar2)
        {
            FcnParameter parameter = new FcnParameter(Name, Value, Type);
            parameter.SetSize(Size);
            parameter.SetDirection(System.Data.ParameterDirection.ReturnValue);
            parameters.Add(parameter);
        }

        OracleCommand execute(OracleConnection connection)
        {
            
            OracleCommand command = new OracleCommand(FcnName, connection);
             connection.Open();

            command.CommandType = !IsNotProcedure?CommandType.StoredProcedure:CommandType.Text;
            foreach (var parameter in parameters)
            {
                parameter.AddToCommad(command);
            }
            var text = command.CommandText;
            var logger = new LoggerService();
            logger.LogDebug(text);
            command.ExecuteNonQuery();
            OrCmd = command;

            return command;
        }

        public IList<object> ExecuteAsCursor(decimal? limit = null)
        {
            var db = new ModelContext();
            var dbConnection = db.Database.GetDbConnection();

            OracleConnection connection = new OracleConnection(dbConnection.ConnectionString);
            IList<object> results = new List<object>();
            OracleCommand command = execute(connection);
            OrCmd = command;

            FcnParameter parameter = parameters.Where(x =>
                x.GetDirection().Equals(ParameterDirection.ReturnValue)).First();

            if (parameter != null)
            {
                if (parameter.GetParamType().Equals(OracleDbType.RefCursor))
                {


                    OracleRefCursor result = (OracleRefCursor)command.Parameters[parameter.Name].Value;
                    var rowsCount = 0;
                    if (result.RowSize > 0)
                    {
                        rowsCount = (int)(result.FetchSize / result.RowSize);
                    }

                    if (!result.IsNull)
                    {
                        OracleDataReader reader = result.GetDataReader();

                        DataTable schemaTable = reader.GetSchemaTable();

                        DataTable table = new DataTable();
                        foreach (DataRow row in schemaTable.Rows)
                        {
                            string colName = row.Field<string>("ColumnName");
                            Type t = row.Field<Type>("DataType");

                            if (!table.Columns.Contains(colName))
                            {
                                table.Columns.Add(colName, t);
                            }


                        }
                        var useLimit = limit != null;
                        if (!reader.HasRows){
                            return results;
                        }
                        while ((!useLimit || (useLimit && limit > 0)) && reader.Read())
                        {

                            dynamic obj = new Dictionary<string, object>();
                            foreach (DataColumn col in table.Columns)
                            {
                                try
                                {
                                    obj[ConvertNameFormat(col.ColumnName)] = reader[col.ColumnName];
                                }catch(Exception e)
                                {
                                    obj[ConvertNameFormat(col.ColumnName)] = null;
                                }
                                
                            }
                            limit--;
                            results.Add(obj);

                        }

                        reader.Close();
                    }
                    
                }
            }
            connection.Close();
            command.Dispose();
            return results;

        }

        //public string ConvertNameFormat(string name)
        //{
        //    var words = name.Split("_");
        //    var newName = words.ElementAt(0).ToString().ToLower();

        //    if(words.Length > 1)
        //    {
        //        for(int i=1; i<words.Length;i++)
        //        {
        //            var word = words[i].ToLower().ToCharArray();
        //            word[0] = Char.ToUpper(word[0]);
        //            newName += new string(word);
                    
                    
        //        }
        //    }

        //    return newName;
            
        //}
        public static string ConvertNameFormat(string name)
        {
            var words = name.Split("_");
            var newName = words.ElementAt(0).ToString().ToLower();

            if (words.Length > 1)
            {
                for (int i = 1; i < words.Length; i++)
                {
                    var word = words[i].ToLower().ToCharArray();
                    word[0] = Char.ToUpper(word[0]);
                    newName += new string(word);


                }
            }

            return newName;

        }
        public object ExecuteAsObject2()
        {
            var db = new ModelContext();
            var dbConnection = db.Database.GetDbConnection();

            OracleConnection connection = new OracleConnection(dbConnection.ConnectionString);
            OracleCommand command = execute(connection);

            dynamic obj = new Dictionary<string, object>();
            foreach (FcnParameter parameter in parameters)
            {

                var value = command.Parameters[parameter.Name].Value;

                if ((value + "") == "null")
                {
                    obj[ConvertNameFormat(parameter.Name)] = null;

                }
                else
                {
                    if ((parameter.GetParamType().Equals(OracleDbType.Char) ||
                    parameter.GetParamType().Equals(OracleDbType.Varchar2)))
                    {
                        value = Convert.ToString(value);
                    }



                    if ((parameter.GetParamType().Equals(OracleDbType.Decimal)))
                    {
                        if (command.Parameters[parameter.Name].Value == null ||
                            command.Parameters[parameter.Name].Value == "null")
                        {

                            value = 0;
                        }
                        try
                        {
                            value = value.GetType().GetProperty("Value").GetValue(value);
                        }
                        catch (Exception e)
                        {
                            var Logger = new LoggerService();
                            Logger.LogDebug(e.Message);
                        }


                    }

                    if (parameter.GetParamType().Equals(OracleDbType.Blob))
                    {
                        OracleBlob data = (OracleBlob)value;
                        if (!data.IsNull)
                        {
                            value = Convert.ToBase64String(data.Value);
                        }
                        else
                        {
                            value = null;
                        }

                    }

                    obj[ConvertNameFormat(parameter.Name)] = value;
                }

                


            }
            command.Dispose();
            connection.Close();

            return obj;

        }

        public object ExecuteAsObject3()
        {
            var db = new ModelContext();
            var dbConnection = db.Database.GetDbConnection();

            OracleConnection connection = new OracleConnection(dbConnection.ConnectionString);
            OracleCommand command = execute(connection);


            dynamic obj = new Dictionary<string, object>();
            var enumerator = command.Parameters.GetEnumerator();
            for(int i=0; i<command.Parameters.Count; i++)
            {
                obj[ConvertNameFormat(command.Parameters[i].ParameterName)] = command.Parameters[i].Value;
            }

            return obj;


            //dynamic obj = new Dictionary<string, object>();
            //foreach (FcnParameter parameter in parameters)
            //{

            //    var value = parameter.Value;
            //    if (!parameter.IsInput)
            //    {
            //        value = command.Parameters[parameter.Name];
            //    }


            //    if ((value + "") == "null")
            //    {
            //        obj[ConvertNameFormat(parameter.Name)] = null;

            //    }
            //    else
            //    {
            //        if ((parameter.GetParamType().Equals(OracleDbType.Char) ||
            //        parameter.GetParamType().Equals(OracleDbType.Varchar2)))
            //        {
            //            value = Convert.ToString(value);
            //        }



            //        if ((parameter.GetParamType().Equals(OracleDbType.Decimal)))
            //        {
            //            if (parameter.Value == null ||
            //                parameter.Value == "null")
            //            {

            //                value = 0;
            //            }
            //            try
            //            {
            //                value = value.GetType().GetProperty("Value").GetValue(value);
            //            }
            //            catch (Exception e)
            //            {
            //                LoggerService.GetLoggerService().LogDebug(e.Message);
            //            }


            //        }

            //        if (parameter.GetParamType().Equals(OracleDbType.Blob))
            //        {
            //            OracleBlob data = (OracleBlob)value;
            //            if (!data.IsNull)
            //            {
            //                value = Convert.ToBase64String(data.Value);
            //            }
            //            else
            //            {
            //                value = null;
            //            }

            //        }

            //        obj[ConvertNameFormat(parameter.Name)] = value;
            //    }




            //}
            //command.Dispose();
            //connection.Close();

            //return obj;

        }
        public OracleCommand OrCmd { get; set; }

        public object ExecuteAsObject()
        {
            var db = new ModelContext();
            var dbConnection = db.Database.GetDbConnection();

            OracleConnection connection = new OracleConnection(dbConnection.ConnectionString);
            OracleCommand command = execute(connection);
            OrCmd = command;

            dynamic obj = new Dictionary<string, object>();
            foreach (FcnParameter parameter in parameters)
            {

                var value = command.Parameters[parameter.Name].Value;

                if ((value + "") == "null")
                {
                    obj[ConvertNameFormat(parameter.Name)] = null;
                    
                }
                else
                {
                

                    if ((parameter.GetParamType().Equals(OracleDbType.Char) ||
                        parameter.GetParamType().Equals(OracleDbType.Varchar2)))
                    {
                        value = Convert.ToString(value);
                    }



                    if ((parameter.GetParamType().Equals(OracleDbType.Decimal)))
                    {
                        if (command.Parameters[parameter.Name].Value == null ||
                            command.Parameters[parameter.Name].Value == "null")
                        {

                            value = 0;
                        }


                    }

                    if (parameter.GetParamType().Equals(OracleDbType.Blob))
                    {
                        OracleBlob data = (OracleBlob)value;
                        if (!data.IsNull)
                        {
                            value = Convert.ToBase64String(data.Value);
                        }
                        else
                        {
                            value = null;
                        }

                    }

                    obj[ConvertNameFormat(parameter.Name)] = value;

            }
                
                
            }
            command.Dispose();
            connection.Close();

            return obj;

        }


        public void ExecuteNoReturn()
        {
            var db = new ModelContext();
            var dbConnection = db.Database.GetDbConnection();

            OracleConnection connection = new OracleConnection(dbConnection.ConnectionString);
            execute(connection);
            connection.Close();

        }

    }

    public class FcnParameter
    {
        OracleDbType Type { get; set; }
        public object Value { get; set; }
        public string Name { get; set; }
        decimal Size { get; set; }
        public bool IsInput { get; set; }
        System.Data.ParameterDirection Direction { get; set; }


        
        public FcnParameter(string Name, object Value, OracleDbType Type)
        {
            SetType(Type);
            SetDirection(ParameterDirection.Input);
            this.Name = Name;
            this.Value = Value;
            


        }
        public System.Data.ParameterDirection GetDirection()
        {
            return Direction;
        }

        public object GetParamType()
        {
            return Type;
        }
        public void SetSize(decimal size = 2000)
        {
            Size = size;
        }
        public void SetDirection(System.Data.ParameterDirection ParamDirection)
        {
            
            this.Direction = ParamDirection;
           

        }


        public void SetType(OracleDbType ParamType)
        {
           this.Type = ParamType;
            

        }

        public void AddToCommad(OracleCommand command)
        {
            command.Parameters.Add(Name, Type);
            if (!Direction.Equals(System.Data.ParameterDirection.ReturnValue))
            {
                command.Parameters[Name].Value = Value;
            }
            
            if(Size > 0)
            {
                command.Parameters[Name].Size = (int)Size;
            }
            
            command.Parameters[Name].Direction = Direction;

        }
    }
}
