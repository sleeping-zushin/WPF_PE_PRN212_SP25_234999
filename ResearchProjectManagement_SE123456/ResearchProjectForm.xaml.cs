using BusinessLayer.Services;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ResearchProjectForm.xaml
    /// </summary>
    public partial class ResearchProjectForm : Window
    {
        private readonly ResearchProjectService _researchProjectService = new ResearchProjectService();
        private readonly ResearcherService _researcherService = new ResearcherService();

        private int? _updateResearchProjectId = null;
        public string Mode { get; set; } = "Add"; // Add or Edit mode
        public ResearchProjectForm()
        {
            InitializeComponent();
            Mode = "Add";
            lblFormTitle.Content = "Add New Research Project";
            LoadResearchersToComboBox();
        }

        public ResearchProjectForm(ResearchProject researchProject)
        {
            InitializeComponent();
            Mode = "Edit";
            LoadResearchersToComboBox();
            lblFormTitle.Content = "Edit Research Project";

            // Populate fields with existing data
            _updateResearchProjectId = researchProject.ProjectId;
            txtProjectTitle.Text = researchProject.ProjectTitle;
            txtResearchField.Text = researchProject.ResearchField;
            dpkStartDate.Text = researchProject.StartDate.ToString();
            dpkEndDate.Text = researchProject.EndDate.ToString();
            cbxLeadResearcher.SelectedValue = researchProject.LeadResearcherId;
            txtBudget.Text = researchProject.Budget.ToString();
        }

        private void LoadResearchersToComboBox()
        {
            var researchers = _researcherService.GetAll();

            cbxLeadResearcher.ItemsSource = researchers;
            cbxLeadResearcher.DisplayMemberPath = "FullName";
            cbxLeadResearcher.SelectedValuePath = "ResearcherId";
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // DATA RETRIEVAL
                string projectTitle = txtProjectTitle.Text;

                string researchField = txtResearchField.Text;

                // Convert DatePicker SelectedDate (DateTime?) to DateOnly
                DateOnly startDate = dpkStartDate.SelectedDate.HasValue
                    ? DateOnly.FromDateTime(dpkStartDate.SelectedDate.Value)
                    : DateOnly.MinValue;

                DateOnly endDate = dpkEndDate.SelectedDate.HasValue
                    ? DateOnly.FromDateTime(dpkEndDate.SelectedDate.Value)
                    : DateOnly.MinValue;

                int leadResearcherId = (int)(cbxLeadResearcher.SelectedValue ?? 0);

                
                decimal budget = 0;
                if (!decimal.TryParse(txtBudget.Text, out budget))
                {
                    throw new Exception("Invalid budget value. Please enter a valid decimal number.");
                }

                // VALIDATION
                // validate required fields
                if (string.IsNullOrWhiteSpace(projectTitle))
                {
                    throw new Exception("Project Title is required.");
                }
                if (string.IsNullOrWhiteSpace(researchField))
                {
                    throw new Exception("Research Field is required.");
                }
                // validate date logic
                if (startDate >= endDate)
                {
                    throw new Exception("Start Date must be earlier than End Date.");
                }
                if (projectTitle.Length < 5 || projectTitle.Length > 100)
                {
                    throw new Exception("Project Title must be between 5 and 100 characters.");
                }


                // regex for check pattern of research field:
                // each word start with a capital letter or a digit (1-9)

                // Cách 1: dùng vòng lặp
                Regex wordRegex = new Regex("^[A-Z0-9].*");
                var words = researchField.Split(' ');
                foreach (var word in words)
                {
                    if (!wordRegex.IsMatch(word))
                    {
                        throw new Exception("Each word in Research Field must start with a capital letter or a digit (1-9).");
                    }
                }
                // Cách 2: dùng LINQ
                //if (researchField.Split(' ').Any(word => !wordRegex.IsMatch(word)))
                //{
                //    throw new Exception("Each word in Research Field must start with a capital letter or a digit (1-9).");
                //}


                // title cannot contain special characters such as $, %, ^, @
                // Cách 1
                //Regex specialCharRegex = new Regex(".*[$%^@].*");
                //if (specialCharRegex.IsMatch(projectTitle))
                //{
                //    throw new Exception("Project Title cannot contain special characters such as $, %, ^, @.");
                //}

                // cách 2
                string specialChars = "$%^@";
                for (int i = 0; i < specialChars.Length; i++)
                {
                    if (projectTitle.Contains(specialChars[i]))
                    {
                        throw new Exception("Project Title cannot contain special characters such as $, %, ^, @.");
                    }
                }

                // DATA SAVING
                ResearchProject researchProject = new ResearchProject
                {
                    ProjectTitle = projectTitle,
                    ResearchField = researchField,
                    StartDate = startDate,
                    EndDate = endDate,
                    LeadResearcherId = leadResearcherId,
                    Budget = budget
                };

                if (Mode == "Add")
                {
                    _researchProjectService.AddNew(researchProject);
                    this.DialogResult = true;
                    MessageBox.Show("Research Project added successfully.");
                }
                else if (Mode == "Edit")
                {
                    researchProject.ProjectId = _updateResearchProjectId.Value;
                    _researchProjectService.Update(researchProject);
                    this.DialogResult = true;
                    MessageBox.Show("Research Project updated successfully.");
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
