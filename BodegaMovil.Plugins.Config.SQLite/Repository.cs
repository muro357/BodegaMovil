using Microsoft.Maui.Animations;
using Microsoft.Maui.ApplicationModel.Communication;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodegaMovil.Plugins.Config.SQLite
{
    public class Config
    {
        private SQLiteAsyncConnection database;
        public Config()
        {
            this.database = new SQLiteAsyncConnection(Constants.DatabasePath);
            this.database.CreateTableAsync<FileConfig>();
        }

        public async Task<string> GetUrl()
        {
            var a = await this.database.Table<FileConfig>().FirstOrDefaultAsync();

            return a == null ? "" : a.Url;
        }

        public async Task UpdateUrl(string url)
        {
            var c = new FileConfig { Url = url };
            await this.database.UpdateAsync(c);
        }


    }
}
