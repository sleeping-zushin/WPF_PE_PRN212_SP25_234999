using BusinessLayer.Services;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ResearchProjectManagement_SE123456
{
    /// <summary>
    /// Interaction logic for ResearchList.xaml
    /// </summary>
    public partial class ResearchList : Window
    {
        public UserAccount LoginUser { get; set; }
        public ResearchProjectService _researchProjectService = new ResearchProjectService();
        List<ResearchProject> projects = new List<ResearchProject>
{
    new ResearchProject
    {
        ProjectId = 1,
        ProjectTitle = "Project Phoenix: AI Consciousness Modeling",
        ResearchField = "Artificial Intelligence",
        StartDate = new DateOnly(2024, 1, 1),
        EndDate = new DateOnly(2027, 1, 1),
        LeadResearcherId = 1, // Giả sử ID 1 là Dr. Aris Thorne
        Budget = 1500000.00m,
        LeadResearcher = null // Bỏ qua theo yêu cầu
    },
    new ResearchProject
    {
        ProjectId = 2,
        ProjectTitle = "CRISPR-Cas9 Therapeutic Applications",
        ResearchField = "Biotechnology",
        StartDate = new DateOnly(2023, 6, 15),
        EndDate = new DateOnly(2026, 6, 15),
        LeadResearcherId = 2, // Giả sử ID 2 là Prof. Elara Vance
        Budget = 2200000.50m,
        LeadResearcher = null
    },
    new ResearchProject
    {
        ProjectId = 3,
        ProjectTitle = "Quantum Entanglement Dynamics",
        ResearchField = "Quantum Physics",
        StartDate = new DateOnly(2024, 3, 1),
        EndDate = new DateOnly(2028, 3, 1),
        LeadResearcherId = 3, // Giả sử ID 3 là Dr. Kenji Tanaka
        Budget = 5000000.00m,
        LeadResearcher = null
    },
    new ResearchProject
    {
        ProjectId = 4,
        ProjectTitle = "Neural Mapping of the Human Cortex",
        ResearchField = "Neuroscience",
        StartDate = new DateOnly(2023, 11, 1),
        EndDate = new DateOnly(2027, 11, 1),
        LeadResearcherId = 4, // Giả sử ID 4 là Dr. Lena Petrova
        Budget = 1850000.00m,
        LeadResearcher = null
    },
    new ResearchProject
    {
        ProjectId = 5,
        ProjectTitle = "Arctic Ice Melt Impact Analysis",
        ResearchField = "Climate Science",
        StartDate = new DateOnly(2024, 5, 20),
        EndDate = new DateOnly(2026, 5, 20),
        LeadResearcherId = 5, // Giả sử ID 5 là Prof. Marcus Cole
        Budget = 750000.00m,
        LeadResearcher = null
    },
    new ResearchProject
    {
        ProjectId = 6,
        ProjectTitle = "Human Genome Sequencing 2.0",
        ResearchField = "Genomics",
        StartDate = new DateOnly(2025, 1, 10),
        EndDate = new DateOnly(2028, 1, 10),
        LeadResearcherId = 6, // Giả sử ID 6 là Dr. Siona Gupta
        Budget = 3000000.00m,
        LeadResearcher = null
    },
    new ResearchProject
    {
        ProjectId = 7,
        ProjectTitle = "Graphene Superconductor Development",
        ResearchField = "Material Science",
        StartDate = new DateOnly(2023, 9, 1),
        EndDate = new DateOnly(2025, 9, 1),
        LeadResearcherId = 7, // Giả sử ID 7 là Dr. Julian Wu
        Budget = 1100000.00m,
        LeadResearcher = null
    },
    new ResearchProject
    {
        ProjectId = 8,
        ProjectTitle = "Exoplanet Atmosphere Composition Study",
        ResearchField = "Astrophysics",
        StartDate = new DateOnly(2024, 7, 15),
        EndDate = new DateOnly(2029, 7, 15),
        LeadResearcherId = 8, // Giả sử ID 8 là Prof. Freya Berg
        Budget = 4500000.00m,
        LeadResearcher = null
    },
    new ResearchProject
    {
        ProjectId = 9,
        ProjectTitle = "Predictive Modeling for Global Pandemics",
        ResearchField = "Data Science",
        StartDate = new DateOnly(2025, 2, 1),
        EndDate = new DateOnly(2027, 2, 1),
        LeadResearcherId = 9, // Giả sử ID 9 là Dr. Omar Hassan
        Budget = 900000.75m,
        LeadResearcher = null
    },
    new ResearchProject
    {
        ProjectId = 10,
        ProjectTitle = "Coral Reef Restoration Technologies",
        ResearchField = "Marine Biology",
        StartDate = new DateOnly(2023, 10, 30),
        EndDate = new DateOnly(2026, 10, 30),
        LeadResearcherId = 10, // Giả sử ID 10 là Dr. Chloe Martin
        Budget = 650000.00m,
        LeadResearcher = null
    }
};

        public ResearchList(UserAccount LoginUser)
        {
            InitializeComponent();
            this.LoginUser = LoginUser;

            if (LoginUser != null) // khi có  thông tin user đăng nhập
            {
                welcomeLabel.Content = $"Welcome, {LoginUser.Email} (Role: {LoginUser.Role})";

                if (LoginUser.Role == 3)  // neu la staff, chi duoc xem danh sach du lieu
                {
                    addButton.Visibility = Visibility.Collapsed; // ẩn nút thêm dự án
                    updateButton.Visibility = Visibility.Collapsed; // ẩn nút cập nhật dự án
                    deleteButton.Visibility = Visibility.Collapsed; // ẩn nút xóa dự án

                }
                if (LoginUser.Role == 2) // neu la manager 
                {
                    addButton.Visibility = Visibility.Visible; // hiện nút thêm dự án
                    updateButton.Visibility = Visibility.Visible; // hiện nút cập nhật dự án
                    deleteButton.Visibility = Visibility.Visible; // hiện nút xóa dự án
                }

            }
            else
            {
                welcomeLabel.Content = "Welcome, Guest";
            }
            LoadResearchProjects();
        }

        private void LoadResearchProjects()
        {
            researchProjectDataGrid.ItemsSource = null;
            var researchProjects = _researchProjectService.GetAll();
            researchProjectDataGrid.ItemsSource = researchProjects;
            researchProjectDataGrid.Items.Refresh();
        }

        private void researchProjectDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // get selected research project
            ResearchProject selectedProject = (ResearchProject)researchProjectDataGrid.SelectedItem;

            if (selectedProject != null)
            {
                // Display selected project details or perform other actions
                MessageBox.Show($"Selected Project: {selectedProject.ProjectTitle}");

                updateButton.IsEnabled = true;
                deleteButton.IsEnabled = true;
            }

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            // get selected research project
            ResearchProject selectedProject = (ResearchProject)researchProjectDataGrid.SelectedItem;

            if (selectedProject != null)
            {
                var result = MessageBox.Show(
                    $"Are you sure you want to delete the project: {selectedProject.ProjectTitle}?" // content
                    , "Confirm Deletion" // title
                    , MessageBoxButton.YesNo
                    , MessageBoxImage.Warning // icon
                    );
                if (result == MessageBoxResult.Yes)
                {
                    _researchProjectService.DeleteResearchProject(selectedProject.ProjectId);
                    LoadResearchProjects();
                }
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            ResearchProjectForm researchProjectForm = new ResearchProjectForm();
            var result = researchProjectForm.ShowDialog();
            
            if (result == true)
            {
                LoadResearchProjects();
            }
            
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            ResearchProject selectedProject = (ResearchProject)researchProjectDataGrid.SelectedItem;
            ResearchProjectForm researchProjectForm = new ResearchProjectForm(selectedProject);
            var result = researchProjectForm.ShowDialog();

            if (result == true)
            {
                LoadResearchProjects();
            }
        }
    }
}
