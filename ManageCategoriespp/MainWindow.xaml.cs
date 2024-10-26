using ManageCategoriesApp.Data.Entities;
using ManageCategoriesApp.Services;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ManageCategoriesApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadCategories()
        {
            var categories = ManageCategories.Instance.GetCategories();

            Binding categoryIDBinding = new Binding("CategoryID")
            {
                Source = categories,
                Mode = BindingMode.OneWay
            };
            txtCategoryID.SetBinding(TextBox.TextProperty, categoryIDBinding);

            Binding categoryNameBinding = new Binding("CategoryName")
            {
                Source = categories,
                Mode = BindingMode.OneWay
            };
            txtCategoryName.SetBinding(TextBox.TextProperty, categoryNameBinding);

            dgCategories.ItemsSource = categories;
        }



        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var category = new Category { CategoryName = txtCategoryName.Text };
                ManageCategories.Instance.InsertCategory(category);
                LoadCategories();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Insert Category");
            }

        }


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(txtCategoryID.Text, out int categoryId))
                {
                    var category = new Category
                    {
                        CategoryID = categoryId,
                        CategoryName = txtCategoryName.Text
                    };

                    ManageCategories.Instance.UpdateCategory(category);
                    LoadCategories();  
                    MessageBox.Show("Category updated successfully.", "Update Category");
                }
                else
                {
                    MessageBox.Show("Invalid Category ID.", "Update Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Category");
            }
        }


        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var category = new Category { CategoryID = int.Parse(txtCategoryID.Text) };
                ManageCategories.Instance.DeleteCategory(category);
                LoadCategories();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Category");
            }

        }

        private void dgCategories_Loaded(object sender, RoutedEventArgs e) => LoadCategories();

        private void dgCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCategories.SelectedItem is Category selectedCategory)
            {
                txtCategoryID.Text = selectedCategory.CategoryID.ToString();
                txtCategoryName.Text = selectedCategory.CategoryName;
            }
        }

    }
}