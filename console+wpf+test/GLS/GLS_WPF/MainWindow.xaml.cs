using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GLS_CLI;
using Microsoft.Win32;

namespace GLS_WPF
{
    public partial class MainWindow : Window
    {
        private List<AutóAdatok> autóAdatokLista;
        private bool újRekord = true;

        public MainWindow()
        {
            InitializeComponent();
            autóAdatokLista = new List<AutóAdatok>();
            AdatokBetöltése();
            FrissítDataGrid();
        }

        private void AdatokBetöltése()
        {
            try
            {
                if (File.Exists("C:\\Users\\Gabcsi\\Desktop\\GLS\\GLS_CLI\\GLS.txt"))
                {
                    string[] sorok = File.ReadAllLines("C:\\Users\\Gabcsi\\Desktop\\GLS\\GLS_CLI\\GLS.txt");
                    foreach (string sor in sorok)
                    {
                        try
                        {
                            AutóAdatok adat = new AutóAdatok(sor);
                            autóAdatokLista.Add(adat);
                        }
                        catch (Exception)
                        {
                            // Hibás sor esetén folytatjuk a következővel
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba az adatok betöltésekor: {ex.Message}", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MezőkTörlése()
        {
            dpDátum.SelectedDate = DateTime.Today;
            txtSofőrNeve.Text = string.Empty;
            txtNapiKilométer.Text = string.Empty;
            txtKézbesítettCsomagok.Text = string.Empty;
            txtNapiFogyasztás.Text = string.Empty;
            tbValidálásÜzenet.Text = string.Empty;
            újRekord = true;
        }

        private void dgAutóAdatok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgAutóAdatok.SelectedItem is AutóAdatok kiválasztottAdat)
            {
                dpDátum.SelectedDate = kiválasztottAdat.Dátum;
                txtSofőrNeve.Text = kiválasztottAdat.SofőrNeve;
                txtNapiKilométer.Text = kiválasztottAdat.NapiKilométer.ToString();
                txtKézbesítettCsomagok.Text = kiválasztottAdat.KézbesítettCsomagokSzáma.ToString();
                txtNapiFogyasztás.Text = kiválasztottAdat.NapiFogyasztásLiterben.ToString();
                újRekord = false;
                tbValidálásÜzenet.Text = string.Empty;
            }
        }

        private bool Validálás(out DateTime dátum, out string sofőrNeve, out int napiKm, out int csomagokSzáma, out int fogyasztás)
        {
            dátum = DateTime.MinValue;
            sofőrNeve = string.Empty;
            napiKm = 0;
            csomagokSzáma = 0;
            fogyasztás = 0;

            // Minden mező kötelező
            if (dpDátum.SelectedDate == null ||
                string.IsNullOrWhiteSpace(txtSofőrNeve.Text) ||
                string.IsNullOrWhiteSpace(txtNapiKilométer.Text) ||
                string.IsNullOrWhiteSpace(txtKézbesítettCsomagok.Text) ||
                string.IsNullOrWhiteSpace(txtNapiFogyasztás.Text))
            {
                tbValidálásÜzenet.Text = "Hibás vagy hiányzó adatok!";
                return false;
            }

            // Dátum validálás
            if (!DateTime.TryParse(dpDátum.SelectedDate.ToString(), out dátum))
            {
                tbValidálásÜzenet.Text = "Hibás vagy hiányzó adatok!";
                return false;
            }

            sofőrNeve = txtSofőrNeve.Text.Trim();

            // Numerikus értékek validálása
            if (!int.TryParse(txtNapiKilométer.Text, out napiKm) || napiKm <= 0 ||
                !int.TryParse(txtKézbesítettCsomagok.Text, out csomagokSzáma) || csomagokSzáma <= 0 ||
                !int.TryParse(txtNapiFogyasztás.Text, out fogyasztás) || fogyasztás <= 0)
            {
                tbValidálásÜzenet.Text = "Hibás vagy hiányzó adatok!";
                return false;
            }

            tbValidálásÜzenet.Text = string.Empty;
            return true;
        }

        private void btnFelvitel_Click(object sender, RoutedEventArgs e)
        {
            if (Validálás(out DateTime dátum, out string sofőrNeve, out int napiKm, out int csomagokSzáma, out int fogyasztás))
            {
                // Ellenőrizzük, hogy az adott dátumú rekord már létezik-e
                if (autóAdatokLista.Any(a => a.Dátum.Date == dátum.Date))
                {
                    tbValidálásÜzenet.Text = "Ezen a dátumon már létezik rekord!";
                    return;
                }

                // Új rekord létrehozása
                AutóAdatok újAdat = new AutóAdatok(dátum, sofőrNeve, napiKm, csomagokSzáma, fogyasztás);
                autóAdatokLista.Add(újAdat);
                FrissítDataGrid();
                MezőkTörlése();
            }
        }

        private void btnMódosítás_Click(object sender, RoutedEventArgs e)
        {
            if (dgAutóAdatok.SelectedItem is AutóAdatok kiválasztottAdat && !újRekord)
            {
                if (Validálás(out DateTime dátum, out string sofőrNeve, out int napiKm, out int csomagokSzáma, out int fogyasztás))
                {
                    // Ellenőrizzük, hogy ha a dátum megváltozott, akkor az új dátum nem szerepel-e már
                    if (dátum.Date != kiválasztottAdat.Dátum.Date &&
                        autóAdatokLista.Any(a => a.Dátum.Date == dátum.Date))
                    {
                        tbValidálásÜzenet.Text = "Ezen a dátumon már létezik rekord!";
                        return;
                    }

                    // A kiválasztott rekord módosítása
                    int index = autóAdatokLista.IndexOf(kiválasztottAdat);
                    autóAdatokLista[index] = new AutóAdatok(dátum, sofőrNeve, napiKm, csomagokSzáma, fogyasztás);
                    FrissítDataGrid();
                    MezőkTörlése();
                    dgAutóAdatok.SelectedIndex = -1;
                }
            }
            else
            {
                tbValidálásÜzenet.Text = "Nincs kiválasztva módosítandó rekord!";
            }
        }

        private void btnMentés_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Szöveges fájl (*.txt)|*.txt",
                DefaultExt = "txt",
                FileName = "GLS.txt"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                    {
                        foreach (var adat in autóAdatokLista)
                        {
                            sw.WriteLine($"{adat.Dátum:yyyy-MM-dd};{adat.SofőrNeve};{adat.NapiKilométer};{adat.KézbesítettCsomagokSzáma};{adat.NapiFogyasztásLiterben}");
                        }
                    }
                    MessageBox.Show("Sikeres Mentés", "Információ", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hiba a mentés során: {ex.Message}", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void FrissítDataGrid()
        {
            dgAutóAdatok.ItemsSource = null;
            dgAutóAdatok.ItemsSource = autóAdatokLista;
        }
    }
}