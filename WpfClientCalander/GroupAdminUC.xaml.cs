using System.Windows;
using System.Windows.Controls;
using WpfClientCalander.ServiceCalander;

namespace WpfClientCalander
{
    /// <summary>
    /// Interaction logic for GroupAdminUC.xaml
    /// </summary>
    public partial class GroupAdminUC : UserControl
    {
        Users user;
        private Grid grid;

        public GroupAdminUC(Users user, ref Grid grid)
        {
            InitializeComponent();
            this.user = user;
            this.DataContext = user;
            this.grid = grid;
            CalanderServiceClient calendarService = new CalanderServiceClient();
            GroupsList list = calendarService.GetGroupsByUser(user);
            foreach (Groups group in list)
            {
                AddEventsUC uC = new AddEventsUC(group, user, ref grid);
                uC.Margin = new Thickness(19);
                adminGroups.Children.Add(uC);

            }
        }
        private void btnCreateGroup_Click(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(new CreateGroupUC(user, ref grid));
        }
    }
}
