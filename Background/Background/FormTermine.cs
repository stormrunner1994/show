using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Background
{
    public partial class FormTermine : Form
    {
        public FormTermine(int terminindex, List<Termin> termine)
        {
            InitializeComponent();

            this.termine = termine;
            indices = new List<int>();
            gruppenindices = new List<int>();

            foreach (Termin t in termine)
            {
                if (!indices.Contains(t.index))
                    indices.Add(t.index);
                if (!gruppenindices.Contains(t.gruppenindex))
                    gruppenindices.Add(t.gruppenindex);
            }
            this.terminindex = terminindex;
            if (terminindex > -1)
            {
                buttonanpassen.Text = "Ändern";
            }
        }

        private int terminindex;
        public List<Termin> termine { get; }
        private List<Termin> änderungstermine = new List<Termin>();
        private List<int> indices = new List<int>();
        private List<int> gruppenindices = new List<int>();

        private void MVoreinstellungen()
        {          
            // Voreinstellungen für "neuer Termin"
            if (terminindex == -1)
            {
                dateTimePickereinzeldatum.MinDate = dateTimePickerwiederdatumstart.MinDate = Convert.ToDateTime(DateTime.Now.ToLongDateString());
                dateTimePickerwiederdatumende.MinDate = DateTime.Now.AddDays(1);
                DateTime vollezeit = DateTime.Now;
                TimeSpan ts = new TimeSpan(0, 59 - vollezeit.Minute, 60 - vollezeit.Second);
                vollezeit = vollezeit.Add(ts);

                dateTimePickereinzeluhrzeit.Value = dateTimePickerwiederuhrzeit.Value = Convert.ToDateTime(String.Format("{0:t}", vollezeit));

            }
            else // Voreinstellungen für "Termin bearbeiten"
            {
                int gruppenindex = termine[terminindex].gruppenindex;
                änderungstermine = new List<Termin>();

                foreach (Termin t in termine)
                {
                    if (t.gruppenindex == gruppenindex)
                        änderungstermine.Add(t);
                }

                if (änderungstermine[0].datum < DateTime.Now)
                    dateTimePickereinzeldatum.MinDate = dateTimePickerwiederdatumstart.MinDate = änderungstermine[0].datum;
                else
                    dateTimePickereinzeldatum.MinDate = dateTimePickerwiederdatumstart.MinDate = DateTime.Now;

                dateTimePickerwiederdatumende.MinDate = dateTimePickereinzeldatum.MinDate.AddDays(1);

                dateTimePickerwiederdatumstart.Value = dateTimePickereinzeldatum.Value = änderungstermine[0].datum;
                dateTimePickerwiederuhrzeit.Value = dateTimePickereinzeluhrzeit.Value = Convert.ToDateTime(änderungstermine[0].uhrzeit);
                textBoxwiedergrund.Text = textBoxeinzelgrund.Text = änderungstermine[0].grund;
                richTextBoxwiederbeschreibung.Text = richTextBoxeinzelbeschreibung.Text = änderungstermine[0].beschreibung;


                if (änderungstermine.Count > 1)// Wiederholungstermin wird geändert
                {
                    dateTimePickerwiederdatumende.Value = Convert.ToDateTime(änderungstermine.Last().datum);
                    int abstand = (änderungstermine[1].datum - dateTimePickerwiederdatumstart.Value).Days;

                    // Gleicher Tag im Monat
                    if (abstand > 28)
                    {
                        // Monatliches
                        string datum1 = änderungstermine[0].datum.ToShortDateString();
                        string datum2 = änderungstermine[1].datum.ToShortDateString();

                        if (datum1.Split('.')[0] == datum2.Split('.')[0])
                        {
                            int monat1 = Convert.ToInt32(datum1.Split('.')[1].ToString());
                            int monat2 = Convert.ToInt32(datum2.Split('.')[1].ToString());
                            if (monat2 < monat1)
                                textBoxanzahl.Text = ((monat2 + 12) - monat1).ToString();
                            else
                                textBoxanzahl.Text = (monat2 - monat1).ToString();

                            comboBoxzeit.SelectedIndex = 2;
                        }
                    }
                    else if (abstand % 7 == 0) // Wöchiges
                    {
                        textBoxanzahl.Text = (abstand / 7).ToString();

                        comboBoxzeit.SelectedIndex = 1;
                    }
                    else // Tägliches
                    {
                        textBoxanzahl.Text = abstand.ToString();

                        comboBoxzeit.SelectedIndex = 0;
                    }
                }
            }
        }
        
        private void FormTermine_Load(object sender, EventArgs e)
        {
            MVoreinstellungen();
        }

        private void MNeuerTermin(List<Termin> neuetermine)
        {
            if (neuetermine.Count > 0)
            {
                List<Termin> temptermine = new List<Termin>();

                while (neuetermine.Count > 0 || termine.Count > 0)
                {
                    if (neuetermine.Count == 0)
                    {
                        while(termine.Count > 0)
                        { 
                            temptermine.Add(termine.First());
                            termine.RemoveAt(0);
                        }
                        break;
                    }
                    else if (termine.Count == 0)
                    {
                        while (neuetermine.Count > 0)
                        {
                            temptermine.Add(neuetermine.First());
                            neuetermine.RemoveAt(0);
                        }
                        break;
                    }
                    else
                    {
                        if (termine.First().datum < neuetermine.First().datum)
                        {
                            temptermine.Add(termine.First());
                            termine.RemoveAt(0);
                        }
                        else if (termine.First().datum > neuetermine.First().datum)
                        {
                            temptermine.Add(neuetermine.First());
                            neuetermine.RemoveAt(0);
                        }
                        else // Selbes Datum
                        {
                            DateTime eins = Convert.ToDateTime(termine.First().uhrzeit);
                            DateTime zwei = Convert.ToDateTime(neuetermine.First().uhrzeit);

                            int alt = eins.Hour * 3600 + eins.Minute * 60 + eins.Second;
                            int neu = zwei.Hour * 3600 + zwei.Minute * 60 + zwei.Second;

                            if (alt < neu)
                            {
                                temptermine.Add(termine.First());
                                termine.RemoveAt(0);
                            }
                            else if (alt > neu)
                            {
                                temptermine.Add(neuetermine.First());
                                neuetermine.RemoveAt(0);
                            }
                            else // Selbes Datum und Uhrzeit
                            {
                                temptermine.Add(termine.First());
                                termine.RemoveAt(0);
                                temptermine.Add(neuetermine.First());
                                neuetermine.RemoveAt(0);
                            }
                        }
                    }

                }
                
                foreach (Termin t in temptermine)
                    termine.Add(t);

                ClassÜbergreifend.SpeichereTermineInDatei(termine);
            }
        }

        private void buttonanpassen_Click(object sender, EventArgs e)
        {
            List<Termin> neuetermine = new List<Termin>();
            bool abgeschlossen = false;

            foreach (Control c in tabPageeinzel.Controls.OfType<TextBox>())
            {
                if (c.GetType() == typeof(TextBox)) c.BackColor = Color.FromName("Window");
            }
            foreach (Control c in tabPagewieder.Controls.OfType<TextBox>())
            {
                if (c.GetType() == typeof(TextBox)) c.BackColor = Color.FromName("Window");
            }

            int index = 1;
            int gruppenindex = 1;

            // Einzeltermin
            if (tabControl1.SelectedIndex == 0)
            {
                if (ClassÜbergreifend.MKürzen(textBoxeinzelgrund.Text) != "")
                {
                    textBoxeinzelgrund.BackColor = Color.FromName("Window");

                    while (gruppenindices.Contains(gruppenindex))
                        gruppenindex++;

                    // Hinzufügen
                    while (indices.Contains(index))
                        index++;

                    neuetermine.Add(new Termin(index, gruppenindex, Convert.ToDateTime(dateTimePickereinzeldatum.Text), dateTimePickereinzeluhrzeit.Text, textBoxeinzelgrund.Text, richTextBoxeinzelbeschreibung.Text, false));
                    if (buttonanpassen.Text == "Ändern") // Ändern
                    {
                        termine.RemoveAt(terminindex);
                    }

                    MNeuerTermin(neuetermine);
                    abgeschlossen = true;
                }
                else
                    textBoxeinzelgrund.BackColor = Color.Red;

            }
            else // Wiederholungstermin
            {
                bool eingabekorrekt = true;
                int iout;
                // Prüfung der Eingabefelder
                if (ClassÜbergreifend.MKürzen(textBoxwiedergrund.Text) == "")
                {
                    textBoxwiedergrund.BackColor = Color.Red;
                    eingabekorrekt = false;
                }
                if (Int32.TryParse(textBoxanzahl.Text, out iout) == true)
                {
                    if (Convert.ToInt32(textBoxanzahl.Text) < 1)
                    {
                        textBoxanzahl.BackColor = Color.Red;
                        eingabekorrekt = false;
                    }
                }
                else
                {
                    textBoxanzahl.BackColor = Color.Red;
                    eingabekorrekt = false;
                }

                // Sind Eingaben korrekt?
                if (eingabekorrekt)
                {
                    DateTime date = Convert.ToDateTime(dateTimePickerwiederdatumstart.Text);

                    int a = 0;
                    while (date <= Convert.ToDateTime(dateTimePickerwiederdatumende.Text))
                    {
                        while (indices.Contains(index))
                            index++;
                        while (gruppenindices.Contains(gruppenindex))
                            gruppenindex++;

                        neuetermine.Add(new Termin(index,gruppenindex, date,dateTimePickerwiederuhrzeit.Text,textBoxwiedergrund.Text,richTextBoxwiederbeschreibung.Text,false));
                        indices.Add(index);
                        date = MGetDatum(date, Convert.ToInt32(textBoxanzahl.Text), comboBoxzeit.Text);
                        a++;
                    }

                    if (buttonanpassen.Text == "Ändern") // Ändern
                    {
                        int löschengruppenindex = termine[terminindex].gruppenindex;
                        int b = 0;

                        while (b < termine.Count)
                        {
                            if (termine[b].gruppenindex == löschengruppenindex)
                                termine.RemoveAt(b);
                            else
                                b++;
                        }
                    }

                    MNeuerTermin(neuetermine);
                    abgeschlossen = true;
                }
            }

            if (abgeschlossen)
                this.Close();
        }


        private DateTime MGetDatum(DateTime temp, int anzahl, string combo)
        {
            DateTime ausgabe;

            if (combo == "Tag" || combo == "Tage")
                ausgabe = temp.AddDays(anzahl);
            else if (combo == "Woche" || combo == "Wochen")
                ausgabe = temp.AddDays(anzahl * 7);
            else if (combo == "Monat" || combo == "Monate")
                ausgabe = temp.AddMonths(anzahl);
            else
                ausgabe = temp.AddYears(anzahl);

            return ausgabe;
        }

        private void dateTimePickerwiederdatumstart_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePickerwiederdatumende.Value < dateTimePickerwiederdatumstart.Value)
                dateTimePickerwiederdatumende.Value = dateTimePickerwiederdatumstart.Value.AddDays(1);
        }

        private void textBoxanzahl_TextChanged(object sender, EventArgs e)
        {
            int iout;

            if (Int32.TryParse(textBoxanzahl.Text, out iout) == true)
            {
                textBoxanzahl.BackColor = Color.FromName("Window");
                comboBoxzeit.Items.Clear();
                if (Convert.ToInt32(textBoxanzahl.Text) > 1)
                {
                    comboBoxzeit.Items.Add("Tage");
                    comboBoxzeit.Items.Add("Wochen");
                    comboBoxzeit.Items.Add("Monate");
                    comboBoxzeit.Items.Add("Jahre");
                }
                else
                {
                    comboBoxzeit.Items.Add("Tag");
                    comboBoxzeit.Items.Add("Woche");
                    comboBoxzeit.Items.Add("Monat");
                    comboBoxzeit.Items.Add("Jahr");
                }
                comboBoxzeit.SelectedIndex = 0;
            }
            else
                textBoxanzahl.BackColor = Color.Red;
        }

        private void textBoxeinzelgrund_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                buttonanpassen_Click(sender, e);
            }
        }

        private void textBoxwiedergrund_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                buttonanpassen_Click(sender, e);
            }
        }
    }
}
