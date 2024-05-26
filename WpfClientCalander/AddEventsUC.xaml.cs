using MaterialDesignThemes.Wpf;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfClientCalander.ServiceCalander;

namespace WpfClientCalander
{
    /// <summary>
    /// Interaction logic for AddEventsUC.xaml
    /// </summary>
    public partial class AddEventsUC : UserControl
    {
        private Groups group;
        private EventList list;
        private Users user;
        private CalanderServiceClient serviceClient;
        private Grid grid;
        UserWindow parent;
        private Event myEvent;

        public AddEventsUC(Groups group, Users user, ref Grid grid, UserWindow parent)
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            ci.DateTimeFormat.ShortTimePattern = "HH:mm";
            Thread.CurrentThread.CurrentCulture = ci;
            this.parent = parent;
            this.group = group;
            this.DataContext = group;
            this.grid = grid;
            this.user = user;
            this.serviceClient = new CalanderServiceClient();
            list = serviceClient.GetEventsByGroup(group);
            ShowEvents();
            SetSelectionDates();
            myEvent = new Event();
            EvenInfotGrid.DataContext = myEvent;
        }
        private void ShowEvents()
        {
            existingEvents.Children.Clear();
            existingEvents.Margin = new Thickness(3.5);
            TextBlock tblk = new TextBlock();
            tblk.FontSize = 15;
            tblk.FontWeight = FontWeights.Bold;
            tblk.Foreground = new SolidColorBrush(Colors.Black);
            tblk.Text = "Existing Events:";
            existingEvents.Children.Add(tblk);
            TextBlock tb = new TextBlock();
            tb.Text = " ";
            existingEvents.Children.Add(tb);
            foreach (Event events in list)
            {
                StackPanel st = new StackPanel();
                st.Orientation = Orientation.Horizontal;
                st.Width = 180;
                st.MinHeight = 50;
                TextBlock tblkEvents = new TextBlock();
                tblkEvents.Text = "* " + events.EventName + ":  " + events.EventStart.ToString("dd/MM/yyyy HH:mm") + " - " 
                    + events.EventEnd.ToString("dd/MM/yyyy HH:mm");
                tblkEvents.FontSize = 13;
                tblkEvents.Foreground = new SolidColorBrush(Colors.DarkGray);
                tblkEvents.TextWrapping = TextWrapping.WrapWithOverflow;
                tblkEvents.Margin = new Thickness(2);
                tblkEvents.Width = 130;
                Button btnDel = new Button();
                PackIcon icon = (new PackIcon());
                icon.Kind = PackIconKind.TrashCan;
                icon.Width = 14;
                icon.Height = 20;
                btnDel.Content = icon;
                btnDel.Visibility = Visibility.Collapsed;
                btnDel.Click += (s, e) => BtnDel_Click(s, e, events);
                st.Children.Add(tblkEvents);
                st.Children.Add(btnDel);
                st.MouseEnter += AddEventsUC_MouseEnter;
                st.MouseLeave += AddEventsUC_MouseLeave;
                existingEvents.Children.Add(st);
            }
        }
        private void AddEventsUC_MouseLeave(object sender, MouseEventArgs e)
        {
            StackPanel st = sender as StackPanel;
            st.Children[1].Visibility = Visibility.Collapsed;
        }
        private void AddEventsUC_MouseEnter(object sender, MouseEventArgs e)
        {
            StackPanel st = sender as StackPanel;
            st.Children[1].Visibility = Visibility.Visible;
        }
        private void BtnDel_Click(object sender, RoutedEventArgs e, Event eventToDelete)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                if (serviceClient.DeleteEvent(eventToDelete) != 1)
                {
                    MessageBox.Show("Error deleting event from service.\n Try again.", "ERROR", MessageBoxButton.OK);
                    return;
                }
                StackPanel parentStackPanel = (StackPanel)btn.Parent;
                existingEvents.Children.Remove(parentStackPanel);
                MessageBox.Show("Event deleted successfully.", "SUCCESS", MessageBoxButton.OK);
                return;
            }
        }
        private void SetSelectionDates()
        {
            CalendarDateRange cdr1 = new CalendarDateRange(DateTime.Today.AddYears(5), DateTime.Today.AddYears(1000));
            CalendarDateRange cdr2 = new CalendarDateRange(DateTime.Today.AddYears(-50), DateTime.Today);
            dtpStart.BlackoutDates.Add(cdr1);
            dtpStart.BlackoutDates.Add(cdr2);
            dtpEnd.BlackoutDates.Add(cdr2);

            tpStart.SelectedTime = DateTime.Now;
            tpEnd.SelectedTime = DateTime.Now.AddMinutes(5);
            dtpStart.SelectedDate = DateTime.Today.AddDays(1);
        }
        private void Back()
        {
            grid.Children.Clear();
            grid.Children.Add(new GroupAdminUC(user, ref grid, parent));
        }
        private void btnAddEvent_Click(object sender, RoutedEventArgs e)
        {
            if (tbxEventName.Text == null || tbxEventName.Text == " " || tbxEventName.Text == "")
            {
                MessageBox.Show("Event name can't be empty.\n Try again.", "ERROR", MessageBoxButton.OK);
                return;
            }
            if (!serviceClient.IsEventNameFree(tbxEventName.Text))
            {
                MessageBox.Show("Event name taken.\n Try again.", "ERROR", MessageBoxButton.OK);
                return;
            }
            myEvent.EventGroup = group;
            myEvent.EventName = tbxEventName.Text;
            DateTime dtStart = DateTime.Parse(dtpStart.Text).AddMinutes(DateTime.Parse(tpStart.Text).Minute).AddHours(DateTime.Parse(tpStart.Text).Hour);
            myEvent.EventStart = dtStart;
            DateTime dtEnd = DateTime.Parse(dtpEnd.Text).AddMinutes(DateTime.Parse(tpEnd.Text).Minute).AddHours(DateTime.Parse(tpEnd.Text).Hour);
            myEvent.EventEnd = dtEnd;
            if (serviceClient.InsertEvent(myEvent) != 1)
            {
                MessageBox.Show("System error.\n Try again.", "ERROR", MessageBoxButton.OK);
                return;
            }
            MessageBox.Show("Event added successfully!", "SUCCESS", MessageBoxButton.OK);
            Back();
        }
        private void dtpStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CalendarDateRange cdr = new CalendarDateRange(DateTime.Today.AddDays(-50), DateTime.Parse(dtpStart.Text).AddDays(-1));
            dtpEnd.BlackoutDates.Clear();
            dtpEnd.BlackoutDates.Add(cdr);
            if (myEvent != null)
                myEvent.EventStart = DateTime.Parse(dtpStart.Text + " " + tpStart.Text);
        }
        private void dtpEnd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CalendarDateRange cdr = new CalendarDateRange(DateTime.Parse(dtpEnd.Text).AddDays(+1), DateTime.Today.AddYears(1000));
            dtpStart.BlackoutDates.Add(cdr);
            if (myEvent != null)
                myEvent.EventEnd = DateTime.Parse(dtpEnd.Text + " "+tpEnd.Text);
        }
        private void tpStart_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            if(dtpStart.Text!=string.Empty && tpStart.Text!= string.Empty && myEvent != null)
                    myEvent.EventStart = DateTime.Parse(dtpStart.Text +" "+ tpStart.Text);
        }
        private void tpEnd_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            if (dtpEnd.Text != string.Empty && tpEnd.Text != string.Empty && myEvent != null)

                myEvent.EventEnd = DateTime.Parse(dtpEnd.Text +" "+ tpEnd.Text);
        }
    }
}
