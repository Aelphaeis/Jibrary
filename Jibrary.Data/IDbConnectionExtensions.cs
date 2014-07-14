﻿using System;
using System.Data;

namespace Jibrary.Data
{
    public static class IDbConnectionExtensions
    {

        public static IDbCommand CreateCommand(this IDbConnection Conn, string CommandText, params object[] Parameters)
        {
            var Command = Conn.CreateCommand();
            Command.CommandText = CommandText;
            foreach (var p in Parameters ?? new object[0])
                Command.Parameters.Add(Command.CreateParameter(p));
            return Command;
        }

        public static IDbDataParameter CreateParameter(this IDbCommand Command, object Value)
        {
            var Param = Command.CreateParameter();
            Param.Value = Value;
            return Param;
        }

        public static JibraryQueryResult Query(this IDbConnection conn, String CommandText, params object[] Arguments)
        {
            using (var Comm = conn.CreateCommand(CommandText, Arguments))
            using (var reader = Comm.ExecuteReader(CommandBehavior.KeyInfo))
                return new JibraryQueryResult(reader);
        }
        public static int NonQuery(this IDbConnection conn, String CommandText, params object[] Arguments)
        {
            using (var Comm = conn.CreateCommand(CommandText, Arguments))
                return Comm.ExecuteNonQuery();
        }

        public static IDbConnection OpenConnection(this IDbConnection connection)
        {
            connection.Open();
            return connection;
        }
    }
}
