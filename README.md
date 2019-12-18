# csv view inset tool and connect to sqlite

> Need reference
1. EF6, system.data.sqite
2. Sqlite.CodeFirst
3. System.Data.SQLite.EF6.Migrations

> Protocol
1. Web.config change:
     * entityFramework -> providers
     * System.data -> DbProviderFactories
     * connectionString
     * 中文參考 <https://www.cnblogs.com/buyixiaohan/p/7233507.html>
2. Enable-migrations -force 之後 修改 Migrations -> Configuration.cs 
    *  SetSqlGenerator("System.Data.SQLite", new SQLiteMigrationSqlGenerator());// the Golden Key
    *  reference <https://stackoverflow.com/questions/56226978/no-migrationsqlgenerator-found-for-provider-system-data-sqlite>
---
PS : migration 操作可能不如預期 <https://github.com/bubibubi/db2ef6migrations>
利用migration 刪除跟增加好像會出錯

PS:  EF 6.3無法使用 因為版本太新了 .net 大概全心不投入開源，但.net core倒是全心開源，所以所配的資料庫最好也參照版本區分會比較容易維護跟事後修正，目前確定 EF 6.3 無法使用sqlite 操作 migration 