using System.IO;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WpfClientCalander.ServiceCalander;
using System.Collections.Generic;
using System.Linq;
using System.Data.OleDb;

namespace WpfClientCalander
{
    /// <summary>
    /// Interaction logic for GroupsTblUC.xaml
    /// </summary>
    public partial class GroupsTblUC : UserControl
    {
        CalanderServiceClient serviceClient;
        GroupsList groupLst;
        UserWindow userWindow;
        
        List<string> names; 
        public GroupsTblUC(UserWindow userWindow)
        {
            InitializeComponent();
            serviceClient = new CalanderServiceClient();
            groupLst = serviceClient.GetAllGroups();
            foreach (Groups group in groupLst)
            {
                group.Users = serviceClient.GetUsersByGroup(group);
            }
            names = groupLst.Select(g => g.Id + "  " + g.GroupName).ToList();
            groupsListView.ItemsSource = groupLst;
            this.userWindow = userWindow;
        }

        private void groupName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Retrieve the user object associated with the TextBox
                Groups group = textBox.DataContext as Groups;

                if (group != null && group.GroupName != string.Empty)
                {
                    if (serviceClient.IsGroupNameFree(group.GroupName))
                    {
                        if (serviceClient.UpdateGroup(group) == 1)// Call the service client to update the user
                        {
                            string oldName = names.Find(n => n.StartsWith(group.Id + "  "));
                            oldName = oldName.Substring(oldName.IndexOf("  ") + 2);
                            string encodedGroupName = oldName;
                            string uriStr = Environment.CurrentDirectory; //המיקום שבו רץ הפרויקט
                            uriStr = uriStr.Substring(0, uriStr.IndexOf("\\bin"));
                            uriStr = uriStr + @"\Images\imgGroups\";
                            DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(uriStr);
                            FileInfo[] filesInDir = hdDirectoryInWhichToSearch.GetFiles(encodedGroupName + ".*");
                            if (filesInDir.Length > 0)
                            {
                                foreach (FileInfo file in filesInDir)
                                {
                                    if (file.Exists)
                                    {
                                        bool deleted = false;
                                        int retryCount = 0;
                                        int maxRetries = 3;
                                        int delay = 1000; // 1 second
                                        file.CopyTo(file.FullName.Replace(oldName, group.GroupName));
                                        while (!deleted && retryCount < maxRetries)
                                        {
                                            try
                                            {
                                                using (FileStream s = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.None))
                                                {
                                                    s.Close();
                                                    s.Dispose();
                                                }

                                                File.Delete(file.FullName);
                                                deleted = true;
                                            }
                                            catch (IOException)
                                            {
                                                retryCount++;
                                                System.Threading.Thread.Sleep(delay); // Wait before retrying
                                            }
                                        }

                                        if (!deleted)
                                        {
                                            MessageBox.Show($"Could not delete file {file.FullName}. It may be in use by another process.", "ERROR", MessageBoxButton.OK);
                                            return;
                                        }
                                    }
                                }
                            }
                            userWindow.LoadMyGroups();
                            MessageBox.Show("Group name changed successfully.", "SUCCESS", MessageBoxButton.OK);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Group name taken. \n Try again.", "ERROR", MessageBoxButton.OK);
                    }
                }
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e) //delete group
        {
            foreach (Groups group in groupLst)
            {
                if (group.GroupName == gnameToDel.Text)
                {
                    serviceClient.DeleteGroup(group);
                    groupLst = serviceClient.GetAllGroups();
                    gnameToDel.Text = "";
                    groupsListView.ItemsSource = groupLst;

                    string encodedGroupName = group.GroupName;
                    string uriStr = Environment.CurrentDirectory; //המיקום שבו רץ הפרויקט
                    uriStr = uriStr.Substring(0, uriStr.IndexOf("\\bin"));
                    uriStr = uriStr + @"\Images\imgGroups\";
                    DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(uriStr);
                    FileInfo[] filesInDir = hdDirectoryInWhichToSearch.GetFiles(encodedGroupName + ".*");

                    if (filesInDir.Length > 0)
                    {
                        foreach (FileInfo file in filesInDir)
                        {
                            if (file.Exists)
                            {
                                bool deleted = false;
                                int retryCount = 0;
                                int maxRetries = 3;
                                int delay = 1000; // 1 second

                                while (!deleted && retryCount < maxRetries)
                                {
                                    try
                                    {
                                        using (FileStream s = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.None)) //class used to read and write files 
                                        {
                                            s.Close();
                                            s.Dispose();
                                        }

                                        File.Delete(file.FullName);
                                        deleted = true;
                                    }
                                    catch (IOException)
                                    {
                                        retryCount++;
                                        System.Threading.Thread.Sleep(delay); // Wait before retrying
                                    }
                                }

                                if (!deleted)
                                {
                                    MessageBox.Show($"Could not delete file {file.FullName}. It may be in use by another process.", "ERROR", MessageBoxButton.OK);
                                    return;
                                }
                            }
                        }
                    }
                    userWindow.LoadMyGroups();
                    MessageBox.Show("Group deleted successfully.", "SUCCESS", MessageBoxButton.OK);
                    return;
                }
            }
            MessageBox.Show("Group name doesn't exist.", "ERROR", MessageBoxButton.OK);
            return;
        }

        private void btnChange_Click(object sender, RoutedEventArgs e) //change group admin
        {
            Groups group = serviceClient.GetGroupByGroupName(GroupChange.Text);
            UsersList users = serviceClient.GetAllUsers();
            Users admin = group.GroupAdmin;
            foreach (Users user in users)
            {
                if (user.UserName == adminChange.Text)
                {
                    admin = user;
                }
            }
            if (admin == null)
            {
                MessageBox.Show("User doesn't exist.", "ERROR", MessageBoxButton.OK);
                return;
            }
            admin.IsGroupAdmin = true;
            serviceClient.UpdateUser(admin);
            group.GroupAdmin = admin;
            serviceClient.InsertUserToGroup(admin, group);
            if (serviceClient.UpdateGroup(group) != 0)
            {
                userWindow.LoadMyGroups();
                groupLst = serviceClient.GetAllGroups();
                foreach (Groups g in groupLst)
                {
                    g.Users = serviceClient.GetUsersByGroup(g);
                }
                groupsListView.ItemsSource = groupLst;
                adminChange.Text = "";
                GroupChange.Text = "";
                MessageBox.Show("Group admin changed successfully.", "SUCCESS", MessageBoxButton.OK);
                return;
            }
            MessageBox.Show("Group admin couldn't update.", "ERROR", MessageBoxButton.OK);
            return;
        }
    }
}
