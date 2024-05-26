using System.Windows;
using System.Windows.Controls;
using WpfClientCalander.ServiceCalander;

namespace WpfClientCalander
{
    /// <summary>
    /// Interaction logic for ChooseGroupsUC.xaml
    /// </summary>
    public partial class ChooseGroupsUC : UserControl
    {
        Users user;
        UserWindow parent;
        public ChooseGroupsUC(Users user,UserWindow window)
        {
            InitializeComponent();
            this.user = user;   
            this.parent = window;
            CalanderServiceClient serviceClient = new CalanderServiceClient();
            GroupsList list = serviceClient.GetAllGroups();
            foreach (Groups group in list)
            {
                GroupsInfoViewUC uC = new GroupsInfoViewUC(group, user, this);
                uC.Margin = new Thickness(10);
                panel.Children.Add(uC);
            }
        }

        internal void NewGroup(GroupsInfoViewUC uc)
        {
            parent.LoadMyGroups();
            uc.ChangeColour();
        }

    }
}
