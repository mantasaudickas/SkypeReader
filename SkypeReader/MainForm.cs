using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SkypeReader
{
    public partial class MainForm : Form
    {
        private const string NameStyle = "color:#2E64FE; float:left; font-size: 12px; font-family: 'Lucida Grande'";
        private const string DateStyle = "color:#848484; float:right; font-size: 12px; font-family: 'Lucida Grande'";
        private const string TextStyle = "color:#07190B; margin-bottom:15px; font-size: 12px; font-family: 'Lucida Grande'";

        private const string RemoveLinkStyle = "color:#848484; margin-left:10px; float:left; font-size: 10px; font-family: 'Lucida Grande'";

        private string _currentDatabaseFilePath;
        private string _currentSkypeName;
        private readonly IDictionary<int, Participants> _participants = new Dictionary<int, Participants>();
        private readonly IDictionary<string, Contact> _contacts = new Dictionary<string, Contact>();

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Shown"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.EventArgs"/> that contains the event data. </param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            try
            {
                ChooseSkypeDatabase();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void listContacts_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.listContacts.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                this.DisplayChat(_currentDatabaseFilePath);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                this.listContacts.Enabled = true;
                Cursor.Current = Cursors.Arrow;
            }
        }

        private void textHistory_NewWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                MessageBox.Show(this.textHistory.StatusText);
                e.Cancel = true;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void openSkypeDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ChooseSkypeDatabase();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void ChooseSkypeDatabase()
        {
            string applicationDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string skypeFolder = Path.Combine(applicationDataFolder, "Skype");

            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = @"Skype database file|main.db";
                dialog.InitialDirectory = skypeFolder;
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    _currentDatabaseFilePath = dialog.FileName;
                    _currentSkypeName = this.LoadContacts(_currentDatabaseFilePath);
                    this.LoadParticipants(_currentDatabaseFilePath);

                    DisplaySkypeLog(_currentDatabaseFilePath);
                }
            }
        }

        private void exitApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void textHistory_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            try
            {
                string url = e.Url.ToString();

                if (url.StartsWith("remove:"))
                {
                    e.Cancel = true;
                    this.RemoveMessage(_currentDatabaseFilePath, url.Substring("remove:".Length));
                }
                else if (!url.Equals("about:blank"))
                {
                    e.Cancel = true;
                    Process.Start(url);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void removeCompleteChatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedIndex = this.listContacts.SelectedIndex;
                if (selectedIndex < 0)
                    return;

                ChatInfo chatInfo = (ChatInfo)this.listContacts.SelectedItem;
                this.RemoveChat(_currentDatabaseFilePath, chatInfo.ChatName);
                this.listContacts.Items.RemoveAt(selectedIndex);

                if (this.listContacts.Items.Count > selectedIndex)
                    this.listContacts.SelectedIndex = selectedIndex;
                else if (selectedIndex > 0)
                    this.listContacts.SelectedIndex = selectedIndex-1;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private string LoadContacts(string databaseFilePath)
        {
            _contacts.Clear();

            string mySkypeName = string.Empty;
            using (SQLiteConnection connection = this.GetConnection(databaseFilePath))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand("select skypename from Accounts", connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            mySkypeName = reader.GetString(0);
                        }
                    }
                }

                using (SQLiteCommand command = new SQLiteCommand("select skypename, displayname, given_displayname from Contacts", connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string skypeName = (string) reader.GetValue(0);
                            string displayName = reader.GetValue(1) as string;
                            string givenDisplayname = reader.GetValue(2) as string;

                            Contact contact = new Contact(skypeName, displayName, givenDisplayname);
                            _contacts[skypeName] = contact;
                        }
                    }
                }

                connection.Close();
            }
            return mySkypeName;
        }

        private void LoadParticipants(string databaseFilePath)
        {
            _participants.Clear();

            using (SQLiteConnection connection = this.GetConnection(databaseFilePath))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand("select distinct convo_id, identity from Participants", connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string displayName = reader.GetString(1);

                            // dont add my self
                            if (displayName.Equals(_currentSkypeName, StringComparison.InvariantCultureIgnoreCase))
                                continue;

                            Contact contact;
                            if (_contacts.TryGetValue(displayName, out contact))
                            {
                                displayName = contact.Name;
                            }

                            Participants participants;
                            if (!_participants.TryGetValue(id, out participants))
                            {
                                participants = new Participants(id);
                                _participants.Add(id, participants);
                            }

                            participants.Names.Add(displayName);
                        }
                    }
                }
            }
        }

        private void DisplaySkypeLog(string databaseFilePath, int selectedIndex = 0)
        {
            using (SQLiteConnection connection = this.GetConnection(databaseFilePath))
            {
                connection.Open();
                this.PopulateChatList(connection);
                connection.Close();
            }

            if (this.listContacts.Items.Count > 0)
            {
                if (this.listContacts.Items.Count > selectedIndex)
                    selectedIndex = 0;
                this.listContacts.SelectedIndex = selectedIndex;
            }
        }

        private void DisplayChat(string databaseFilePath)
        {
            if (this.listContacts.SelectedIndex < 0)
                return;

            ChatInfo chatInfo = (ChatInfo) this.listContacts.SelectedItem;

            using (SQLiteConnection connection = this.GetConnection(databaseFilePath))
            {
                connection.Open();
                PopulateChat(connection, chatInfo.ChatName);
                connection.Close();
            }
        }

        private void RemoveMessage(string databaseFilePath, string id)
        {
            using (SQLiteConnection connection = this.GetConnection(databaseFilePath))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(string.Format("delete from Messages where id = {0}", id), connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        private void RemoveChat(string databaseFilePath, string chatName)
        {
            using (SQLiteConnection connection = this.GetConnection(databaseFilePath))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(string.Format("delete from Messages where chatName = '{0}'", chatName), connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        private void PopulateChatList(SQLiteConnection connection)
        {
            this.listContacts.Items.Clear();

            using (SQLiteCommand command = new SQLiteCommand("select distinct chatname, convo_id from Messages order by timestamp desc", connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string chatName = (reader.GetValue(0) as string) ?? "unresolved";
                        int convoId = reader.GetInt32(1);

                        ChatInfo chatInfo = new ChatInfo(chatName);
                        chatInfo.Name = chatName;

                        Participants participants;
                        if (_participants.TryGetValue(convoId, out participants))
                        {
                            chatInfo.Name = participants.ToString();
                        }

                        this.listContacts.Items.Add(chatInfo);
                    }
                }
            }
        }

        private void PopulateChat(SQLiteConnection connection, string chatName)
        {
            string commandText = string.Format("select id, timestamp, body_xml, from_dispname from Messages where chatname = '{0}' order by timestamp asc", chatName);

            StringBuilder html = new StringBuilder();
            html.Append("<html><meta charset=\"UTF-8\"><body>");

            html.Append(
                @"<script>function labelOnClick (me) {
		                    var makeDivId=function (id) {return id + ""_div"";}; 
		                    if (document.getElementById(makeDivId(me.id))) { 
                                window.location.href = 'remove:' + me.id;
			                    var div=document.getElementById(makeDivId(me.id)); 
			                    div.parentNode.removeChild(div); 
		                    }}</script>");

            using (SQLiteCommand command = new SQLiteCommand(commandText, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        int timestamp = reader.GetInt32(1);
                        object bodyValue = reader.GetValue(2);
                        string body = bodyValue != null ? bodyValue.ToString() : string.Empty;
                        string dispName = reader.GetString(3);

                        DateTime time = ConvertFromUnixTimestamp(timestamp).ToLocalTime();
/*
                        if (time >= fromDate && time <= tillDate)
                            builder.AppendFormat("{0} - {1} ({4}){2}{3}{2}{2}", time, disp_name, Environment.NewLine, body, author);
*/
                        if (!string.IsNullOrWhiteSpace(body))
                        {
                            html.AppendFormat("<div id=\"{0}_div\">", id);
                            html.AppendFormat("<div style=\"{0}\"><strong>{1}</strong></div>", NameStyle, dispName);
                            html.AppendFormat("<label style=\"{0}\" onclick=\"labelOnClick(this)\" id=\"{1}\"> (remove) </label>", RemoveLinkStyle, id);
                            html.AppendFormat("<div style=\"{0}\">{1}</div>", DateStyle, time);
                            html.AppendFormat("<br/>");
                            html.AppendFormat("<div style=\"{0}\">{1}</div>", TextStyle, body);
                            html.Append("</div>");
                        }
                    }
                }
            }
            html.Append("</body></html>");
            this.textHistory.DocumentText = html.ToString();
            
        }

        static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        private SQLiteConnection GetConnection(string databaseFilePath)
        {
            SQLiteConnectionStringBuilder connectionStringBuilder = new SQLiteConnectionStringBuilder();
            connectionStringBuilder.DataSource = databaseFilePath;

            return new SQLiteConnection(connectionStringBuilder.ConnectionString);
        }
    }
}
