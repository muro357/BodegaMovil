using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BodegaMovil.Plugins.Config.SQLite
{
    public class Constants
    {
        public const string DatabaseFileName = "ConfigSQLite.db3";

        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFileName);
    }
}
