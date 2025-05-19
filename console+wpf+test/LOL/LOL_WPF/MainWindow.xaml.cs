using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Globalization;

namespace LolWPF
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Hos> Heroes { get; set; } = new ObservableCollection<Hos>();
        private ObservableCollection<string> Categories { get; set; } = new ObservableCollection<string>();

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            HeroesListBox.ItemsSource = Heroes;
            CategoriesComboBox.ItemsSource = Categories;
        }

        private void LoadData()
        {
            foreach (var line in File.ReadLines("champions2017.csv").Skip(1))
                Heroes.Add(new Hos(line));

            var uniqueCategories = Heroes.Select(h => h.Category).Distinct().OrderBy(c => c);
            foreach (var category in uniqueCategories)
                Categories.Add(category);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var searchTerm = SearchTextBox.Text.Trim();
            var filtered = Heroes.Where(h => h.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            HeroesListBox.ItemsSource = filtered;
            SearchTextBox.Clear();
            CategoriesComboBox.SelectedIndex = -1;
        }

        private void CategoriesComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (CategoriesComboBox.SelectedItem is string selectedCategory)
                HeroesListBox.ItemsSource = Heroes.Where(h => h.Category == selectedCategory).ToList();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var items = HeroesListBox.ItemsSource as IEnumerable<Hos>;
            if (items == null || !items.Any())
            {
                MessageBox.Show("Nincs adat a mentéshez!");
                return;
            }

            string fileName;
            if (!string.IsNullOrEmpty(SearchTextBox.Text))
                fileName = SearchTextBox.Text;
            else if (CategoriesComboBox.SelectedItem is string category)
                fileName = category;
            else
            {
                MessageBox.Show("Válasszon ki keresési feltételt!");
                return;
            }

            fileName = string.Join("_", fileName.Split(Path.GetInvalidFileNameChars())) + ".txt";
            try
            {
                using (var writer = new StreamWriter(fileName))
                    foreach (var hero in items)
                        writer.WriteLine($"{hero.Name};{hero.Hp.ToString("N2", CultureInfo.GetCultureInfo("hu-HU"))}");
                MessageBox.Show("Sikeres mentés!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt: {ex.Message}");
            }
        }
    }
}