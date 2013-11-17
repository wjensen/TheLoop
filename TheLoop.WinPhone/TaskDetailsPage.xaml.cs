using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using TheLoop.BL.Managers;
using TheLoop.BL;

namespace TheLoop.WinPhone {
    public partial class TaskDetailsPage : PhoneApplicationPage {
        public TaskDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.NavigationMode == System.Windows.Navigation.NavigationMode.New) {
                var vm = new TaskViewModel();
                var task = default(Task);

                if (NavigationContext.QueryString.ContainsKey("id")) {
                    var id = int.Parse(NavigationContext.QueryString["id"]);
                    task = (App.Current as TheLoop.WinPhone.App).TaskMgr.GetTask(id);
                } 

                if (task != null)
                {
                    vm.Update(task);
                }
               
                DataContext = vm;
            }
        }

        private void HandleSave(object sender, EventArgs e)
        {
            var taskvm = (TaskViewModel)DataContext;
            var task = taskvm.GetTask();
            (App.Current as TheLoop.WinPhone.App).TaskMgr.SaveTask(task);

            NavigationService.GoBack();
        }

        private void HandleDelete(object sender, EventArgs e)
        {
            var taskvm = (TaskViewModel)DataContext;
            if (taskvm.ID >= 0)
                (App.Current as TheLoop.WinPhone.App).TaskMgr.DeleteTask(taskvm.ID);

            NavigationService.GoBack();
        }
    }
}