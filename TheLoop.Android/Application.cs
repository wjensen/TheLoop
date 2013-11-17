using System;
using System.IO;
using Android.App;
using TheLoop.BL.Managers;
using TheLoop.DL.SQLite;

namespace TheLoop.Droid {
    [Application]
    public class App : Application {
        public static App Current { get; private set; }

        public TaskManager TaskMgr { get; set; }
        Connection conn;

        public App(IntPtr handle, global::Android.Runtime.JniHandleOwnership transfer)
            : base(handle, transfer) {
                Current = this;
        }

        public override void OnCreate()
        {
            base.OnCreate();

            var sqliteFilename = "TaskDB.db3";
            string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(libraryPath, sqliteFilename);
            conn = new Connection(path);

            TaskMgr = new TaskManager(conn);
        }
    }
}
